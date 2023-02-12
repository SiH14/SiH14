using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Fundraising.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Fundraising.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserInfoController : ControllerBase
    {
        private readonly FundraisingDbContext _context;
        public UserInfoController(FundraisingDbContext context)
        {
            _context = context;
        }

        // GET: api/UserInfo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {

            return await _context.Users.ToListAsync();
        }


        //拿取user基本資料
        // GET: api/UserInfo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<dynamic>> GetUser(int id)
        {
            var productcnt = from user in _context.Users
                             join product in _context.Products on user.UserId equals product.UserId into p
                             from product in p.DefaultIfEmpty()
                             group product by user.UserId into grouped
                             select new
                             {
                                 userid = grouped.Key,
                                 productCNT = grouped.Count(a => a != null),
                             };

            var ordercnt = from user in _context.Users
                           join order in _context.Orders on user.UserId equals order.UserId into o
                           from order in o.DefaultIfEmpty()
                           group order by user.UserId into grouped
                           select new
                           {
                               userid = grouped.Key,
                               orderCNT = grouped.Count(a => a != null),
                           };


            var query = from user in _context.Users
                        join pcnt in productcnt on user.UserId equals pcnt.userid into p
                        from pcnt in p.DefaultIfEmpty()
                        join ocnt in ordercnt on user.UserId equals ocnt.userid into o
                        from ocnt in o.DefaultIfEmpty()
                        where user.UserId == id
                        select new
                        {
                            user.UserId,
                            user.UserName,
                            user.UserIntro,
                            user.UserPhoto,
                            pcnt.productCNT,
                            ocnt.orderCNT
                        };

            return await query.FirstOrDefaultAsync();
        }



        //拿取userName
        // GET: api/UserInfo/name/5
        [HttpGet("name/{id}")]
        public async Task<ActionResult<dynamic>> GetName(int id)
        {
            var query = from user in _context.Users
                        where user.UserId == id
                        select user.UserName;
            return await query.FirstOrDefaultAsync();
        }

        [HttpGet("ProductList/{id}")]
        public async Task<ActionResult<dynamic>> GetProduct(int id)
        {
            var productlist = from user in _context.Users
                              join prod in _context.Products on user.UserId equals prod.UserId
                              where prod.UserId == id
                              select new
                              {
                                  user.UserId,
                                  user.UserName,
                                  prod
                              };
            return await productlist.OrderByDescending(x=>x.prod.ProductId).ToListAsync();
        }

        [HttpGet("OrderList/{id}")]
        public async Task<ActionResult<dynamic>> GetOrder(int id)
        {
            var orderlist = from user in _context.Users
                              join order in _context.Orders on user.UserId equals order.UserId
                              join plan in _context.Plans on order.PlanId equals plan.PlanId
                              join prod in _context.Products on plan.ProductId equals prod.ProductId
                              join pu in _context.Users on prod.UserId equals pu.UserId
                              where order.UserId == id
                              select new
                              {
                                  prod,
                                  pu.UserName,
                                  order.OrderId

                              };
            return await orderlist.OrderByDescending(x=>x.OrderId).ToListAsync();
        }


        [HttpGet("Setting/{id}")]
        public async Task<ActionResult<dynamic>> UserSetting(int id)
        {
            var query = from user in _context.Users
                        where user.UserId == id
                        select new
                        {
                            userId=user.UserId,
                            userEmail=user.UserEmail,
                            userPassword=user.UserPassword,
                            userName=user.UserName,
                            userPhone=user.UserPhone,
                            userBirthday=user.UserBirthday.ToString("yyyy-MM-dd"),
                            userGender=user.UserGender,
                            userIntro=user.UserIntro,
                            userFblink=user.UserFblink,
                            createDate=user.CreateDate.ToString("yyyy-MM-dd"),
                            userPhoto=user.UserPhoto
                        };


            return await query.FirstOrDefaultAsync();

        }

        // PUT: api/UserInfo/Setting/5
        [HttpPut("Setting/{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/UserInfo
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        // DELETE: api/UserInfo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
