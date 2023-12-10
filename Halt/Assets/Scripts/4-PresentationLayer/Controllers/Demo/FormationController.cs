using Mole.Halt.ApplicationLayer;
using Mole.Halt.Utils;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace Mole.Halt.PresentationLayer
{
    public class FormationController : ControllerNode
    {
        [SerializeField] TrainerView trainer;
        [SerializeField] TraineeView[] trainees;
        [SerializeField] float formationSize;
        [SerializeField] float tolerance;
        Vector3 showCenter;
        Vector3 formationCenter;
        Position cachedPosition;

        public override void Deinit() { }

        private void OnValidate()
        {
            Assert.IsNotNull(trainer);
            Assert.IsTrue(trainees.Any());
            Assert.IsFalse(trainees.Any(t => t == null));
        }

        private void Awake()
        {
            showCenter = trainer.transform.position;
            formationCenter = trainees
                .Select(t => t.transform.position)
                .Aggregate((p1, p2) => p1 + p2)
                 / trainees.Length;

            int slot1DSize = (int)Math.Ceiling(Math.Sqrt(trainees.Length));
            int activeSlot = 0;
            for (int x = 0; x < slot1DSize && activeSlot < trainees.Length; x++)
            {
                for (int y = 0; y < slot1DSize && activeSlot < trainees.Length; y++)
                {
                    float v = -slot1DSize / 2f + x * slot1DSize;
                    float h = -slot1DSize / 2f + y * slot1DSize;

                    trainees[activeSlot].SetOffset(formationCenter - showCenter + new Vector3(h, 0, v));

                    activeSlot++;
                }
            }
        }

        private void Start()
        {
            UpdateTargets();
        }

        private void Update()
        {
            if (trainer.GetPosition().Distance(cachedPosition) > tolerance)
            {
                UpdateTargets();
            }
        }

        void UpdateTargets()
        {
            Vector3 position = trainer.GetPosition().ToVector3;
            Quaternion rotation = trainer.transform.rotation;

            trainees
                .ForEach(t => t.SetNewTarget(position, rotation));

            cachedPosition = trainer.GetPosition();
        }
    }
}