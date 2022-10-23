using System;
using System.Collections.Generic;

namespace FakerLib.Generator
{
    public class CommonGenrator: IGenerator
    {

        public object GenerateValue(Type valueType)
        {
            GenerateValueDelegate generatorFunction;
            bool hasFunctionGenerator = _typeGenerators.TryGetValue(valueType, out generatorFunction);
            if (!hasFunctionGenerator) return null;
            return generatorFunction();
        }


        public bool IsGeneratedValue(Type valueType)
        {
            return _typeGenerators.ContainsKey(valueType);
        }


        protected void RegisterTypeGeneratorFunction(Type registerType, GenerateValueDelegate generatorFunction)
        {
            if (_typeGenerators.ContainsKey(registerType))
            {
                _typeGenerators[registerType] = generatorFunction;
            }
            else
            {
                _typeGenerators.Add(registerType, generatorFunction);
            }
        }


        private Dictionary<Type, GenerateValueDelegate> _typeGenerators = new Dictionary<Type, GenerateValueDelegate>();
    }
}
