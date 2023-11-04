using System;

namespace Mole.Halt.ApplicationLayer.Internal
{
    public readonly struct LevelFilter
    {
        public readonly LevelNumber number;
        public readonly LevelCategory category;
        public readonly LevelSource source;

        public LevelFilter(LevelNumber number, LevelCategory category, LevelSource source = default)
        {
            this.number = number;
            this.category = category;
            this.source = source;
        }

        public override bool Equals(object obj)
        {
            return obj.Equals(this) ||
                (obj is LevelFilter filter && this == filter);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(number, category);
        }

        public static bool operator !=(LevelFilter left, LevelFilter right) => !(left == right);
        public static bool operator ==(LevelFilter left, LevelFilter right)
        {
            return left.category == right.category
                && left.number == right.number;
        }

        public override string ToString()
        {
            return $"[{category}-{(int)number}]({source})";
        }
    }

    public enum LevelNumber : uint
    {
        noLevelSelected = default,
        lvl1 = 1,
        lvl2 = 2,
        lvl3 = 3,
        lvl4 = 4,
        lvl5 = 5,
        //extend as needed
    }

    public enum LevelCategory : uint
    {
        uncategorized = default,
        PAC = 1,
    }

    public enum LevelSource : int
    {
        levelDesign = default,
        builtIn = 1,
        dlc = 2,
        upgrade = 3,
    }
}