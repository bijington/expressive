using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Expressive.Utils
{
    public static class Reflective
    {
        /// <summary>
        /// Method to get a list of types implementing an interface
        /// </summary>
        /// <param name="interfaceType">Interface we want to know implementing classes.</param>
        /// <returns></returns>
        public static List<TypeInfo> GetClassesImplementingInterface(Type interfaceType)
        {
            Assembly asm = interfaceType.GetTypeInfo().Assembly; //thanks to @mjwills  for his helpfull comment

            return asm.DefinedTypes.Where(x => x.ImplementedInterfaces.Contains(interfaceType) && !x.IsAbstract).ToList();
        }

        /// <summary>
        /// Getting a list of objects from a type info list
        /// </summary>
        /// <param name="typeInfos">List of types for which we want to create an instance.</param>
        /// <returns>A list of instances of T type</returns>
        /// <typeparam name="T">Interface type to return</typeparam>
        public static List<T> GetInstances<T>(List<TypeInfo> typeInfos)
        {
            return typeInfos.Select(typeInfo => Activator.CreateInstance(typeInfo.AsType())).Cast<T>().ToList();
        }
    }
}
