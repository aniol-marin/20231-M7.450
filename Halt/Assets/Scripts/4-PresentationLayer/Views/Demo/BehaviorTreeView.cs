using Mole.Halt.ApplicationLayer;
using Mole.Halt.Utils;
using Pada1.BBCore;
using UnityEngine;

namespace Mole.Halt.PresentationLayer
{
    public class BehaviorTreeView : BehaviorExecutor
    {
        [InParam("PostOffice"), Injected] public PostOffice postOffice;
        [SerializeField] BehaviorTreeData data;

        public PostOffice PostOffice => postOffice;
    }
}