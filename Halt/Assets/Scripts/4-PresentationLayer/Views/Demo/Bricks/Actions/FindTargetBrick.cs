using Mole.Halt.Utils;
using Pada1.BBCore;
using Pada1.BBCore.Framework;
using System.Linq;
using UnityEngine;

namespace Mole.Halt.PresentationLayer
{
    [Action("Mole/Find Target")]
    public class FindTargetBrick : BasePrimitiveAction
    {
        [InParam("Target Tags")]
        public string[] targets;

        [InParam("Candidate Tags")]
        public GameObject[] candidates;

        [OutParam("Found Target")]
        public GameObject target;

        public override void OnStart()
        {
            base.OnStart();
            new Info("thief seeking for targets");

            target = candidates
                .FirstOrDefault(c => targets.Any(t => c.CompareTag(t)));
        }
        public override void OnEnd()
        {
            new Info("thief seeking ended");
            base.OnEnd();
        }
    }
}