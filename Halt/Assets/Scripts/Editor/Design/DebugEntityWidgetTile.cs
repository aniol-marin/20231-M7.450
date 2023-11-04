using Mole.Halt.ApplicationLayer;
using Mole.Halt.DataLayer;
using Mole.Halt.Utils;
using TMPro;
using UnityEngine;

namespace Mole.Halt.Design
{
    [LevelDesign]
    public class DebugEntityWidgetTile : NodeClass
    {
        [Injected] private readonly CameraProvider cameras;
        [SerializeField] private RectTransform widget;
        [SerializeField] private TMP_Text display;
        private NodeClass target;


        public void Init(EntityId entity, ViewNode view)
        {
            display.text = entity.ToString();
            target = view;
        }

        private void Update()
        {
            widget.position = cameras.ActiveCamera.WorldToScreenPoint(target.GetPosition().ToVector3);
        }

    }
}