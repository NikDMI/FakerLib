using System;

namespace FakerLib.Faker
{
    public interface IFaker
    {
        //Creates DTO
        public T Create<T>();
    }

    internal delegate object GenerateValueDelegate(Type objectType);
}
