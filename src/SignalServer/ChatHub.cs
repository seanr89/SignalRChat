

using System;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace SignalServer;
public class ChatHub : Hub
{
    private ChatHistory _ChatHistory = new ChatHistory();
    public ChatHub()
    {
        
    }
    /// <summary>
    /// Supports messages being received from external sources
    /// Handles then the broadcasting of that message to all others
    /// </summary>
    /// <param name="name">App or Users Name</param>
    /// <param name="message">The string content to send across</param>
    /// <returns>a broadcastMessage to others listening</returns>
    public async Task SendMessage(string name, string message)
    {
        _ChatHistory.logMessage(new ChatRecord(name, message));
        await Clients.All.SendAsync("broadcastMessage", name, message);
    }

    /// <summary>
    /// Additionally endpoint to test reading out a chat history log
    /// UN-TESTED
    /// </summary>
    /// <returns></returns>
    public async Task GetChatHistory()
    {
        await Clients.All.SendAsync("broadcastHistory", JsonConvert.SerializeObject(_ChatHistory._history));
    }
}
