using System;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Tables;
using UnityEngine.UI;

namespace Mole.Halt.PresentationLayer.Views
{
    public class MenuOptionView : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private TMP_Text label;
        [SerializeField] private LocalizeStringEvent localization;

        public void Init(Action onSelected)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => onSelected());
        }

        public void UpdateLabel(TableReference table, TableEntryReference entry)
        {
            throw new NotImplementedException();
            /*
            TableReference table = new();
            TableEntryReference entry = new();
            string reference = entry.ResolveKeyName(null);
            localization.StringReference.SetReference(table, reference);
             */
        }

        //[Obsolete("Unlocalized labeling is deprecated. Please use localization")]
        public void ChangeLabel(string label)
        {
            this.label.text = label;
        }

        public void ToggleVisibility(bool visible)
        {
            gameObject.SetActive(visible);
        }
    }
}