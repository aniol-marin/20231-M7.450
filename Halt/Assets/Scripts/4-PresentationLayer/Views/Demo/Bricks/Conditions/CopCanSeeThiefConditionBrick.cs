using Mole.Halt.Utils;
using Pada1.BBCore;
using Pada1.BBCore.Framework;

namespace Mole.Halt.PresentationLayer
{
    [Condition("Mole/Cop Can See Thief")]
    public class CopCanSeeThiefConditionBrick : ConditionBase
    {
        const float VISION_DISTANCE = 5;

        [InParam("Owner Id")]
        public int id;

        public override bool Check()
        {
            return JusticeController.IsChasedThiefHidden(id)
                && JusticeController
                .GetPositionOfChasedThief(id)
                .Distance(JusticeController.GetOwnPosition(id)) < VISION_DISTANCE;
        }
    }
}