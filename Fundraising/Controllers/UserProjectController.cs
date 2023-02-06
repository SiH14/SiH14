using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Fundraising.Models;

namespace Fundraising.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProjectController : ControllerBase
    {
        private readonly FundraisingDbContext _context;

        public UserProjectController(FundraisingDbContext context)
        {
            _context = context;
        }

        // GET: api/UserProject
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        // GET: api/UserProject/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        //此use的提案紀錄列表，id為userid
        // GET: api/UserProject/prodlist/5
        [HttpGet("prodlist/{id}")]
        public async Task<ActionResult<dynamic>> GetProdlist(int id)
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

            var query = from prod in _context.Products
                        join sum in prodsum on prod.ProductId equals sum.productId
                        where prod.UserId == id
                       select new
                       {
                           productId=prod.ProductId,
                           coverphoto=prod.Coverphoto,
                           productTitle =prod.ProductTitle,
                           startime=prod.Startime.ToString("yyyy-MM-dd"),
                           endtime = prod.Endtime.ToString("yyyy-MM-dd"),
                           targetAmount=prod.TargetAmount,
                           currentAmount=sum.currentAmount,
                           productStateId=prod.ProductStateId,
                       };

            return await query.OrderByDescending(x=>x.productId).ToListAsync();
        }

        // PUT: api/UserProject/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
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

        // POST: api/UserProject
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.ProductId }, product);
        }

        // DELETE: api/UserProject/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
