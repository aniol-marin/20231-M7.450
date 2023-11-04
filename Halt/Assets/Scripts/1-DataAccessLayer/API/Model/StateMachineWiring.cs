using Mole.Halt.DataLayer;
using Mole.Halt.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mole.Halt.DataAccessLayer
{
    [CreateAssetMenu(fileName = "asset-behavior-state-machine", menuName = "Mole/Halt/State machine")]
    public class StateMachineWiring : DataAsset, Wiring
    {
        [Header("Classification")]
        [SerializeField] private PremadeCharacter owner;
        [SerializeField] private CharacterType character;
        [SerializeField] private MoodType mood;
        [Header("Initial state")]
        [SerializeField] private BehaviorType initialBehavior;
        [Header("States")]
        [SerializeField] private List<State> states;

        public CharacterType Character => character;
        public MoodType Mood => mood;
        public BehaviorType InitialBehavior => initialBehavior;
        public IEnumerable<State> States => states;
    }

    [Serializable]
    public struct State
    {
        [SerializeField] private ActivityData activityData;
        public BehaviorType behavior;
        public List<Reaction> reactions;
        public IActivity activity => activityData;
    }
}
