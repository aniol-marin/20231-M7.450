using System;
using System.Collections.Generic;
using System.Linq;

namespace Mole.Halt.Utils
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
        public static T DequeueFirst<T>(this IEnumerable<T> collection, out IEnumerable<T> remnant)
        {
            remnant = collection.Skip(1);

            return collection.FirstOrDefault();
        }
        public static IEnumerable<T> Debug<T>(this IEnumerable<T> source, Callback<T> message)
        {
            foreach (T item in source)
            {
                message(item);
            }

            return source;
        }

        public static IEnumerable<T> SelectEachWhere<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            foreach (T item in source)
            {
                if (item is IEnumerable<T> subsource)
                {
                    foreach (T subitem in subsource)
                    {
                        if (predicate(subitem))
                        {
                            yield return subitem;
                        }
                    }
                }
                else
                {
                    if (predicate(item))
                    {
                        yield return item;
                    }
                }
            }
            yield break;
        }

        public static IEnumerable<U> SelectEachWhere<T, U>(this IEnumerable<T> source, Func<T, bool> predicate, Func<T, U> extraction)
        {
            foreach (T item in source)
            {
                if (item is IEnumerable<T> subsource)
                {
                    foreach (T subitem in subsource)
                    {
                        if (predicate(subitem))
                        {
                            yield return extraction(subitem);
                        }
                    }
                }
                else
                {
                    if (predicate(item))
                    {
                        yield return extraction(item);
                    }
                }
            }
            yield break;
        }

        public static IEnumerable<T> Mirror<T>(this IEnumerable<T> source, out IEnumerable<T> mirrored)
        {
            mirrored = source;
            return source;
        }

        public static IEnumerable<T> Extract<T, U>(this IEnumerable<T> source, Func<T, U> predicate, out IEnumerable<U> extraction)
        {
            extraction = source.Select(predicate);
            return source;
        }

        public static IEnumerable<T> WhereAndExtractWhereNot<T>(this IEnumerable<T> source, Func<T, bool> predicate, out IEnumerable<T> extraction)
        {
            extraction = source.Where(predicate);
            return source.Except(extraction);
        }

        public static IEnumerable<T> SideEffect<T>(this IEnumerable<T> source, Callback<T> predicate)
        {
            foreach (T item in source)
            {
                predicate(item);
                yield return item;
            }

            yield break;
        }

        public static T SingleSideEffect<T>(this T item, Callback<T> predicate)
        {
            predicate(item);

            return item;
        }

        public static IEnumerable<T> Store<T>(this IEnumerable<T> source, Callback<IEnumerable<T>> destination)
        {
            destination(source);
            return source;
        }

        public static T Store<T>(this T source, out T destination)
        {
            destination = source;
            return source;
        }

        public static T Extract<T, U>(this T source, Func<T, U> predicate, out U extraction)
        {
            extraction = predicate(source);
            return source;
        }

        public static IEnumerable<Tuple<T, U>> Join<T, U>(this IEnumerable<T> source, Func<T, U> predicate)
        {
            foreach (T item in source)
            {
                yield return new Tuple<T, U>(item, predicate(item));
            }

            yield break;
        }

        public static T FirstOrDefault<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate, T defaultElement) where T : class
        {
            return enumerable.FirstOrDefault(predicate) ?? defaultElement;
        }

        public static T FirstOrDefault<T>(this IEnumerable<T> enumerable, T defaultElement) where T : class
        {
            return enumerable.FirstOrDefault() ?? defaultElement;
        }

        public static void ForEach<T>(this IEnumerable<T> enumerable, Callback<T> action)
        {
            if (enumerable == null)
            {
                new Warning($"Foreach<{typeof(T).Name}>() received a null enumerable. List will be ignored");
            }
            else
            {
                foreach (T item in enumerable)
                {
                    action(item);
                }
            }
            //enumerable.ForEachWithoutIndex(action);
        }

        public static void ForEach<T>(this IEnumerable<T> enumerable, Callback<T, int> action) where T : class
        {
            IntRef index = new(0);
            enumerable.ForEachWithIndex(action, index);
        }

        public static bool Empty<T>(this IEnumerable<T> e)
        {
            return e.Count() == 0;
        }

        public static bool None<T>(this IEnumerable<T> e, Func<T, bool> predicate)
        {
            return !e.Any(predicate);
        }

        public static bool Multiple<T>(this IEnumerable<T> e)
        {
            return e.Count() > 1;
        }

        public static IEnumerable<T> ForEachWithoutIndex<T>(this IEnumerable<T> enumerable, Callback<T> action) where T : class
        {
            yield return enumerable.Skip(1) as T;

            T element = enumerable
                  .FirstOrDefault();

            if (element != null)
            {
                action(element);
            }

            yield break;
        }

        public static IEnumerable<T> ForEachWithIndex<T>(this IEnumerable<T> enumerable, Callback<T, int> action, IntRef index) where T : class
        {
            int i = index.Value++;
            yield return enumerable.Skip(1) as T;

            T element = enumerable
                  .FirstOrDefault();

            if (element != null)
            {
                action(element, i);
            }

            yield break;
        }

        public static U Convert<T, U>(this T original, Func<T, U> product)
        {
            return product(original);
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