using Mole.Halt.ApplicationLayer;
using Mole.Halt.GameLogicLayer;
using UnityEngine;

namespace Mole.Halt.Design
{
    [CreateAssetMenu(menuName = "Mole/Design/Events/Mock")]
    public class MockRecord : Record<MockEvent>
    {
        public override GameEvent GameEvent => @event ?? new MockEvent();

        protected override void PopulateInternal(History container) { }
    }
}