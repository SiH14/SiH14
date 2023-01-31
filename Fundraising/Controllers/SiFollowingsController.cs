using System;
using System.Collections.Generic;
using System.Linq;
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
    public class SiFollowingsController : ControllerBase
    {
        private readonly FundraisingDbContext _context;

        public SiFollowingsController(FundraisingDbContext context)
        {
            _context = context;
        }

        [HttpGet("tolcomment/{productid}")]//留言
        public IActionResult GettolcommentList(int productid)
        {
            var query = (from comment in _context.Comments
                         where comment.ProductId == productid
                         select comment).Count();
            var tolcomment = query;
            return Content(tolcomment.ToString());
        }

        [HttpGet("tolfaq/{productid}")]//留言
        public IActionResult GettolfaqList(int productid)
        {
            var query = (from faq in _context.Questions
                         where faq.ProductId == productid
                         select faq).Count();
            var tolfaq = query;
            return Content(tolfaq.ToString());
        }

        // GET: api/SiFollowings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Following>>> GetFollowings()
        {
            return await _context.Followings.ToListAsync();
        }

        // GET: api/SiFollowings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Following>> GetFollowing(int id)
        {
            var following = await _context.Followings.FindAsync(id);

            if (following == null)
            {
                return NotFound();
            }

            return following;
        }

        // GET: api/SiFollowings/?/?
        [HttpGet("{followuserid}/{productfollowid}")]
        public async Task<ActionResult<IEnumerable<dynamic>>> GetFollowingList(long followuserid, long productfollowid)
        {

            var query = from f in _context.Followings
                        where f.UserId == followuserid && f.ProductId == productfollowid
                        select new
                        {
                            user = f.UserId
                        };

            return await query.ToListAsync();
        }

        // PUT: api/SiFollowings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFollowing(int id, Following following)
        {
            if (id != following.UserId)
            {
                return BadRequest();
            }

            _context.Entry(following).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FollowingExists(id))
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

        // POST: api/SiFollowings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Following>> PostFollowing(Following following)
        {
            _context.Followings.Add(following);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FollowingExists(following.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFollowing", new { id = following.UserId }, following);
        }

        // DELETE: api/SiFollowings/5
        [HttpDelete("{followuserid}/{followproductid}")]
        public async Task<IActionResult> DeleteFollowing(int followuserid, int followproductid)
        {
            var following = await _context.Followings.FindAsync(followuserid, followproductid);
            if (following == null)
            {
                return NotFound();
            }

            _context.Followings.Remove(following);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //// DELETE: api/SiFollowings/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteFollowing(int id)
        //{
        //    var following = await _context.Followings.FindAsync(id);
        //    if (following == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Followings.Remove(following);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool FollowingExists(int id)
        {
            return _context.Followings.Any(e => e.UserId == id);
        }
    }
}
