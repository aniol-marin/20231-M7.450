using Mole.Halt.Utils;
using Mole.Halt.Utils.Internal;
using UnityEditor;
using UnityEngine;

namespace Mole.Halt.Editor
{
    [CustomEditor(typeof(MonoCoroutineProvider))]
    public class CoroutineProviderEditor : UnityEditor.Editor
    {
        private static GUIStyle TITLE_STYLE;
        private static GUIStyle TILE_STYLE;
        private MonoCoroutineProvider provider;

        private void Awake()
        {
            TITLE_STYLE = new GUIStyle()
            {
                normal = new GUIStyleState() { textColor = Color.green },
                fontStyle = FontStyle.Bold,
                fontSize = 14,
                alignment = TextAnchor.MiddleCenter,
            };

            TILE_STYLE = new GUIStyle()
            {
                normal = new GUIStyleState() { textColor = Color.grey },
                fontStyle = FontStyle.Normal,
                fontSize = 12,
                alignment = TextAnchor.MiddleCenter,
            };
        }
        private void OnEnable()
        {
            Awake();
            /*
            SerializedObject serialized = serializedObject;
            SerializedProperty property = serialized.FindProperty("activeCoroutines");
            new Warning($"{(property?.name ?? "null")}");
             */

            provider = target as MonoCoroutineProvider;
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (provider.ActiveCoroutines.Empty())
            {
                GUILayout.Label("No active coroutines", TILE_STYLE);
            }
            else
            {
                GUILayout.Label("Active coroutines:", TITLE_STYLE);
                provider
                    .ActiveCoroutines
                    .ForEach(c => GUILayout.Label(c.requester.ToString(), TILE_STYLE));
            }
        }
    }
}