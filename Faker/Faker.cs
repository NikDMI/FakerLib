using System;
using System.Reflection;
using FakerLib.Generator;


namespace FakerLib.Faker
{
    public class Faker: IFaker
    {
        private IGenerator _generator;

        public Faker(IGenerator generator)
        {
            _generator = generator;
        }

        //Creates DTO
        public T Create<T>()
        {
            T dataTransferObject = default(T);
            ConstructorInfo defaultConstructor;
            if (!IsSuitableDTO(typeof(T), out defaultConstructor)) return dataTransferObject;
            //Create DTO with the help of parametrless constuctor
            dataTransferObject = (T)defaultConstructor.Invoke(null);
            FillPublicFields(dataTransferObject);
            return dataTransferObject;
        }


        //Check if type correspond contracts of DTO
        private bool IsSuitableDTO(Type objType, out ConstructorInfo parametrlessConstructor)
        {
            parametrlessConstructor = null;
            var publicCtor = objType.GetConstructors();
            foreach(var ctor in publicCtor)
            {
                //***If type has public constructor without params
                if (ctor.GetParameters().Length == 0)
                {
                    parametrlessConstructor = ctor;
                    return true;
                }
            }
            return false;
        }


        //Find all public field and generates 
        private void FillPublicFields<T>(T generatedObject)
        {
            var publicFields = typeof(T).GetFields();
            foreach(var fieldInfo in publicFields)
            {
                if (Config.IsGeneratedType(fieldInfo.FieldType))
                {
                    fieldInfo.SetValue(generatedObject, _generator.GenerateValue(fieldInfo.FieldType));
                }
            }
        }
    }
}
