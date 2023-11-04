using Mole.Halt.Utils;
using System;

namespace Mole.Halt.ApplicationLayer
{
    public interface MessageHandler
    {
        MessageHandlerType Type { get; }
        Type MessageType { get; }

        event Callback OnMessageReceived;
    }
}