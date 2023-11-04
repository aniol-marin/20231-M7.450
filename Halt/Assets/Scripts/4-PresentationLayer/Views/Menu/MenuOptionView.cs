using Mole.Halt.ApplicationLayer;
using Mole.Halt.Utils;

namespace Mole.Halt.PresentationLayer
{
    public class MenuOptionView : GenericButtonHelper<DummyStruct>
    {
        public void Init(Callback onSelected)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => onSelected());
        }
    }
}