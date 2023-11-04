namespace Mole.Halt.Utils
{
    public interface Allocator
    {
        T Instantiate<T>();
    }
}