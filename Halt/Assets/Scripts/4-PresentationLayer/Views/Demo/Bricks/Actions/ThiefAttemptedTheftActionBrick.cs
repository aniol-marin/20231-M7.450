using Mole.Halt.Utils;
using Pada1.BBCore;
using Pada1.BBCore.Framework;

namespace Mole.Halt.PresentationLayer
{
    [Action("Mole/Attempt Theftk")]
    public class ThiefAttemptedTheftActionBrick : BasePrimitiveAction
    {
        [InParam("Owner Id")]
        public int id;
        
        public override void OnStart()
        {
            bool result = RandomValue.Bool(); // TO DO regulate percentage
            new Info($"thieve {id} {(result ? "performed" : "failed")} a theft");
            JusticeController.ReportTheftAttemt(id, result);

            base.OnStart();
        }
        public override void OnEnd()
        {
            base.OnEnd();
        }
    }
}