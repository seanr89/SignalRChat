using System;

namespace ChatConsole
{
    /// <summary>
    /// New class to handle dedicated support for input and output events!
    /// </summary>
    public class ChatHandler
    {
        private readonly Func<string> _inputProvider;
        private readonly Action<string> _outputProvider;

        public ChatHandler(Func<string> inputProvider, Action<string> outputProvider)
        {
            _inputProvider = inputProvider;
            _outputProvider = outputProvider;
        }
    }
}