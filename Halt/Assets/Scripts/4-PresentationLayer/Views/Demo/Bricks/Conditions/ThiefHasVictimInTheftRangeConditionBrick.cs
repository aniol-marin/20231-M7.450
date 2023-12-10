using Mole.Halt.Utils;
using Pada1.BBCore;
using Pada1.BBCore.Framework;

namespace Mole.Halt.PresentationLayer
{
    [Condition("Mole/Thief has victim ready")]
    public class ThiefHasVictimInTheftRangeConditionBrick : ConditionBase
    {
        const float THEFT_THRESHOLD = 1f;

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