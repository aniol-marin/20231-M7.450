using Mole.Halt.PresentationLayer;
using UnityEditor;
using UnityEngine;

namespace Mole.Halt.Editor
{
    [CustomEditor(typeof(PathView))]
    public class PathViewEditor : UnityEditor.Editor
    {

        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Fetch tiles"))
            {
                PathView view = target as PathView;
                view.FetchTiles();
                EditorUtility.SetDirty(view);
            }

            base.OnInspectorGUI();
        }
    }
}