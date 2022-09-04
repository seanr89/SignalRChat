
using Domain;

namespace SignalServer;

/// <summary>
/// Basic class to support the storing, updating and cleaning of previous chat messages!
/// </summary>
public class ChatHistory
{
    public List<ChatRecord> _history { get; private set; }
    private static int _maxCount = 10;
    public ChatHistory()
    {
        _history = new List<ChatRecord>();
        InitialiseTestData();
    }

    /// <summary>
    /// Initialistion of seed/test historical data
    /// </summary>
    void InitialiseTestData()
    {
        _history.Add(new ChatRecord("Sean", "Hi All"));
        _history.Add(new ChatRecord("Ross", "Hi Sean"));
        _history.Add(new ChatRecord("Sean", "Thanks"));
        _history.Add(new ChatRecord("Conor", "Nawwww"));
    }

    /// <summary>
    /// Support logging of message with history clean up
    /// </summary>
    /// <param name="record">ChatRecord for recording</param>
    public void logMessage(ChatRecord record)
    {
        if(_history.Count >= _maxCount)
            _history.Remove(_history.First());
        this._history.Add(record);
    }
}