using Mole.Halt.ApplicationLayer;
using Mole.Halt.GameLogicLayer;
using Mole.Halt.GameLogicLayer.Internal;
using Mole.Halt.Utils;
using UnityEngine;

namespace Mole.Halt.Design
{
    [LevelDesign]
    public class QueueToHistoryService : NodeClass, Initializable
    {
        [Injected] private readonly IQueueManager queue;
        [SerializeField] private History history;

        public void Init()
        {
            history.Clear();
            queue.OnEventDequeued += HandleMessage;
        }


        public void Deinit()
        {
        }
        private void HandleMessage(GameEvent @event)
        {
            GameMessageWrapper wrapper;

            switch (@event.GetType().Name)
            {
                case nameof(MockEvent):
                    wrapper = ScriptableObject.CreateInstance<MockRecord>();
                    break;
                case nameof(OrderEvent):
                    OrderRecord order = ScriptableObject.CreateInstance<OrderRecord>();
                    order.Populate(@event as OrderEvent, history);
                    wrapper = order;
                    break;
                case nameof(InteractionEvent):
                    InteractionRecord interaction = ScriptableObject.CreateInstance<InteractionRecord>();
                    interaction.Populate(@event as InteractionEvent, history);
                    wrapper = interaction;
                    break;
                case nameof(RegistrationEvent):
                    RegistrationRecord registration = ScriptableObject.CreateInstance<RegistrationRecord>();
                    registration.Populate(@event as RegistrationEvent, history);
                    wrapper = registration;
                    break;
                default:
                    new Error($"wrapper for type {@event.GetType().Name} not implemented, history will be ignored");
                    return;
            };

            history.Add(wrapper);
        }
    }
}