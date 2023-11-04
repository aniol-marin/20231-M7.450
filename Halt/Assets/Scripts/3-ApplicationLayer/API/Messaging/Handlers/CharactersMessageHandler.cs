using Mole.Halt.GameLogicLayer;
using Mole.Halt.GameLogicLayer.Internal;
using Mole.Halt.Utils;
using System;

namespace Mole.Halt.ApplicationLayer.Internal
{
    public class CharactersMessageHandler : MessageHandler, Initializable
    {
        [Injected] private readonly IQueueManager queue;

        public MessageHandlerType Type => MessageHandlerType.characters;
        public Type MessageType => typeof(RegistrationEvent);

        public event Callback OnMessageReceived = delegate { };

        public void Init()
        {
            queue.OnEventDequeued += HandleMessage;
        }

        public void Deinit()
        {
            throw new NotImplementedException();
        }

        private void HandleMessage(GameEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}