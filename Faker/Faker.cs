using System;
using System.Reflection;
using FakerLib.Generator;
using System.Collections.Generic;


namespace FakerLib.Faker
{
    public class Faker : IFaker
    {
        private IGenerator _generator;
        private int MAX_COLLECTION_ELEMENT_COUNT = 10;

        public Faker(IGenerator generator)
        {
            _generator = generator;
        }

        //Creates DTO (or default presantation)
        public T Create<T>()
        {
            object generatedObject = Create(typeof(T));
            if (generatedObject == null) return default(T);
            return (T)generatedObject;
        }

        //Generate object or return null if generation is depricated
        public object Create(Type objectType)
        {
            ConstructorInfo defaultConstructor;
            if (!IsSuitableDTO(objectType, out defaultConstructor)) return null;
            //Create DTO with the help of parametrless constuctor
            object dataTransferObject = defaultConstructor.Invoke(null);
            FillPublicFields(dataTransferObject);
            FillPublicProperties(dataTransferObject);
            return dataTransferObject;
        }


        //Check if type correspond contracts of DTO
        private bool IsSuitableDTO(Type objType, out ConstructorInfo parametrlessConstructor)
        {
            parametrlessConstructor = null;
            var publicCtor = objType.GetConstructors();
            foreach (var ctor in publicCtor)
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


        //Return generated collection of primitive types
        private object GenerateCollectionsTypes(Type collectionType)
        {
            int collectionLength = new Random().Next(0, MAX_COLLECTION_ELEMENT_COUNT);
            //If collection inhereted from IList
            if(collectionType.GetInterface(typeof(System.Collections.IList).FullName)!=null)
            {
                Type elementType = collectionType.GetGenericArguments()[0];
                if (Config.IsGeneratedType(elementType))
                {
                    var listObject = (System.Collections.IList)System.Activator.CreateInstance(collectionType);
                    for (int i = 0; i < collectionLength; i++)
                    {
                        listObject.Add(_generator.GenerateValue(elementType));
                    }
                    return listObject;
                }
            }
            return null;
        }


        //Find all public field and generates 
        private void FillPublicFields(object generatedObject)
        {
            Type generatedObjectType = generatedObject.GetType();
            var publicFields = generatedObjectType.GetFields();
            foreach (var fieldInfo in publicFields)
            {
                if (Config.IsGeneratedType(fieldInfo.FieldType))
                {
                    fieldInfo.SetValue(generatedObject, _generator.GenerateValue(fieldInfo.FieldType));
                    //return;
                }
                else
                {
                    object collectionObject = GenerateCollectionsTypes(fieldInfo.FieldType);
                    fieldInfo.SetValue(generatedObject, collectionObject);
                }
            }
        }



        //Find all public field and generates 
        private void FillPublicProperties(object generatedObject)
        {
            Type generatedObjectType = generatedObject.GetType();
            var publicProp = generatedObjectType.GetProperties();
            foreach (var propertyInfo in publicProp)
            {
                if (propertyInfo.CanWrite)
                {
                    if (Config.IsGeneratedType(propertyInfo.PropertyType))
                    {
                        propertyInfo.SetValue(generatedObject, _generator.GenerateValue(propertyInfo.PropertyType));
                    }
                    else
                    {
                        object collectionObject = GenerateCollectionsTypes(propertyInfo.PropertyType);
                        propertyInfo.SetValue(generatedObject, collectionObject);
                    }
                }
            }
        }

    }
}
