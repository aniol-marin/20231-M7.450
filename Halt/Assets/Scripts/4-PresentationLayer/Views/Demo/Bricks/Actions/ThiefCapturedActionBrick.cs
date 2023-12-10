using Pada1.BBCore;
using Pada1.BBCore.Framework;

namespace Mole.Halt.PresentationLayer
{
    [Action("Mole/Behave as captured")]
    public class ThiefCapturedActionBrick : BasePrimitiveAction
    {
        [InParam("Owner Id")]
        public int id;
        
        public override void OnStart()
        {
            base.OnStart();
        }
        public override void OnEnd()
        {
            base.OnEnd();
        }
    }
}