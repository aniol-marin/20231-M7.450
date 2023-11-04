using Mole.Halt.DataAccessLayer;
using Mole.Halt.Utils;
using UnityEditor;
using UnityEngine;

namespace Mole.Halt.Editor
{
    [CustomEditor(typeof(ObjectRepository))]
    public class ObjectRepositoryEditor : UnityEditor.Editor
    {
        private static GUIStyle TITLE_STYLE;
        private static GUIStyle TILE_STYLE;
        ObjectRepository repository;

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
#pragma warning disable CS0618 // intended usage
        private void OnEnable()
        {
            Awake();
            /*
            SerializedObject serialized = serializedObject;
            SerializedProperty property = serialized.FindProperty("activeCoroutines");
            new Warning($"{(property?.name ?? "null")}");
             */

            repository = target as ObjectRepository;
            repository.OnRepositoryUpdated += HandleUpdate;

        }

        private void OnDisable()
        {
            repository.OnRepositoryUpdated -= HandleUpdate;
        }

        private void HandleUpdate()
        {
            Repaint();
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (repository == null || repository.GetAll.Empty())
            {
                GUILayout.Label("No active entities", TILE_STYLE);
            }
            else
            {
                GUILayout.Label("Active entities:", TITLE_STYLE);
                repository.GetAll
                    .ForEach(c => GUILayout.Label(c.ToString(), TILE_STYLE));
            }
        }
#pragma warning restore CS0618 // Type or member is obsolete
    }
}