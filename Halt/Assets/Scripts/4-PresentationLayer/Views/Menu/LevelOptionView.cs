using Mole.Halt.ApplicationLayer;
using Mole.Halt.ApplicationLayer.Internal;
using Mole.Halt.Utils;

namespace Mole.Halt.PresentationLayer
{
    public class LevelOptionView : GenericButtonHelper<LevelFilter>
    {
        public void Init(LevelFilter data, Callback<LevelFilter> onSelected)
        {
            button.onClick.AddListener(() => onSelected(data));
        }
    }
}