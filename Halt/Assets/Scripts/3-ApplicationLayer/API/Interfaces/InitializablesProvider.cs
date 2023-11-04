using Mole.Halt.Utils;
using System.Collections.Generic;

namespace Mole.Halt.ApplicationLayer.Internal
{
    public interface InitializablesProvider
    {
        public IEnumerable<Initializable> FetchCompositionTree();
        public IEnumerable<Initializable> FetchNodeTree(NodeClass root);
    }
}