using System;
using System.Collections.Generic;

namespace FakerLib.Generator
{
    //This class describes "contracts" to methods of generators
    internal static class Config
    {
        //Check is type T can be generated through IGenerator types
        public static bool IsGeneratedType<T>()
        {
            if (IsPrimitiveType<T>()) return true;
            return false;
        }

        //*** This methods have default constructors and can be generated
        private static bool IsPrimitiveType<T>(){
            Type generatedType = typeof(T);
            if (IsIntegralType<T>() || IsDoubleType<T>() ||
                generatedType == typeof(string) || generatedType == typeof(DateTime) || generatedType == typeof(bool))
            {
                return true;
            }
            return false;
        }

        internal static bool IsIntegralType<T>() 
        {
            Type generatedType = typeof(T);
            if (generatedType == typeof(int) || generatedType == typeof(short) || generatedType == typeof(long) ||
                generatedType == typeof(char) || generatedType == typeof(byte))
            {
                return true;
            }
            return false;
        }

        internal static bool IsDoubleType<T>()
        {
            Type generatedType = typeof(T);
            if (generatedType == typeof(double) || generatedType == typeof(float))
            {
                return true;
            }
            return false;
        }
    }
}
