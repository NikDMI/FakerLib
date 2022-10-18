using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace FakerLib.AssemblyLoader
{
    public class AssemblyLoader : IAssemblyLoader
    {
        public Type[] GetAssemblyTypesByInterfaceInSpecialAssembly<T>(string assemblyFileName) where T : class
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


        public Type[] GetAssemblyTypesByInterfaceInDirectory<T>(string assemblyDir = null) where T : class
        {
            if (assemblyDir == null) assemblyDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            List<Type> findAssemblies = new List<Type>();
            try
            {
                var pluginAssemblies = Directory.EnumerateFiles(assemblyDir, "*.dll");
                foreach (var pluginFileName in pluginAssemblies)
                {
                    findAssemblies.AddRange(GetAssemblyTypesByInterfaceInSpecialAssembly<T>(pluginFileName));
                }
            }
            catch (Exception e) { }     //Dir not found exception

            return findAssemblies.ToArray();
        }

    }
}
