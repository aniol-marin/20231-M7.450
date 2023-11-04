using System.Collections.Generic;

namespace Mole.Halt.Utils
{
    public delegate void Callback();
    public delegate void Callback<T>(T parameter);
    public delegate void Callback<T, U>(T first, U second);
    // Consider writing a struct instead of adding more parameters to the callbacks

    public delegate void IterableCallback<T>(IEnumerable<T> collection);
}