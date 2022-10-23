using System;
using System.Collections.Generic;
using System.Text;

namespace FakerLib.Generator
{
    public interface IGenerator
    {
        /// <summary>
        /// Generates values according to type of var (null - if function can't be completed)
        /// </summary>
        /// <param name="valueType"></param>
        /// <returns></returns>
        object GenerateValue(Type valueType);
        bool IsGeneratedValue(Type valueType);
    }

    //
    public delegate object GenerateValueDelegate();
}
