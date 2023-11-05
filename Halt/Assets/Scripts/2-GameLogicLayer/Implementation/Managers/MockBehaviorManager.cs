using Mole.Halt.DataLayer;
using Mole.Halt.Utils;

namespace Mole.Halt.GameLogicLayer.Internal
{
    public class MockBehaviorManager : BehaviorManager, Initializable
    {
        [Injected] private readonly Allocator allocator;
        [Injected] private readonly InteractionManager interactions;
        private EntityId id;
        private Wander wander;

        public void Init() { }
        public void Deinit()
        {
            wander.Stop();
        }

        public void Initialize(Character character)
        {
            id = character.Id;

            interactions.OnInteractionReported += HandleInteraction;

            wander = allocator.Instantiate<Wander>();
            wander.Initialize(id);
            wander.Start();
        }

        private void HandleInteraction(EntityId id, Entity target)
        {
            if (this.id == id)
            {
                new Info($"Mock detected relevant interaction: {id} - {target}");
            }
        }

    }
}
