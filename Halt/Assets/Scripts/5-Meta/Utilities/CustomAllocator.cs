using Mole.Halt.Utils;
using Zenject;


namespace Mole.Halt.Meta
{
    public class CustomAllocator : Allocator
    {
        [Injected] private readonly DiContainer container;
        //[Injected] private readonly SceneInitializer initializer; // TODO migrate initializer as util

        public T Instantiate<T>()
        {
            T instance = container.Instantiate<T>();

            /*
            if (instance is Initializable i)
            {
                initializer.TryInitialize(i);
            }
             */

            return instance;
        }
    }
}