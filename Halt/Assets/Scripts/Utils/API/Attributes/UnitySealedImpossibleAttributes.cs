using System;

namespace Mole.Halt.Utils
{
#if UNITY_EDITOR || UNITY_STANDALONE
    [Obsolete("This attribute cannot be used within unity since its intended target is sealed")]
#endif
    public sealed class Serialize : Attribute{ } // cannot extend Unity's sealed SerializeField
}