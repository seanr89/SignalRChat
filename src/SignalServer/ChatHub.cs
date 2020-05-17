

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SignalServer
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string name, string message)
        {
            await Clients.All.SendAsync("broadcastMessage", name, message);
        }
    }
}