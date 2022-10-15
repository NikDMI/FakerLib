

namespace FakerLib.Faker
{
    public interface IFaker
    {
        //Creates DTO
        public T Create<T>();

        //Generates values according to type of var
        internal T GenerateValue<T>();
    }
}
