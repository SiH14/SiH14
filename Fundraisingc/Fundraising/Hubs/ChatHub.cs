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

        public async Task Connect(int fromuser)
        {
            //使用者連線
            string formUser = fromuser.ToString();
            await Groups.AddToGroupAsync(Context.ConnectionId, formUser);
        }


        public Task SendMessageToUser(int sender, int receiver, int chat,string content,string time)
        {
            // 透過Groups.SendAsync將訊息傳送給特定群組
            string formUser = sender.ToString();
            string toUser = receiver.ToString();
            string chatroom = chat.ToString();
            return Clients.Group(toUser).SendAsync("ReceiveMessage", formUser, toUser,chatroom, content,time);
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
