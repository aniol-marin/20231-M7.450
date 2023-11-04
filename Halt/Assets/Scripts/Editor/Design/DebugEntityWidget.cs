using Mole.Halt.ApplicationLayer;
using Mole.Halt.DataLayer;
using Mole.Halt.GameLogicLayer;
using Mole.Halt.GameLogicLayer.Internal;
using Mole.Halt.PresentationLayer;
using Mole.Halt.Utils;
using System.Collections.Generic;

namespace Mole.Halt.Design
{
    [LevelDesign]
    public class DebugEntityWidget : NodeClass, Initializable
    {
        [Injected] private readonly IQueueManager queue;
        [Injected] private readonly ICensusManager census;
        [Injected] private readonly EntityMappingService mapping;
        [Injected] private readonly IPrefabFactory prefabs;
        [Injected] private readonly IUserInputService input;
        private readonly List<DebugEntityWidgetTile> tiles = new();


        public void Init()
        {
            census.OnCharacterRegistered += HandleEvent;
            input.OnToggleConsole += ToggleVisuals;
        }


        public void Deinit()
        {
        }

        private void HandleEvent(EntityId id)
        {
            DebugEntityWidgetTile tile = prefabs.Instantiate<DebugEntityWidgetTile>(transform);
            tile.Init(id, mapping.GetView(id));
            tiles.Add(tile);
        }
    }
}
