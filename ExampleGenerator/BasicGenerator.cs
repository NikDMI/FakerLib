using System;
using FakerLib.Generator;

namespace BasicGenerator
{
    public class BasicGenerator : CommonGenrator
    {
        public BasicGenerator()
        {
            RegisterTypeGeneratorFunction(typeof(byte), () => (byte)GenerateIntegralType());
            RegisterTypeGeneratorFunction(typeof(short), () => (short)GenerateIntegralType());
            RegisterTypeGeneratorFunction(typeof(int), () => GenerateIntegralType());
            RegisterTypeGeneratorFunction(typeof(long), () => (long)GenerateIntegralType());
            RegisterTypeGeneratorFunction(typeof(char), () => (char)('А' + _random.Next('Я' - 'А' + 1)));
            RegisterTypeGeneratorFunction(typeof(bool), () => true);
            RegisterTypeGeneratorFunction(typeof(float), () => (float)GenerateDoubleValue());
            RegisterTypeGeneratorFunction(typeof(double), () => GenerateDoubleValue());
            RegisterTypeGeneratorFunction(typeof(string), GenerateStringValue);
        }


        private Random _random = new Random();

        private int MAX_INTEGRAL_VALUE = 100;

        private int MAX_STRING_CHARS_COUNT = 10;


        private int GenerateIntegralType()
        {
            return _random.Next(MAX_INTEGRAL_VALUE) + 1;
        }


        private double GenerateDoubleValue()
        {
            return _random.NextDouble();
        }


        private object GenerateStringValue()
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
