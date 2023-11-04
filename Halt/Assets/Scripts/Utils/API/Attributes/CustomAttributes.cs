using System;
using System.Diagnostics;

namespace Mole.Halt.Utils
{
    public sealed class InjectedAttribute : Zenject.InjectAttribute { }


    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public sealed class SerializedAttribute : Attribute
    {
        ///Intended to replace Unity intra-asset serialization
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public sealed class MonitoredAttribute : Attribute
    {
        ///Intended to replace Unity GUI visibility (but not serialization)
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class LevelDesignAttribute : Attribute
    {
        ///Intended to label design assets
    }

    [Conditional("DEBUG")]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public sealed class MockedAttribute : Attribute
    {
#if !UNITY_EDITOR   // TO DO refine assembly definitions
        [Obsolete("Mock being used in build")]
#endif
        public MockedAttribute()
        {
        }
    }
}