using System;
using System.Collections.Generic;
using System.Text;

namespace FakerLib.AssemblyLoader
{
    public interface IAssemblyLoader
    {
        //Return array of types, that implement corresponding interface
        Type[] GetAssemblyTypesByImplementedInterface<T>(string AssemblyFileName) where T : class;
    }
}
