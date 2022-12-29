
namespace Domain;

/// <summary>
/// Store a single chat message
/// </summary>
/// <param name="username">the user tagged to the message</param>
/// <param name="message">the message content</param>
public record ChatRecord(string username, string message, DateTime eventDate)
{
    public override string ToString()
    {
        return $"User: {username} said: {message}";
    }
}
