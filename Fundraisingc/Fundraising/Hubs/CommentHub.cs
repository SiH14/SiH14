
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Xml.Linq;
//using Microsoft.AspNetCore.SignalR;
//using System.Threading.Tasks;

namespace Fundraising.Hubs
{
    public class CommentHub : Hub
    {
        //public async Task SendMessage(string user, string message)
        //{
        //    await Clients.All.SendAsync("ReceiveMessage", user, message);
        //}


        public async Task Connect(int userid)
        {
            //使用者連線
            string userId = userid.ToString();
            await Groups.AddToGroupAsync(Context.ConnectionId, userId);
        }

        public Task SendMessageToUser(int productuserid,string producttitle,string word1,string thisuser)
        {
            // 透過Groups.SendAsync將訊息傳送給特定群組
            string wuser = productuserid.ToString();
            return Clients.Group(wuser).SendAsync("ReceiveMessage", producttitle , word1 , thisuser);
        }

        public Task SendMessageToCom(int comuserid, string producttitle, string word1, string thisuser)
        {
            // 透過Groups.SendAsync將訊息傳送給特定群組
            string wuser = comuserid.ToString();
            return Clients.Group(wuser).SendAsync("ReceiveMessage", producttitle , word1, thisuser);
        }


        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }


        public override async Task OnDisconnectedAsync(System.Exception exception)
        {
            var userid = Context.User.Identity.Name;
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, userid);
            await base.OnDisconnectedAsync(exception);
        }
    }
}