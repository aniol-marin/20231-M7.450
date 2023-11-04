using Mole.Halt.DataLayer;
using System.Collections.Generic;

namespace Mole.Halt.DataAccessLayer
{
    public interface Wiring
    {
        CharacterType Character { get; }
        BehaviorType InitialBehavior { get; }
        MoodType Mood { get; }
        IEnumerable<State> States { get; }
    }
}