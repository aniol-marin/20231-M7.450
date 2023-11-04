using System.Collections.Generic;

namespace Mole.Halt.Utils
{
    public delegate bool Verification();
    public delegate bool Verification<T>(T parameter);
    public delegate bool Verification<T, U>(T first, U second);
    // Consider writing a struct instead of adding more parameters to the verifications

    public delegate bool IterableVerification<T>(IEnumerable<T> collection);
}