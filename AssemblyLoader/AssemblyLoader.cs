using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace FakerLib.AssemblyLoader
{
    public class AssemblyLoader : IAssemblyLoader
    {
        public Type[] GetAssemblyTypesByImplementedInterface<T>(string assemblyFileName) where T : class
        {
            Assembly loadedAssembly;
            try
            {
                loadedAssembly = Assembly.LoadFrom(assemblyFileName);
            }
            catch (Exception e)
            {
                return new Type[0];
            }
            Type[] assemblyTypes = loadedAssembly.GetExportedTypes();
            List<Type> suitableAssemblies = new List<Type>();
            foreach (var assemblyType in assemblyTypes)
            {
                if (assemblyType.IsClass && assemblyType.GetInterface(typeof(T).FullName) != null)
                {
                    suitableAssemblies.Add(assemblyType);
                }
            }
            return suitableAssemblies.ToArray();
        }

    }
}
