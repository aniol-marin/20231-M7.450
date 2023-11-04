using Mole.Halt.Utils;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Mole.Halt.Design
{
    [CreateAssetMenu(menuName = "Mole/Design/History")]
    public class History : DataAsset
    {
        [SerializeField] private GameMessageWrapper pushable;
        [SerializeField] private List<GameMessageWrapper> pastEvents;
        [SerializeField] private List<EntityWrapper> pastEntities;

        public IEnumerable<GameMessageWrapper> PastEvents => pastEvents;

        public void Add(GameMessageWrapper message)
        {
            GameMessageWrapper instance = CreateInstance(message.GetType()) as GameMessageWrapper;
            JsonUtility.FromJsonOverwrite(JsonUtility.ToJson(message), instance);
            instance.Rename($"entry-{message.GameEvent}");
            instance.Store(this);

            pastEvents.Add(instance);

            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
        }

        public void Add(EntityWrapper entity)
        {
            EntityWrapper instance = CreateInstance(entity.GetType()) as EntityWrapper;
            JsonUtility.FromJsonOverwrite(JsonUtility.ToJson(entity), instance);
            instance.Rename($"entity-{entity.Entity}");
            instance.Store(this);

            pastEntities.Add(instance);

            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
        }

        public void Push()
        {
            Add(pushable);
        }

        public GameMessageWrapper GetStep(int step)
        {
            return pastEvents
                .Skip(step)
                .FirstOrDefault();
        }

        public void Prune()
        {
            GetAllEntries()
                .Except(pastEvents)
                .ForEach(w => AssetDatabase.RemoveObjectFromAsset(w));

            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
        }

        public void Clear()
        {
            pastEvents.Clear();

            GetAllEntries()
                .ForEach(e => AssetDatabase.RemoveObjectFromAsset(e));
        }

        private IEnumerable<Object> GetAllEntries()
        {

            return AssetDatabase
                .LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(this))
                .Where(o => o is GameMessageWrapper || o is EntityWrapper);
        }
    }
}