

using System;
using System.Threading.Tasks;

namespace ChatConsole
{
    /// <summary>
    /// Handler class to support input and output events in app for signalr server
    /// can support azure and local based implementation
    /// </summary>
    public class ChatHandler
    {
        private readonly Func<string> _inputProvider;
        private readonly Action<string> _outputProvider;

        /// <summary>
        /// Primart constructor to control the console input and output events from the console!
        /// </summary>
        /// <param name="inputProvider"></param>
        /// <param name="outputProvider"></param>
        public ChatHandler(Func<string> inputProvider, Action<string> outputProvider)
        {
            _inputProvider = inputProvider;
            _outputProvider = outputProvider;
        }
    }
}