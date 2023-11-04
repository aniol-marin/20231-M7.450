using Mole.Halt.GameLogicLayer;

namespace Mole.Halt.PresentationLayer
{
    public class MockEvent : GameEvent
    {
        private string label;

        public MockEvent(string label = null)
        {
            this.label = string.IsNullOrEmpty(label) ? "mock" : label;
        }

        public override string ToString()
        {
            return $"event-{label}";
        }
    }
}