

using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SignalServer
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string userName, string message)
        {
            await Clients.All.SendAsync("broadcastMessage", userName, message);
        }

        public async Task Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            await Clients.All.SendAsync("broadcastMessage", name, message);
        }

        /// <summary>
        /// This method broadcasts a message to all clients.
        /// </summary>
        public void BroadCastMessage(string name, string message)
        {
            Clients.All.SendAsync("broadcastMessage", name, message);
        }

        /// <summary>
        /// This method sends a message back to the caller.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="message"></param>
        public void Echo(string name, string message)
        {
            Clients.Client(Context.ConnectionId).SendAsync("echo", name, message + " (echo from server)");
        }
    }
}