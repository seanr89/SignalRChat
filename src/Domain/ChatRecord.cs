
namespace Domain;

/// <summary>
/// basic record object for storing a single chat message
/// </summary>
/// <param name="username"></param>
/// <param name="message"></param>
public record ChatRecord(string username, string message)
{
    public override string ToString()
    {
        return $"User: {username} said: {message}";
    }
}
