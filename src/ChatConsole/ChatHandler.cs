

using System;
using System.Threading.Tasks;

namespace ChatConsole
{
    /// <summary>
    /// New chat handler class to support input and output events in app for signal r
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