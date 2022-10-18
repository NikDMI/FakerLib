using System;

namespace FakerLib.Faker
{
    public interface IFaker
    {
        //Creates DTO
        public T Create<T>();   //noexcept
    }

    internal delegate object GenerateValueDelegate(Type objectType);
}
