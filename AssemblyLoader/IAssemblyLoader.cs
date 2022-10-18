using System;
using System.Reflection;

namespace FakerLib.AssemblyLoader
{
    public interface IAssemblyLoader
    {
        //Return array of types, that implement corresponding interface
        Type[] GetAssemblyTypesByInterfaceInSpecialAssembly<T>(string assemblyFileName) where T : class;    //noexcept
        Type[] GetAssemblyTypesByInterfaceInDirectory<T>(string assemblyDir = null) where T : class;        //noexcept
    }
}
