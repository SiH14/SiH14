using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.Xml.Linq;
using Fundraising.Models;

namespace Fundraising.Hubs
{
    public class ChatHub : Hub
    {

        public async Task Connect(int userid)
        {
            //使用者連線
            string userId = userid.ToString();
            await Groups.AddToGroupAsync(Context.ConnectionId, userId);
        }


        public Task SendMessageToUser(string user, string message,int userid)
        {
            // 透過Groups.SendAsync將訊息傳送給特定群組
            string userId= userid.ToString();
            return Clients.Group(user).SendAsync("ReceiveMessage", message, userid);
        }


        //public Task SendMessageToAll(string message)
        //{
        //    // 透過Clients.All.SendAsync將訊息傳送給所有連接到Hub的客戶端
        //    return Clients.All.SendAsync("ReceiveMessage", message);
        //}


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
