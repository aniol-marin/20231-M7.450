using Mole.Halt.Utils;
using System;
using System.Collections.Generic;

namespace Mole.Halt.ApplicationLayer
{
    public class DataPackage : DataAsset
    {
        // TO DO replace with DataAsset which can at its own time create or serialize packages
        static Dictionary<Type, DataPackage> packages = new(); 

        public static T Install<T>() where T : DataPackage
        {
            if (!packages.TryGetValue(typeof(T), out DataPackage package))
            {
                packages.Add(typeof(T), CreateInstance<T>());
            }

            return package as T;
        }
    }
}