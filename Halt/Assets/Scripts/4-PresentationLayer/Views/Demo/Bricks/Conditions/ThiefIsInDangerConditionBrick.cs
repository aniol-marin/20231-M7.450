using Pada1.BBCore;
using Pada1.BBCore.Framework;

namespace Mole.Halt.PresentationLayer
{
    [Condition("Mole/Thief Is In Danger")]
    public class ThiefIsInDangerConditionBrick : ConditionBase
    {
        [InParam("Owner Id")]
        public int id;

        public override bool Check()
        {
            return JusticeController.IsThiefBeingChased(id)
                || JusticeController.IsThiefInCooldown(id);
        }
    }
}