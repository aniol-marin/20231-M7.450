using Mole.Halt.Utils;
using System.Collections.Generic;

namespace Mole.Halt.GameLogicLayer.Internal
{
    public class LevelLogic : Resettable, Initializable, ILevelLogic
    {
        private List<Puzzle> _puzzles = new();

        public event Callback OnLevelCompleted = delegate { };

        public void Init()
        {
            ResetData();
            CheckForLevelEnd();
        }

        public void Deinit()
        {
        }

        private void CheckForLevelEnd()
        {
            if (_puzzles.TrueForAll(p => p.Solved()))
            {
                OnLevelCompleted();
            }
        }

        public void ResetData()
        {
            _puzzles.Clear();
            SetPuzzles();
        }

        private void SetPuzzles()
        {
            // TO DO
        }
    }
}