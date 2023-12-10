using Pada1.BBCore;
using Pada1.BBCore.Framework;
using UnityEngine.AI;

namespace Mole.Halt.PresentationLayer
{
    [Action("Mole/Try to Hide")]
    public class HidingBrick : BasePrimitiveAction
    {
        [InParam("Owner Id")]
        public int id;

        public override void OnStart()
        {
            NavMeshAgent agent = JusticeController.GetAgent(id);
            agent.destination = JusticeController
                .GetPositionOfNearestHideout(id)
                .ToVector3;

            base.OnStart();
        }
        public override void OnEnd()
        {
            base.OnEnd();
        }
    }
}