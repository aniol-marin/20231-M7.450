using Mole.Halt.DataAccessLayer;
using System;
using UnityEngine;

namespace Mole.Halt.ApplicationLayer
{
    [Serializable]
    public class InitialCharacterData
    {
        [SerializeField] private PremadeCharacter prototype;
        [SerializeField] private CharacterFilter filter;
        [SerializeField] private Vector3 initialPosition;
        [SerializeField] private Quaternion initialRotation;

        public PremadeCharacter Prototype { get => prototype; set => prototype = value; }
        public Vector3 InitialPosition { get => initialPosition; set => initialPosition = value; }
        public Quaternion InitialRotation { get => initialRotation; set => initialRotation = value; }
        public CharacterFilter Filter { get => filter; set => filter = value; }
        public float Speed => filter.Speed;
    }
}