using System;
using System.Collections.Generic;
using System.Linq;

namespace Mole.Halt.Utils.Extensions
{
    public class StructToClass<T> where T : struct
    {
        public readonly object Content;

        public StructToClass(object content)
        {
            Content = content;
        }
    }
    public static class IEnumerableExtensions
    {

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action) where T : class
        {
            enumerable.ToList().ForEach(action);
            //enumerable.ForEachWithoutIndex(action);
        }

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T, int> action) where T : class
        {
            IntRef index = new IntRef(0);
            enumerable.ForEachWithIndex(action, index);
        }

        public static bool Empty<T>(this IEnumerable<T> e) where T : class
        {
            return e.Count() == 0;
        }

        public static IEnumerable<T> ForEachWithoutIndex<T>(this IEnumerable<T> enumerable, Action<T> action) where T : class
        {
            yield return enumerable.Skip(1) as T;

            T element = enumerable
                  .FirstOrDefault();

            if (element != null)
            {
                action(element);
            }
        }

        public static IEnumerable<T> ForEachWithIndex<T>(this IEnumerable<T> enumerable, Action<T, int> action, IntRef index) where T : class
        {
            int i = index.Value++;
            yield return enumerable.Skip(1) as T;

            T element = enumerable
                  .FirstOrDefault();

            if (element != null)
            {
                action(element, i);
            }
        }

        public class IntRef
        {
            public int Value;
            public IntRef(int value)
            {
                Value = value;
            }
        }
    }
}