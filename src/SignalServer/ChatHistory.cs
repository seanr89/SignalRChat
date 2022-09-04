
namespace SignalServer;

/// <summary>
/// Basic class to support the storing, updating and cleaning of previous chat messages!
/// </summary>
public class ChatHistory
{
    public List<ChatRecord> _history { get; private set; }
    private static int _maxCount = 5;
    public ChatHistory()
    {
        _history = new List<ChatRecord>();
    }

    /// <summary>
    /// Support logging of message with history clean up
    /// </summary>
    /// <param name="record"></param>
    public void logMessage(ChatRecord record)
    {
        if(_history.Count >= _maxCount)
        {
            _history.Remove(_history.First());
        }
        this._history.Add(record);
    }
}