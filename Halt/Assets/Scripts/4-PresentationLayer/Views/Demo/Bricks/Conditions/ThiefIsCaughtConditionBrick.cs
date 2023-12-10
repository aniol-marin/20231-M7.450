using Pada1.BBCore;
using Pada1.BBCore.Framework;

namespace Mole.Halt.PresentationLayer
{
    [Condition("Mole/Thief Is Caught")]
    public class ThiefIsCaughtConditionBrick : ConditionBase
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