using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Fundraising.Models;

namespace Fundraising.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserOrderController : ControllerBase
    {
        private readonly FundraisingDbContext _context;

        public UserOrderController(FundraisingDbContext context)
        {
            _context = context;
        }

        // GET: api/UserOrder
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _context.Orders.ToListAsync();
        }

        //單獨order table
        // GET: api/UserOrder/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        //此userid的訂單清單
        // GET: api/UserOrder/list/1
        [HttpGet("list/{id}")]
        public async Task<ActionResult<dynamic>> GetOrderList(int id)
        {
            //總額
            var prodsum = from product in _context.Products
                          join plan in _context.Plans on product.ProductId equals plan.ProductId into plan2
                          from plan in plan2.DefaultIfEmpty()
                          join order in _context.Orders on plan.PlanId equals order.PlanId into order2
                          from order in order2.DefaultIfEmpty()
                          where order.OrderStateId != 5 && order.OrderStateId != 4
                          group new { plan, order } by product.ProductId into s
                          select new
                          {
                              productId = s.Key,
                              currentAmount = s.Sum(a => a.plan.PlanPrice + a.order.AddSponsorship)
                          };


            var orderDetail = (from order in _context.Orders
                              join plan in _context.Plans on order.PlanId equals plan.PlanId
                              join product in _context.Products on plan.ProductId equals product.ProductId
                              join csum in prodsum on product.ProductId equals csum.productId
                              where order.UserId == id
                              select new
                              {
                                  userId = order.UserId,
                                  orderId = order.OrderId,
                                  orderStateId = order.OrderStateId,
                                  purchaseTime = order.PurchaseTime.ToString("yyyy-MM-dd"),
                                  productId = product.ProductId,
                                  productTitle = product.ProductTitle,
                                  productPhoto = product.Coverphoto,
                                  startTime = product.Startime.ToString("yyyy-MM-dd"),
                                  endTime = product.Endtime.ToString("yyyy-MM-dd"),
                                  currentAmount = csum.currentAmount,
                                  targetAmount = product.TargetAmount,
                                  planId = plan.PlanId,
                                  planTitle = plan.PlanTitle,
                                  planContent = plan.PlanContent,
                                  planPrice = plan.PlanPrice,
                                  AddSponsorship = order.AddSponsorship,
                                  puserId = product.UserId

                              }).OrderByDescending(x => x.orderId);
            return await orderDetail.ToListAsync();
        }

        //此orderid的詳細內容
        // GET: api/UserOrder/myorder/1
        [HttpGet("myorder/{id}")]
        public async Task<ActionResult<dynamic>> GetMyOrder(int id)
        {

            var prodsum = from product in _context.Products
                          join plan in _context.Plans on product.ProductId equals plan.ProductId into plan2
                          from plan in plan2.DefaultIfEmpty()
                          join order in _context.Orders on plan.PlanId equals order.PlanId into order2
                          from order in order2.DefaultIfEmpty()
                          where order.OrderStateId != 5 && order.OrderStateId != 4
                          group new { plan, order } by product.ProductId into s
                          select new
                          {
                              productId = s.Key,
                              currentAmount = s.Sum(a => a.plan.PlanPrice + a.order.AddSponsorship)
                          };


            var myorderDetail = from order in _context.Orders
                                join plan in _context.Plans on order.PlanId equals plan.PlanId
                                join product in _context.Products on plan.ProductId equals product.ProductId
                                join csum in prodsum on product.ProductId equals csum.productId
                                where order.OrderId == id
                                select new
                                {
                                    orderId = order.OrderId,
                                    orderStateId = order.OrderStateId,
                                    purchaseTime = order.PurchaseTime.ToString("yyyy-MM-dd"),
                                    productId = product.ProductId,
                                    productTitle = product.ProductTitle,
                                    productPhoto = product.Coverphoto,
                                    startTime = product.Startime.ToString("yyyy-MM-dd"),
                                    endTime = product.Endtime.ToString("yyyy-MM-dd"),
                                    currentAmount = csum.currentAmount,
                                    targetAmount = product.TargetAmount,
                                    planId = plan.PlanId,
                                    planTitle = plan.PlanTitle,
                                    planContent = plan.PlanContent,
                                    planPrice = plan.PlanPrice,
                                    AddSponsorship = order.AddSponsorship,
                                    RecipientName = order.RecipientName,
                                    RecipientPhone = order.RecipientPhone,
                                    RecipientMail = order.RecipientMail,
                                    RecipientAddress = order.RecipientAddress,
                                    Note = order.Note,
                                };
            return await myorderDetail.FirstOrDefaultAsync();
        }


        //ProjectManage，id==productid
        // GET: api/UserOrder/ProjectOrder/1
        [HttpGet("ProjectOrder/{id}")]
        public async Task<ActionResult<dynamic>> GetProjectOrder(int id)
        {
            var ProjectOrder = from order in _context.Orders
                               join plan in _context.Plans on order.PlanId equals plan.PlanId
                               join prod in _context.Products on plan.ProductId equals prod.ProductId
                               where plan.ProductId == id
                               select new
                               {
                                   productId = plan.ProductId,
                                   orderId = order.OrderId,
                                   orderStateId = order.OrderStateId,
                                   purchaseTime = order.PurchaseTime.ToString("yyyy-MM-dd"),
                                   planId = plan.PlanId,
                                   planTitle = plan.PlanTitle,
                                   planPrice = plan.PlanPrice,
                                   addSponsorship = order.AddSponsorship,
                                   recipientName = order.RecipientName,
                                   userId = prod.UserId,
                                   ouserId=order.UserId
                               };
            return await ProjectOrder.ToListAsync();
        }


        // PUT: api/UserOrder/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.OrderId)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
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




        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }
    }
}
