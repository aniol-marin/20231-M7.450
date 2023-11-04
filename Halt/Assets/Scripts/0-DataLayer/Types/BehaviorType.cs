namespace Mole.Halt.DataLayer
{
    public enum BehaviorType
    {
        mock = -1,
        Idle = default,
        Wandering = 1,
        Strolling = 2,
        Jogging = 3,
        Resting = 4,
        Scanning = 5,
        Fleeing = 6,
        Attacking = 7,
        Chasing = 8,
        Scouting = 9,
    }
}