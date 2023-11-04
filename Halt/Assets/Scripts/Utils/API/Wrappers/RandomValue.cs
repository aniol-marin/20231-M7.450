namespace Mole.Halt.Utils
{
    public static class RandomValue
    {
        public static int Int(int min = 0, int max = int.MaxValue) => UnityEngine.Random.Range(min, max);
        public static float Float(float min = 0, float max = int.MaxValue) => UnityEngine.Random.Range(min, max);
    }
}