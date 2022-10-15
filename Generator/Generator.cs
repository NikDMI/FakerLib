using System;
using System.Collections.Generic;
using System.Text;

namespace FakerLib.Generator
{
    public sealed class Generator: IGenerator
    {
        private Random _random = new Random();
        private int MAX_STRING_CHARS_COUNT = 10;

        //Generates values according to type of var
        //*** T must be checked in FakerLib.Generator.Config class before calling this method 
        T IGenerator.GenerateValue<T>()
        {
            dynamic obj = default(T);
            if (Config.IsIntegralType<T>())
            {
                obj = GenerateIntegralValue();
            } 
            else if (Config.IsDoubleType<T>())
            {
                obj = GenerateDoubleValue();
            }
            else if(typeof(T) == typeof(string))
            {
                obj = GenerateStringValue();
            }

            return obj;
        }

        private int GenerateIntegralValue(int maxValue = int.MaxValue)
        {
            return _random.Next(maxValue);
        }

        private double GenerateDoubleValue()
        {
            return _random.NextDouble();
        }

        private string GenerateStringValue()
        {
            int strLen = _random.Next(1, MAX_STRING_CHARS_COUNT);
            Span<char> str = stackalloc char[strLen];
            for (int i = 0; i < strLen; ++i)
            {
                str[i] = (char)_random.Next('А', 'Я');
            }
            return str.ToString();
        }
    }
}
