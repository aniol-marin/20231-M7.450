using Mole.Halt.Utils;
using Pada1.BBCore;
using Pada1.BBCore.Framework;

namespace Mole.Halt.PresentationLayer
{
    [Condition("Mole/Thief Can Steal")]
    public class ThiefCanStealConditionBrick : ConditionBase
    {
        const float THEFT_THRESHOLD = 10f;

        [InParam("Owner Id")]
        public int id;

        public override bool Check()
        {
            Position self = JusticeController.GetOwnPosition(id);
            Position victim = JusticeController.GetPositionOfNearestVictim(id);

            return self.Distance(victim) < THEFT_THRESHOLD;
        }
    }
}