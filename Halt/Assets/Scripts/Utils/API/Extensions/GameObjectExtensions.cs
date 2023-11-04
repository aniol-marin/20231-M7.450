using System;
using System.Linq;
using UnityEngine;

namespace Mole.Halt.Utils
{
    public static class GameObjectExtensions
    {
        /// <summary>
        /// Non-nullable version of GetComponent<T>().
        /// 
        /// [NOTES]
        /// * for interfaces, use GetSingleInterface 
        /// * for multiple (in children) implement GetAll~ 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="holder">root gameobject</param>
        /// <returns></returns>
        /// <exception cref="Exception">returns a runtime exception if not found (hopefully in development time)</exception>
        public static T Get<T>(this GameObject holder) where T : MonoBehaviour
        {
            return holder
                .GetComponent<T>()
                ?? throw new Exception($"{holder.name} doesn't contain a valid script of type {typeof(T)}");
        }

        public static bool TryGet<T>(this GameObject holder, out T component) where T : MonoBehaviour
        {
            component = holder
                .GetComponent<T>();

            return component != null;
        }

        /// <summary>
        /// Non-nullable version of GetComponent<T>() for interfaces.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="holder"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static T GetInterface<T>(this GameObject holder)
        {
#if UNITY_EDITOR
            if (typeof(T).IsAssignableFrom(typeof(MonoBehaviour)))
            {
                new Warning($"Unnecessarily trying to get interface {typeof(T).Name} through GetInterface<T>() since {typeof(T).Name} is a MonoBehavior implementation. Please consider using Get<T>() instead");
            }
#endif
            return holder
                .GetComponents<MonoBehaviour>()
                .Where(s => s is T)
                .Cast<T>()
                .SingleOrDefault()
                ?? throw new Exception($"{holder.name} doesn't contain a valid interface of type {typeof(T)}");
        }
    }
}