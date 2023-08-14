using System;
using UnityEngine;

namespace Mole.Halt.Utils
{
    /// <summary>
    /// Casting utility. Avoids direct unhandled GetComponent<>() usage.
    /// </summary>
    public class Caster : MonoBehaviour
    { 
        /// <summary>
        /// Generic method. Returns the requested Monobehavior subclass (or its interfaces) from the given MonoBehaviour transform. Throws an exception if not found.
        /// 
        /// TODO
        /// - Overcharge for other types (i.e. GameObject)
        /// - Propagate to children
        /// - The sky is the limit
        /// </summary>
        /// <typeparam name="T">the type to be returned</typeparam>
        /// <param name="holder">the transform that should contain the type T</param>
        /// <returns></returns>
        static public T GetType<T>(Transform holder)
        {
            T component = holder.GetComponent<T>();

            if (component == null)
            {
                throw new Exception($"transform {holder.name} doesn't contain a valid element of type {typeof(T)}");
            }

            return component;
        }
    }
}