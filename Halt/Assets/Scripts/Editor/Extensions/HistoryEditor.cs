using Mole.Halt.Design;
using UnityEditor;
using UnityEngine;

namespace Mole.Halt.Editor
{
    [CustomEditor(typeof(History))]
    public class HistoryEditor : UnityEditor.Editor
    {
        private History history;

        private void OnEnable()
        {
            history = target as History;
        }

        public override void OnInspectorGUI()
        {
            Clear();
            Prune();

            base.OnInspectorGUI();
        }

        private void Clear()
        {
            if (GUILayout.Button("Wipe"))
            {
                history.Clear();
            }
        }

        private void Prune()
        {
            if (GUILayout.Button("Prune"))
            {
                history.Prune();
            }
        }
    }
}