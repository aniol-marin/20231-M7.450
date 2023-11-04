using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Mole.Halt.ApplicationLayer
{
    /// <summary>
    /// Dummy struct offered as a placeholder 
    /// until a meaningful struct is needed for a given implementation
    /// (also as a pseudo-default while it is not yet used)
    /// </summary>
    public struct DummyStruct { }

    /// <summary>
    /// Helps implementing the common behavior of a 
    /// </summary>
    /// <typeparam name="T">an identifier data type (to be implemented)</typeparam>
    public class GenericButtonHelper<T> : ViewNode
    where T : struct
    {
        [SerializeField] protected Button button;
        [SerializeField] private TMP_Text label;
        //[SerializeField] private LocalizeStringEvent localization;


        //[Obsolete("Unlocalized labeling is deprecated. Please use localization instead")]
        public void ChangeLabel(string label)
        {
            this.label.text = label;
        }

        /*
        public void UpdateLabel(TableReference table, TableEntryReference entry)
        {
            throw new NotImplementedException();
            TableReference table = new();
            TableEntryReference entry = new();
            string reference = entry.ResolveKeyName(null);
            localization.StringReference.SetReference(table, reference);
        }
         */
        public void ToggleVisibility(bool visible)
        {
            gameObject.SetActive(visible);
        }
    }
}