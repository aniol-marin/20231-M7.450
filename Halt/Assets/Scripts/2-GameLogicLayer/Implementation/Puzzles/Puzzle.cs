namespace Mole.Halt.GameLogicLayer
{
    /// <summary>
    /// Interface for managing logic puzzlies.
    /// 
    /// NOTE: Puzzles without a key may always return null Collectible data
    /// </summary>
    public interface Puzzle
    {
        public bool Solved();
        public bool HasKey();
    }
}