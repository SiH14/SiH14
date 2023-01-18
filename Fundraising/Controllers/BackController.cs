using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Fundraising.Models;



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
        public class OrderInfo
        {
            public int OrderId;
            public int UserId;
            public string ProductTitle;
            public string PlanTitle;
            public DateTime PurchaseTime;
            public int OrderStateId;
            public int CurrentAmount;
            public int TargetAmount;
            public DateTime Startime;
            public DateTime Endtime;

        }

        private FundraisingDbContext _context;
        public BackController(FundraisingDbContext context)
        {
            _context = context;
        }


        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<dynamic>>> GetOrders()
        {

            return await _context.Orders.ToListAsync();
        }

        //Get: api/Back/index
        [HttpGet("index")]
        public List<MyDtat> GetAction()
        {
            var prodsum = from product in _context.Products
                          join plan in _context.Plans on product.ProductId equals plan.ProductId
                          join order in _context.Orders on plan.PlanId equals order.PlanId
                          group new { plan, order } by product.ProductId into s
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
            //    _context.Products.Where(x => x.Endtime < System.DateTime.Now &&
            //x.CurrentAmount > x.TargetAmount &&
            //x.ProductStateId == 3);
            //篩選熱門提案
            //產品訂單筆數
            var pltotal = from pl in _context.Plans
                          join o in _context.Orders on pl.PlanId equals o.PlanId 
                          group pl by pl.ProductId into g
                          select new { ProductId = g.Key, Count = g.Count(), };
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
            List<MyDtat> myData = new List<MyDtat>() {
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


            return myData;
        }
        // GET: api/back/OrderList    訂單+方案+提案
        [HttpGet("OrderList")]
        public async Task<ActionResult<IEnumerable<dynamic>>> GetOrderList()
        {
            var prodsum = from product in _context.Products
                          join plan in _context.Plans on product.ProductId equals plan.ProductId
                          join order in _context.Orders on plan.PlanId equals order.PlanId
                          group new { plan, order } by product.ProductId into s
                          select new
                          {
                              productId = s.Key,
                              sum = s.Sum(a => a.plan.PlanPrice + a.order.AddSponsorship)
                          };

            var query = from o in _context.Orders
                        join pl in _context.Plans on o.PlanId equals pl.PlanId
                        join p in _context.Products on pl.ProductId equals p.ProductId
                        join ps in prodsum on p.ProductId equals ps.productId
                        select new
                        {
                            OrderId = o.OrderId,
                            UserId = o.UserId,
                            ProductTitle = p.ProductTitle,
                            PlanTitle = pl.PlanTitle,
                            PlanPrice = pl.PlanPrice,
                            PurchaseTime = o.PurchaseTime,
                            OrderStateId = o.OrderStateId,
                            CurrentAmount = ps.sum,
                            TargetAmount = p.TargetAmount,
                            Startime = p.Startime,
                            Endtime = p.Endtime
                        };


            return await query.ToListAsync();
        }

        // GET: api/back/ProductList
        [HttpGet("ProductList")]
        public async Task<ActionResult<IEnumerable<dynamic>>> GetProducts()
        {
            var prodsum = from product in _context.Products
                          join plan in _context.Plans on product.ProductId equals plan.ProductId
                          join order in _context.Orders on plan.PlanId equals order.PlanId
                          group new { plan, order } by product.ProductId into s
                          select new
                          {
                              productId = s.Key,
                              sum = s.Sum(a => a.plan.PlanPrice + a.order.AddSponsorship)
                          };

            var result = from p in _context.Products
                         join ps in prodsum on p.ProductId equals ps.productId
                         select new
                         {
                             p,
                             ps.sum
                         };
            return await result.ToListAsync();
        }


    }
}




