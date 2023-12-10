using Pada1.BBCore;
using Pada1.BBCore.Framework;

namespace Mole.Halt.PresentationLayer
{
    [Condition("Mole/Thief is captured")]
    public class ThiefIsCapturedConditionBrick : ConditionBase
    {
        [InParam("Owner Id")]
        public int id;

        [InParam("Debug")]
        public bool debug;

        public override bool Check()
        {
            return debug;
        }
    }
}