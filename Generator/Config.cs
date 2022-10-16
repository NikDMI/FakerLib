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
            if (IsPrimitiveType(typeof(T))) return true;
            return false;
        }

        //Check is type can be generated through IGenerator types
        public static bool IsGeneratedType(Type checkedType)
        {
            if (IsPrimitiveType(checkedType)) return true;
            return false;
        }

        //*** This methods have default constructors and can be generated
        private static bool IsPrimitiveType(Type checkedType){
            //Type generatedType = typeof(T);
            if (IsIntegralType(checkedType) || IsDoubleType(checkedType) ||
                checkedType == typeof(string) || checkedType == typeof(DateTime) || checkedType == typeof(bool))
            {
                return true;
            }
            return false;
        }

        internal static bool IsIntegralType(Type checkedType) 
        {
            //Type generatedType = typeof(T);
            if (checkedType == typeof(int) || checkedType == typeof(short) || checkedType == typeof(long) ||
                checkedType == typeof(char) || checkedType == typeof(byte))
            {
                return true;
            }
            return false;
        }

        internal static bool IsDoubleType(Type checkedType)
        {
            //Type generatedType = typeof(T);
            if (checkedType == typeof(double) || checkedType == typeof(float))
            {
                return true;
            }
            return false;
        }
    }
}
