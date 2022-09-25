
namespace SignalServer;

/// <summary>
/// basic record object for storing a single chat message
/// </summary>
/// <param name="username"></param>
/// <param name="message"></param>
public record ChatRecord(string username, string message, DateTime eventDate)
{
    public override string ToString()
    {
        return $"User: {username} said: {message}";
    }
}
