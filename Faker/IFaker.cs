using System;
using FakerLib.FakerConfig;

namespace FakerLib.Faker
{
    public interface IFaker
    {
        //Creates DTO
        public T Create<T>();   //noexcept
        public T Create<T>(IFakerConfig fakeConfig);
    }

    internal delegate object GenerateValueDelegate(Type objectType);
}
