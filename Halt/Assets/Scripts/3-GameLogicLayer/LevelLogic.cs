using Mole.Halt.ApplicationLayer;
using Mole.Halt.Utils;
using Mole.Halt.Utils.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mole.Halt.GameLogicLayer.Levels
{
    /// <summary>
    /// Basic level logic handler.
    /// 
    /// Calls for end of level upon completion of all the puzzles / challenges.
    /// NOTE: At least one challenge is required. Set an end-level checkpoint at least.
    /// </summary>
    public class LevelLogic : MonoBehaviour, Resettable
    {
        [SerializeField] List<Transform> _requiredPuzzles = null;
        [SerializeField] bool sandboxLevel = false;
        private List<Puzzle> _puzzles = null;

        private void Awake()
        {
            ResetData();
        }
        void Start()
        {
            AttachListeners();
        }

        private void CheckForLevelEnd()
        {
            if (_puzzles.TrueForAll(p => p.Solved() == true))
            {
                Debug.Log("All the puzzles solved");
            }
            else
            {
                Debug.Log("Puzzle solved. Still some puzzles left in the level");
            }
        }
        /// <summary>
        /// Resettable implementation
        /// </summary>
        public void ResetData()
        {
            if (_puzzles == null)
            {
                _puzzles = new List<Puzzle>();
            }
            else
            {
                _puzzles.Clear();
            }

            StartCoroutine(SetPuzzles()); // workaround. Waits for the IResetter to finish all the other relevant resets.

        }
        /// <summary>
        /// Sets the puzzles for the current level. Throws an exception if there are none, given that would be an endless level.
        /// 
        /// TODO
        /// - implement sandbox mode
        /// </summary>
        /// <returns></returns>
        private IEnumerator SetPuzzles()
        {
            yield return new WaitForEndOfFrame();

            Puzzle item;
            foreach (Transform puzzle in _requiredPuzzles)
            {
                item = Caster.GetType<Puzzle>(puzzle);

                if (item.HasKey())
                {
                    //_API.RequiredCollectibles.Add(item.Key());
                }
                _puzzles.Add(item);
            }
            if (!sandboxLevel && _puzzles.Empty())
            {
                throw new Exception("Tried to load an endless level");
            }
            yield return null;
        }
        /// <summary>
        /// IResetter implementation
        /// </summary>
        public void ClearListeners()
        {
            // do nothing
        }
        /// <summary>
        /// IResetter implementation
        /// </summary>
        public void AttachListeners()
        {
            //_API.AddListenerToPuzzleSolved(CheckForLevelEnd);
        }

        public void AttachToReset()
        {
            throw new NotImplementedException();
        }
    }
}