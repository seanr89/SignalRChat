using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace SignalServer;
public class ChatHub : Hub
{
    private ChatHistory _ChatHistory = new ChatHistory();
    /// <summary>
    /// Supports messages being received from external sources
    /// Handles then the broadcasting of that message to all others
    /// </summary>
    /// <param name="name">App or Users Name</param>
    /// <param name="message">The string content to send across</param>
    /// <returns>a broadcastMessage to others listening</returns>
    public async Task SendMessage(string name, string message)
    {
        _ChatHistory.logMessage(new ChatRecord(name, message, DateTime.UtcNow));
        await Clients.All.SendAsync("broadcastMessage", name, message);
    }

    /// <summary>
    /// Additional endpoint to read out a chat history log
    /// </summary>
    /// <returns>Task for a SignalR response</returns>
    public async Task GetChatHistory()
    {
        Console.WriteLine("GetChatHistory:Called");
        await Clients.All.SendAsync("broadcastHistory", JsonConvert.SerializeObject(_ChatHistory._history));
    }
}
