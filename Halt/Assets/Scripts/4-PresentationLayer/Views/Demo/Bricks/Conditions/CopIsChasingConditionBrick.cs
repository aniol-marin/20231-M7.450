using Mole.Halt.Utils;
using Pada1.BBCore;
using Pada1.BBCore.Framework;

namespace Mole.Halt.PresentationLayer
{
    [Condition("Mole/Cop Is Chasing Thief")]
    public class CopIsChasingConditionBrick : ConditionBase
    {
        [InParam("Owner Id")]
        public int id;

        public override bool Check()
        {
            new Error($"cop chasing: {JusticeController.IsCopChasing(id)}");
            return JusticeController.IsCopChasing(id);
        }
    }
}