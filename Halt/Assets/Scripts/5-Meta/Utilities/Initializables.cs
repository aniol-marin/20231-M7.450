using Mole.Halt.ApplicationLayer.Internal;
using Mole.Halt.Utils;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;


namespace Mole.Halt.Meta
{
    public class Initializables : InitializablesProvider
    {
        [Injected] private readonly DiContainer diContainer;

        public IEnumerable<Initializable> FetchCompositionTree()
        {
            return diContainer
                .AllContracts
                .Where(i => i.Type.FullName.Contains("Mole")) // narrows down to specific targets
                .Select(t => diContainer.ResolveAll(t.Type)) // generates a LIST of implementations
                .SelectEachWhere(c => c.Count > 0, l => l[0]) // selects each element of the lists
                .Where(c => c is  Initializable)
                .Cast<Initializable>()
                .Distinct();
        }

        public IEnumerable<Initializable> FetchNodeTree(NodeClass root)
        {
            return root
                .transform
                .GetComponentsInChildren<MonoBehaviour>(includeInactive: true)
                .Where(s => s is Initializable)
                .Cast<Initializable>();
        }
    }
}