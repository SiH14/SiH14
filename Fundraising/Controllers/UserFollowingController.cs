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
    public class UserFollowingController : ControllerBase
    {
        private readonly FundraisingDbContext _context;

        public UserFollowingController(FundraisingDbContext context)
        {
            _context = context;
        }

        // GET: api/UserFollowing
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Following>>> GetFollowings()
        {
            return await _context.Followings.ToListAsync();
        }

        // GET: api/UserFollowing/5
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

        // GET: api/UserFollowing/Promyduct/1，id是userid
        [HttpGet("my/{id}")]
        public async Task<ActionResult<dynamic>> GetMyFollowing(int id)
        {
            var query = from f in _context.Followings
                        join p in _context.Products on f.ProductId equals p.ProductId 
                        join u in _context.Users on p.UserId equals u.UserId
                        where f.UserId == id
                        select new
                        {
                            userId=f.UserId,
                            productId=f.ProductId,
                            ptitle=p.ProductTitle,
                            photo=p.Coverphoto,
                            puser=u.UserName,
                        };
                
            return await query.ToListAsync();
        }

        // PUT: api/UserFollowing/5
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

        // POST: api/UserFollowing
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

        // DELETE: api/UserFollowing/5
        [HttpDelete("{uid}/{pid}")]
        public async Task<IActionResult> DeleteFollowing(int uid,int pid)
        {
            var following = await _context.Followings.FindAsync(uid,pid);
            if (following == null)
            {
                return NotFound();
            }

            _context.Followings.Remove(following);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FollowingExists(int id)
        {
            return _context.Followings.Any(e => e.UserId == id);
        }
    }
}
