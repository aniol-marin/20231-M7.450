using Mole.Halt.Utils;
using Pada1.BBCore;
using Pada1.BBCore.Framework;
using System.Threading.Tasks;
using UnityEngine.AI;

namespace Mole.Halt.PresentationLayer
{
    [Action("Mole/Approach Victim")]
    public class ThiefApproachVictimActionBrick : BasePrimitiveAction
    {
        [InParam("Owner Id")]
        public int id;

        public override void OnStart()
        {
            CharacterView victim = JusticeController.GetNearestVictim(id);
            JusticeController.AttemptTheft(id, victim);

            base.OnStart();
        }
    }
}