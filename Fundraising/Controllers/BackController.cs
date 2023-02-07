using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Fundraising.Models;
using Microsoft.IdentityModel.Tokens;
using System.Security.Principal;
using System.Xml.Linq;

namespace 募資.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BackController : ControllerBase
    {
        public class MyDtat
        {
            public int memberTotal { get; set; }
            public int productTotal { get; set; }
            public int successesTotal { get; set; }
            public string successesMoney { get; set; }
            public Array newmember { get; set; }
            public Array hotProduct { get; set; }

        }
        private FundraisingDbContext _context;
        public BackController(FundraisingDbContext context)
        {
            _context = context;
        }



        // GET: api/back/test
        [HttpGet("test")]
        public async Task<ActionResult<IEnumerable<dynamic>>> GetOrders()
        {

            return await _context.Orders.ToListAsync();
        }
        public class Login
        {
            public string employeeAccount { get; set; }
            public string employeePassword { get; set; }
        }
        //[HttpPost] api/back/login 登入頁面
        [HttpPost("login")]
        public ActionResult Postlogin(Login login)
        {
            var user = _context.Employees.Where(x => x.Account == login.employeeAccount && x.Password == login.employeePassword).SingleOrDefault();
            if (user == null)
            {
                return Content("null");
            }
            else
            {
                CookieOptions cookieOptions =
                    new CookieOptions()
                    {
                        //HttpOnly = true,
                        Expires = DateTime.Now.AddHours(1)
                    };
                Response.Cookies.Append("employeeId", user.EmployeeId.ToString(), cookieOptions);
                return Content("成功登入");
            }
        }
        // api/back/logout
        [HttpGet("logout")]
        public ActionResult Logout()
        {
            Response.Cookies.Delete("employeeId");

            return Content("已登出");
        }

        //Get: api/Back/index
        [HttpGet("index")]
        public List<MyDtat> GetAction()
        {   //產品目前訂單金額加總
            var prodsum = from plan in _context.Plans
                          join order in _context.Orders on plan.PlanId equals order.PlanId
                          where order.OrderStateId != 5 && order.OrderStateId != 4
                          group new { plan, order } by plan.ProductId into s
                          select new
                          {
                              productId = s.Key,
                              sum = s.Sum(a => a.plan.PlanPrice + a.order.AddSponsorship)
                          };
            //成功募資 & 成功募資總額 &
            var succproduct = from p in _context.Products
                              join ps in prodsum on p.ProductId equals ps.productId
                              where p.ProductStateId == 3 && p.Endtime < DateTime.Now && ps.sum > p.TargetAmount
                              select new { p, ps };
            //篩選熱門提案 產品訂單筆數
            var pltotal = from pl in _context.Plans
                          join o in _context.Orders on pl.PlanId equals o.PlanId
                          group pl by pl.ProductId into g
                          select new 
                          { 
                            ProductId = g.Key,
                            Count = g.Count(), 
                          };
            //產品列表+產品訂單筆數
            var result = from p in _context.Products
                         join pl in pltotal on p.ProductId equals pl.ProductId
                         join ps in prodsum on pl.ProductId equals ps.productId
                         select new
                         {
                             p,
                             pl.Count,
                             ps.sum,
                             sumformat = ps.sum.ToString("C0")
                         };
            //會員近期新增列表
            var newmem = from u in _context.Users
                         where u.CreateDate > DateTime.Now.AddDays(-7)
                         select u;
            List<MyDtat> myData = new List<BackController.MyDtat>() {
            new MyDtat()
            {
                memberTotal = _context.Users.Count(),
                productTotal = _context.Products.Count(),
                successesTotal = succproduct.Count(),
                successesMoney = succproduct.Sum(x => x.ps.sum).ToString("C0"),
                newmember = newmem.ToArray(),
                hotProduct = result.ToArray()
            }
            };
            return  myData;
        }
        // GET: api/back/OrderList    取得所有訂單列表 並加入減去退款後的訂單總額
        [HttpGet("OrderList")]
        public async Task<ActionResult<IEnumerable<dynamic>>> GetOrderList()
        {
            //產品編號+產品的訂單總額
            var totalamount = from product in _context.Products
                              join plan in _context.Plans on product.ProductId equals plan.ProductId
                              join order in _context.Orders on plan.PlanId equals order.PlanId
                              group new { plan, order } by product.ProductId into s
                              select new
                              {
                                  s.Key,
                                  sum = s.Sum(a => a.plan.PlanPrice + a.order.AddSponsorship).ToString()
                              };
            //產品編號+產品取消的總額
            var cancelamount = from product in _context.Products
                               join plan in _context.Plans on product.ProductId equals plan.ProductId
                               join order in _context.Orders on plan.PlanId equals order.PlanId
                               where order.OrderStateId == 5 || order.OrderStateId == 4
                               group new { plan, order } by product.ProductId into s
                               select new
                               {
                                   s.Key,
                                   sum = s.Sum(a => a.plan.PlanPrice + a.order.AddSponsorship).ToString()
                               };
            //產品編號+產品的訂單總額+產品取消的總額+剩餘的總額
            var finallysum = from total in totalamount
                             join cancel in cancelamount on total.Key equals cancel.Key into mix
                             from left in mix.DefaultIfEmpty()
                             select
                             new
                             {
                                 productid = total.Key,
                                 total = total.sum,
                                 cancelsum = left.sum ?? "0",
                                 finallysum = (Convert.ToInt32(total.sum) - Convert.ToInt32(left.sum ?? "0"))
                             };
            var queryresult = from o in _context.Orders
                              join pl in _context.Plans on o.PlanId equals pl.PlanId
                              join p in _context.Products on pl.ProductId equals p.ProductId
                              join finsum in finallysum on p.ProductId equals finsum.productid
                              orderby o.OrderId descending
                              select new
                              {
                                  OrderId = o.OrderId.ToString(),
                                  ProductTitle = p.ProductTitle,
                                  PlanTitle = pl.PlanTitle,
                                  PurchaseTime = o.PurchaseTime.AddHours(8).ToString("yyyy-MM-dd HH:mm:ss"),
                                  OrderStateId = o.OrderStateId,
                                  TargetAmount = p.TargetAmount,
                                  Startime = p.Startime,
                                  Endtime = p.Endtime,
                                  currentAmount = finsum.finallysum
                              };
            return await queryresult.ToListAsync();
        }
        // GET: api/back/Odetails/id 取得某一筆訂單明細
        [HttpGet("Odetails/{id}")]
        public async Task<ActionResult<IEnumerable<dynamic>>> GetOdetails(int id)
        {
            var result = from o in _context.Orders
                         join pl in _context.Plans on o.PlanId equals pl.PlanId
                         where o.OrderId == id
                         select new
                         {
                             orderId = o.OrderId,
                             planId = o.PlanId,
                             orderDateId = o.OrderDateId,
                             userId = o.UserId,
                             recipientName = o.RecipientName,
                             recipientPhone = o.RecipientPhone,
                             recipientAddress = o.RecipientAddress,
                             note = o.Note,
                             masterCardId = o.MasterCardId,
                             orderStateId = o.OrderStateId,
                             purchaseTime = o.PurchaseTime.ToString("yyyy-MM-dd HH:mm:ss"),
                             addSponsorship = o.AddSponsorship,
                             postCode = o.PostCode,
                             recipientMail = o.RecipientMail,
                             productId = pl.ProductId,
                             planTitle = pl.PlanTitle,
                             planContent = pl.PlanContent,
                             planPrice = pl.PlanPrice,
                             planPhoto = pl.PlanPhoto,
                         };

            return await result.ToListAsync();
        }
        // GET: api/back/Refundetails/id 取得某一筆正在取消申請的訂單明細
        [HttpGet("Refundetails/{id}")]
        public async Task<ActionResult<IEnumerable<dynamic>>> GetRefundetails(int id)
        {
            var result = from o in _context.Orders
                         join pl in _context.Plans on o.PlanId equals pl.PlanId
                         join re in _context.Refunds on o.OrderId equals re.OrderId
                         where o.OrderId == id
                         select new
                         {
                             orderId = o.OrderId,
                             planId = o.PlanId,
                             orderDateId = o.OrderDateId,
                             userId = o.UserId,
                             recipientName = o.RecipientName,
                             recipientPhone = o.RecipientPhone,
                             recipientAddress = o.RecipientAddress,
                             note = o.Note,
                             masterCardId = o.MasterCardId,
                             orderStateId = o.OrderStateId,
                             purchaseTime = o.PurchaseTime.ToString("yyyy-MM-dd HH:mm:ss"),
                             addSponsorship = o.AddSponsorship,
                             postCode = o.PostCode,
                             recipientMail = o.RecipientMail,
                             productId = pl.ProductId,
                             planTitle = pl.PlanTitle,
                             planContent = pl.PlanContent,
                             planPrice = pl.PlanPrice,
                             planPhoto = pl.PlanPhoto,
                             re.RefundId,
                             re.RefundStateId,
                             re.RefundResult
                         };

            return await result.ToListAsync();
        }

        // GET: api/back/PreProductList  取得待審核產品列表
        [HttpGet("PreProductList")]
        public async Task<ActionResult<IEnumerable<dynamic>>> GetPreProductList()
        {
            var prodsum = from product in _context.Products
                          join plan in _context.Plans on product.ProductId equals plan.ProductId
                          join order in _context.Orders on plan.PlanId equals order.PlanId
                          where order.OrderStateId != 5 && order.OrderStateId != 4
                          group new { plan, order } by product.ProductId into s
                          select new
                          {
                              productId = s.Key,
                              sum = s.Sum(a => a.plan.PlanPrice + a.order.AddSponsorship).ToString()
                          };

            var result = from p in _context.Products
                         join ps in prodsum on p.ProductId equals ps.productId into s
                         from get in s.DefaultIfEmpty()
                         where p.ProductStateId == 1
                         orderby p.ProductId descending
                         select new
                         {
                             p,
                             sum = get.sum ?? "0",
                         };


            return await result.ToListAsync();
        }
        // GET: api/back/ProductList  取得所有產品列表
        [HttpGet("ProductList")]
        public async Task<ActionResult<IEnumerable<dynamic>>> GetProducts()
        {
            var prodsum = from product in _context.Products
                          join plan in _context.Plans on product.ProductId equals plan.ProductId
                          join order in _context.Orders on plan.PlanId equals order.PlanId
                          where order.OrderStateId != 5 && order.OrderStateId != 4
                          group new { plan, order } by product.ProductId into s
                          select new
                          {
                              productId = s.Key,
                              sum = s.Sum(a => a.plan.PlanPrice + a.order.AddSponsorship).ToString()
                          };

            var result = from p in _context.Products
                         join ps in prodsum on p.ProductId equals ps.productId into s
                         from get in s.DefaultIfEmpty()
                         orderby p.ProductId descending
                         select new
                         {
                             p,
                             sum = get.sum ?? "0",
                         };


            return await result.ToListAsync();
        }
        // GET: api/back/Pdetails/id 取得產品明細
        [HttpGet("Pdetails/{id}")]
        public async Task<ActionResult<IEnumerable<dynamic>>> GetPdetails(int id)
        {
            var sumprive = from pl in _context.Plans
                           join o in _context.Orders on pl.PlanId equals o.PlanId
                           where o.OrderStateId != 5 && o.OrderStateId != 4
                           group new { pl, o } by new { pl.ProductId } into s
                           select new
                           {
                               ProductId = s.Key.ProductId,
                               sum = s.Sum(x => x.pl.PlanPrice + x.o.AddSponsorship)
                           };
            var pdquery = from pd in _context.Products
                          join sp in sumprive on pd.ProductId equals sp.ProductId into ps
                          from sp in ps.DefaultIfEmpty()
                          where pd.ProductId == id
                          select new
                          {
                              pd.ProductId,
                              pd.ProductTitle,
                              pd.ProductContent,
                              pd.TargetAmount,
                              TargetAmountstring = (pd.TargetAmount).ToString("C0"),
                              Startime = (pd.Startime).ToString("yyyy-MM-dd"),
                              Endtime = (pd.Endtime).ToString("yyyy-MM-dd"),
                              pd.ProductStateId,
                              pd.PrincipalId,
                              pd.PrincipalName,
                              pd.PrincipalPhone,
                              pd.PrincipalEmail,
                              pd.PrincipalBankAccount,
                              pd.Featured,
                              pd.Coverphoto,
                              sum = sp.sum == null ? 0 : sp.sum,
                              sumstring = sp.sum == null ? "NT$0" : sp.sum.ToString("c0"),
                          };
            return await pdquery.ToListAsync();
        }
        // GET: api/back/Pplans/id 取得產品方案
        [HttpGet("Pplans/{id}")]
        public async Task<ActionResult<IEnumerable<dynamic>>> Pplans(int id)
        {
            var query = _context.Plans.Where(x => x.ProductId == id).Select(x => new
            {
                planId = x.PlanId,
                productId = x.ProductId,
                planTitle = x.PlanTitle,
                planContent = x.PlanContent,
                planPrice = x.PlanPrice.ToString("c0"),
                planPhoto = x.PlanPhoto
            });
            return await query.ToListAsync();
        }
        // GET: api/back/Pquestion/id 取得產品方案
        [HttpGet("Pquestion/{id}")]
        public async Task<ActionResult<IEnumerable<dynamic>>> Pquestion(int id)
        {
            var query = _context.Questions.Where(x => x.ProductId == id).Select(x => x);
            return await query.ToListAsync();
        }




        // PUT: api/back/Products/ID   修改產品
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("Products/{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        //回傳值
        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }

        [HttpGet("getcomment/{id}")]
        public async Task<ActionResult<IEnumerable<dynamic>>> Getcomment(int id)
        {
            var p = _context.Products.Select(x => new
            {
                x.ProductId,
                x.Coverphoto,
                x.ProductTitle,
                x.PrincipalName,
                comcount = x.Comments.Count(),
                Comments = x.Comments.Select(com => new
                {
                    com.CommentId,
                    com.CommentContent,
                    Commenttime = com.Commenttime.ToString("yyyy-MM-dd HH:mm:ss"),
                    comuser = new
                    {
                        com.User.UserId,
                        com.User.UserName,
                        com.User.UserPhoto
                    },
                    anscount = com.Answers.Count(),
                    Answers = com.Answers.Select(ans => new
                    {
                        ans.CommentId,
                        ans.AnswerId,
                        ans.AnswerContent,
                        AnswerTime = ans.AnswerTime.ToString("yyyy-MM-dd HH:mm:ss"),
                        ansUser = new
                        {
                            ans.User.UserId,
                            ans.User.UserName,
                            ans.User.UserPhoto
                        }
                    })
                })
            }).Where(x => x.ProductId == id);


            return await p.ToListAsync();
        }
        //api/back/users
        [HttpGet("Users")]
        public async Task<ActionResult<IEnumerable<dynamic>>> GetUser()
        {
            var result = _context.Users.Select(x => new
            {
                UserId = x.UserId.ToString(),
                UserName = x.UserName,
                //UserIntro = x.UserIntro,
                CreateDate = x.CreateDate,
                UserEmail = x.UserEmail,
                //UserPassword = x.UserPassword,
                UserPhone = x.UserPhone,
                //UserBirthday = x.UserBirthday,
                //UserGender = x.UserGender,
                //UserFblink = x.UserFblink,
                //UserPhoto = x.UserPhoto
            });
            return await result.ToListAsync();
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<dynamic>>> GetEmpolyee()
        {
            var result = _context.Users.Select(x => new
            {
                UserId = x.UserId.ToString(),
                UserEmail = x.UserEmail,
                UserPassword = x.UserPassword,
                UserName = x.UserName,
                UserPhone = x.UserPhone,
                UserBirthday = x.UserBirthday,
                UserGender = x.UserGender,
                UserIntro = x.UserIntro,
                UserFblink = x.UserFblink,
                CreateDate = x.CreateDate,
                UserPhoto = x.UserPhoto
            });
            return await result.ToListAsync();
        }

        // GET: api/back/Employees
        [HttpGet("Employees")]
        public async Task<ActionResult<IEnumerable<dynamic>>> GetEmployees()
        {
            var result = _context.Employees.Select(x => new
            {
                account = x.Account,
                password = x.Password,
                email = x.Email,
                employeeId = x.EmployeeId,
                employeephoto = x.Employeephoto,
                name = x.Name,
                phone = x.Phone,
                sexy = (x.Sexy == true)? "男":"女",
                position = x.PositionNavigation.PositionName,
                status = x.StatusNavigation.StatusName
            });
            return await result.OrderByDescending(x=>x.employeeId).ToListAsync();
        }

        // GET: api/back/Employees/5
        [HttpGet("Employees/{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return  employee;
        }
        //GET: api/back/Products
        [HttpGet("Products")]
        public async Task<ActionResult<IEnumerable<Product>>> GetComProductsList()
        {

            return await _context.Products.OrderByDescending(x=>x.ProductId).ToListAsync();
        }


    }
}




