
namespace SignalServer;

/// <summary>
/// Basic record for storing chat messages
/// </summary>
/// <param name="username">user who sent the message</param>
/// <param name="message">simple message sent</param>
public record ChatRecord(string username, string message, DateTime eventDate)
{
    public override string ToString()
    {
        return $"User: {username} said: {message}";
    }
}
