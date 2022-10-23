using System;
using System.Linq.Expressions;
using FakerLib.Generator;
using System.Collections.Generic;
using System.Reflection;


namespace FakerLib.FakerConfig
{
    public class FakerConfig: IFakerConfig
    {
        public void Add<TClass, TValueType>(Expression<Func<TClass, TValueType>> expr, IGenerator generator)
        {
            //
            MemberExpression memberExpr = expr.Body as MemberExpression;
            if (memberExpr == null) throw new Exception("Invalid lambda");
            //Get parameter
            if (expr.Parameters.Count != 1) throw new Exception("Invalid lambda params");
            ParameterExpression paramExpr = expr.Parameters[0];
            //Check validation
            if (memberExpr.Member.DeclaringType != typeof(TClass)) throw new Exception("Missmatching of class types");
            //Add configuration
            Dictionary<MemberInfo, IGenerator> configGenerators;
            if(!_configInformation.TryGetValue(typeof(TClass), out configGenerators))
            {
                configGenerators = new Dictionary<MemberInfo, IGenerator>();
                _configInformation.Add(typeof(TClass), configGenerators);
            }
            if (configGenerators.ContainsKey(memberExpr.Member)) throw new Exception("Config table also contains this configuration");
            configGenerators.Add(memberExpr.Member, generator);
        }


        IGenerator IFakerConfig.GetGenerator(Type generatedType, MemberInfo memberInfo)
        {
            Dictionary<MemberInfo, IGenerator> configGenerators;
            if (!_configInformation.TryGetValue(generatedType, out configGenerators)) return null;
            IGenerator configGenerator = null;
            configGenerators.TryGetValue(memberInfo, out configGenerator);
            return configGenerator;
        }


        IGenerator IFakerConfig.GetGeneratorByName(Type generatedType, string memberName, Type memberType)
        {
            Dictionary<MemberInfo, IGenerator> configGenerators;
            if (!_configInformation.TryGetValue(generatedType, out configGenerators)) return null;
            foreach(var keyInfo in configGenerators.Keys)
            {
                if(keyInfo.Name == memberName && keyInfo.DeclaringType == memberType)
                {
                    return configGenerators[keyInfo];
                }
            }
            return null;
        }


        //**Type in nested dictionary is implemented throught IGenerator 
        private Dictionary<Type, Dictionary<MemberInfo, IGenerator>> _configInformation = new Dictionary<Type, Dictionary<MemberInfo, IGenerator>>();
    }
}
