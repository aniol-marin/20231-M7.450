using Mole.Halt.DataLayer;
using Mole.Halt.Utils;

namespace Mole.Halt.GameLogicLayer.Internal
{
    public class UserBehaviorManager : BehaviorManager, Initializable
    {
        [Injected] private readonly InteractionManager interactions;
        private EntityId id;

        public void Init() { }
        public void Deinit() { }

        public void Initialize(Character character)
        {
            id = character.Id;

            interactions.OnInteractionReported += HandleInteraction;
        }

        private void HandleInteraction(EntityId id, Entity target)
        {
            if (this.id == id)
            {
                new Info($"User-controlled unit detected relevant interaction: {id} - {target}");
            }
        }

    }
}
