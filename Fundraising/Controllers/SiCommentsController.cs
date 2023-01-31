using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Fundraising.Models;
using static System.Net.Mime.MediaTypeNames;

namespace Fundraising.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SiCommentsController : ControllerBase
    {
        private readonly FundraisingDbContext _context;

        public SiCommentsController(FundraisingDbContext context)
        {
            _context = context;
        }

        // GET: api/SiComments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comment>>> GetComments()
        {
            return await _context.Comments.ToListAsync();
        }

        // GET: api/SiComments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            return comment;
        }

        // GET: api/SiComments/comment/{pruductid}  留言
        [HttpGet("comment/{productid}")]
        public async Task<ActionResult<dynamic>> GetCommentList(int productid)
        {
            var query = from comment in _context.Comments
                        join u in _context.Users on comment.UserId equals u.UserId
                        where comment.ProductId == productid
                        select new
                        {
                            CommentID = comment.CommentId,
                            userphoto = u.UserPhoto,
                            CommentContent = comment.CommentContent,
                            Commenttime = comment.Commenttime.ToString("yyyy年MM月dd日 HH:mm"),
                            userid = comment.UserId,
                            username = u.UserName
                        };
            return await query.ToListAsync();
        }

        [HttpGet("Answer")]
        public async Task<ActionResult<dynamic>> GetAnsList()
        {
            var oderby = from answer in _context.Answers
                        orderby answer.AnswerTime
                         select answer;
            var query = from answer in oderby
                        join u in _context.Users on answer.UserId equals u.UserId
                        orderby answer.CommentId
                        select new
                        {
                            answerid = answer.AnswerId,
                            commentid = answer.CommentId,
                            answercontent = answer.AnswerContent,
                            answertime = answer.AnswerTime.ToString("yyyy年MM月dd日 HH:mm"),
                            userid = answer.UserId,
                            username = u.UserName
                        };
            return await query.ToListAsync();
        }


        [HttpGet("AnsP")]
        public async Task<ActionResult<dynamic>> GetAnsPList()
        {
            var pro = from comment in _context.Comments
                        where comment.ProductId == 15
                        select new
                        {
                            CommentID = comment.CommentId,
                            CommentContent = comment.CommentContent,
                            Commenttime = comment.Commenttime.ToString("yyyy年MM月dd日 HH:mm")
                        };

            var query = from p in pro
                        join a in _context.Answers on p.CommentID equals a.CommentId into g
                        from a in g.DefaultIfEmpty()
                        select new
                        {
                            id = p.CommentID,
                            con = a.AnswerContent
                        };
            //select new
            //{
            //    ProductId = comment.ProductId,
            //    CommentId = comment.CommentId,
            //    AnsID = Answer.AnswerId,
            //    AnswerContent = Answer.AnswerContent,
            //    AnswerTime = Answer.AnswerTime

            //};
            return await query.ToListAsync();
        }

        [HttpGet("editcomment/{id}")]
        public async Task<ActionResult<dynamic>> Geteditans(int id)
        {
            var query = from comment in _context.Comments
                        where comment.CommentId == id
                        select new
                        {
                            commentid = comment.CommentId,
                            commentcontent = comment.CommentContent,
                            commenttime = comment.Commenttime,
                            productid = comment.ProductId,
                            userid = comment.UserId
                        };
            return await query.FirstOrDefaultAsync();
        }

        // PUT: api/SiComments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComment(int id, Comment comment)
        {
            if (id != comment.CommentId)
            {
                return BadRequest();
            }

            _context.Entry(comment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(id))
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

        // POST: api/SiComments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Comment>> PostComment(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComment", new { id = comment.CommentId }, comment);
        }

        // DELETE: api/SiComments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CommentExists(int id)
        {
            return _context.Comments.Any(e => e.CommentId == id);
        }
    }
}
