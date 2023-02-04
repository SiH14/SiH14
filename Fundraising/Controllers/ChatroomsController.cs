using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Fundraising.Models;

namespace Fundraising.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatroomsController : ControllerBase
    {
        private readonly FundraisingDbContext _context;

        public ChatroomsController(FundraisingDbContext context)
        {
            _context = context;
        }

        // GET: api/Chatrooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Chatroom>>> GetChatrooms()
        {
            return await _context.Chatrooms.ToListAsync();
        }

        // GET: api/Chatrooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Chatroom>> GetChatroom(int id)
        {
            var chatroom = await _context.Chatrooms.FindAsync(id);

            if (chatroom == null)
            {
                return NotFound();
            }

            //return chatroom;
            return chatroom;
        }

        //拿取對話清單
        [HttpGet("Chats/{id}")]
        public async Task<ActionResult<dynamic>> GetChats(int id)
        {


            var chatroom = from chats in _context.Chatrooms
                           where chats.UserId1 == id || chats.UserId2 == id
                           select new
                           {
                               chatroomId = chats.ChatroomId,
                               userId = chats.UserId1 == id ? chats.UserId2 : chats.UserId1,

                           };
            var query = from chats in chatroom
                        join user in _context.Users on chats.userId equals user.UserId
                        orderby _context.Messages.OrderByDescending(x => x.SentTime).Where(x => x.ChatroomId == chats.chatroomId).Select(x => x.MessageId).First() descending
                        select new
                        {
                            chatroomId = chats.chatroomId,
                            userId = chats.userId,
                            userName = user.UserName,
                            userPhoto = user.UserPhoto,
                            lastMsg = _context.Messages.OrderByDescending(x => x.SentTime).Where(x => x.ChatroomId == chats.chatroomId).Select(x => x.MessageContent).First(),
                            lastTime = (_context.Messages.OrderByDescending(x => x.SentTime).Where(x => x.ChatroomId == chats.chatroomId).Select(x => x.SentTime.AddHours(8)).First()).ToString("yyyy-MM-dd HH:mm"),
                            unread = _context.Messages.Where(x => x.ChatroomId == chats.chatroomId).Count(x => x.SenderId != id && x.IsRead == false)
                        };

            return await query.ToListAsync();
        }

        //拿取特定聊天室(雙方userid)
        [HttpGet("Chat/{muid}/{tuid}")]
        public async Task<ActionResult<dynamic>> GetChat(int muid, int tuid)
        {
            var query = from chats in _context.Chatrooms
                        where (chats.UserId1 == muid && chats.UserId2 == tuid) || (chats.UserId2 == muid && chats.UserId1 == tuid)
                        select chats;

            return await query.FirstOrDefaultAsync();
        }


        //拿取未讀的聊天室(senderid,chatroomId)
        [HttpPut("unread/{receiverId}/{chatroomId}")]
        public ActionResult PutThisChat(int receiverId, int chatroomId)
        {
            var query = from msg in _context.Messages
                        where msg.ChatroomId == chatroomId && msg.ReceiverId == receiverId && msg.IsRead == false
                        select msg;

            foreach (var item in query)
            {
                item.IsRead = true;
               
            }

            _context.Messages.UpdateRange(query);
            _context.SaveChanges();
            return NoContent();
        }




        // PUT: api/Chatrooms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChatroom(int id, Chatroom chatroom)
        {
            if (id != chatroom.ChatroomId)
            {
                return BadRequest();
            }

            _context.Entry(chatroom).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChatroomExists(id))
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

        // POST: api/Chatrooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Chatroom>> PostChatroom(Chatroom chatroom)
        {
            _context.Chatrooms.Add(chatroom);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetChatroom", new { id = chatroom.ChatroomId }, chatroom);
        }

        // DELETE: api/Chatrooms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChatroom(int id)
        {
            var chatroom = await _context.Chatrooms.FindAsync(id);
            if (chatroom == null)
            {
                return NotFound();
            }

            _context.Chatrooms.Remove(chatroom);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChatroomExists(int id)
        {
            return _context.Chatrooms.Any(e => e.ChatroomId == id);
        }
    }
}
