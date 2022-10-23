using System;
using System.Linq.Expressions;
using FakerLib.Generator;
using System.Reflection;

namespace FakerLib.FakerConfig
{
    public interface IFakerConfig
    {
        void Add<TClass, TValueType> (Expression<Func<TClass, TValueType>> expr, IGenerator generator);

        internal IGenerator GetGenerator(Type generatedType, MemberInfo memberInfo);
        internal IGenerator GetGeneratorByName(Type generatedType, string memberName, Type memberType);
    }
}
