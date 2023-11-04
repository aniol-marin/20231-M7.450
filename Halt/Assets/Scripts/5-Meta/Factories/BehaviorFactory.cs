using Mole.Halt.ApplicationLayer.Internal;
using Mole.Halt.DataAccessLayer;
using Mole.Halt.DataAccessLayer.Internal;
using Mole.Halt.DataLayer;
using Mole.Halt.GameLogicLayer;
using Mole.Halt.GameLogicLayer.Internal;
using Mole.Halt.Utils;
using System.Collections.Generic;
using System.Linq;
using Zenject;


namespace Mole.Halt.Meta
{
    /// <summary>
    /// Provides dependency at runtime (given that the implementation values are serialized in config)
    /// </summary>
    public class BehaviorFactory : IBehaviorFactory
    {
        [Injected] private readonly DiContainer container;
        [Injected] private readonly Wirings wirings;

        BehaviorManager Mock => container.ResolveId<BehaviorManager>(ControllerType.mock);

        public BehaviorManager Get(ControllerType managerType)
        {
            return container
                // TO DO optimize once mock is not necessary any longer
                .ResolveIdAll<BehaviorManager>(managerType)
                .FirstOrDefault(_ => true, Mock);
        }

        public StateMachineWiring GetWiring(Character character)
        {
            return wirings
                .Data
                .Where(w => w.Character == character.CharacterType)
                .Mirror(out IEnumerable<StateMachineWiring> self)
                .FirstOrDefault(w => w.Mood == character.Mood, self.First());
        }
    }
}