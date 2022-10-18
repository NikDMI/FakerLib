using System;
using System.Reflection;
using FakerLib.Generator;
using System.Collections.Generic;


namespace FakerLib.Faker
{
    public class Faker : IFaker
    {
        private IGenerator _generator;
        private Random _random = new Random();
        private List<Type> _generatedTypes;

        private int MAX_COLLECTION_ELEMENT_COUNT = 10;

        public Faker(IGenerator generator)
        {
            _generator = generator;
        }

        //Creates DTO (or default presantation) -> user version
        public T Create<T>()
        {
            _generatedTypes = new List<Type>();
            object generatedObject = Create(typeof(T));
            if (generatedObject == null) return default(T);
            return (T)generatedObject;
        }

        //Generate object or return null if generation is depricated -> class version
        private object Create(Type objectType)
        {
            if (_generatedTypes.Contains(objectType)) return null;  //Deprecate recursion
            object generatedObject = null;
            if (!IsSuitableDTO(objectType, out generatedObject)) return null;
            //If obj is non generated reference type
            if (!Config.IsGeneratedType(objectType))
            {
                FillPublicFields(generatedObject);
                FillPublicProperties(generatedObject);
            }
            _generatedTypes.Remove(objectType); //When object was generated and we go up if was recursion call
            return generatedObject;
        }


        //Check if type correspond contracts of DTO and create default instance
        private bool IsSuitableDTO(Type objType, out object generatedObject)
        {
            generatedObject = null;
            //Check if it's ref type and has ppublic parametless constructor
            var publicCtor = objType.GetConstructors();
            foreach (var ctor in publicCtor)
            {
                //***If type has public constructor without params
                if (ctor.GetParameters().Length == 0)
                {
                    generatedObject = ctor.Invoke(null);
                    _generatedTypes.Add(objType);    //Add to list of generated objects
                    return true;
                }
            }
            //Check if it is generated type by IGenerator
            if (Config.IsGeneratedType(objType))
            {
                generatedObject = _generator.GenerateValue(objType);
                return true;
            }
            //If it is a structure
            if (objType.IsValueType)
            {
                generatedObject = Activator.CreateInstance(objType);
                return true;
            }
            return false;
        }


        //***
        //*** THIS SECTION DESCRIBES METHODS TO GENERATE REFERENCE OBJECTS***
        //***


        //Returs method, that can be used to generate object of corresponding type (like Algorithm factory pattern)
        private GenerateValueDelegate GetGeneratedDelegate(Type objectType)
        {
            if (Config.IsGeneratedType(objectType))
            {
                return _generator.GenerateValue;
            }
            else if (objectType.GetInterface(typeof(System.Collections.IList).FullName) != null && objectType.IsGenericType)
            {
                return this.GenerateCollectionsTypes;
            }
            else
            {
                return this.Create;
            }
            //return null;
        }

        //Return generated collection of primitive types
        private object GenerateCollectionsTypes(Type collectionType)
        {
            //Generate collection size
            int collectionLength = _random.Next(1, MAX_COLLECTION_ELEMENT_COUNT);
            //If collection inhereted from IList
            if (collectionType.GetInterface(typeof(System.Collections.IList).FullName) != null && collectionType.IsGenericType)
            {
                Type elementType = collectionType.GetGenericArguments()[0];
                var listObject = (System.Collections.IList)System.Activator.CreateInstance(collectionType);
                for (int i = 0; i < collectionLength; i++)
                {
                    listObject.Add(Create(elementType));
                }
                return listObject;
            }
            //If there is no supported collection types
            return null;
        }


        //Find all public field and generates 
        private void FillPublicFields(object generatedObject)
        {
            Type generatedObjectType = generatedObject.GetType();
            var publicFields = generatedObjectType.GetFields();
            foreach (var fieldInfo in publicFields)
            {
                var generationAlgorithm = GetGeneratedDelegate(fieldInfo.FieldType);
                fieldInfo.SetValue(generatedObject, generationAlgorithm(fieldInfo.FieldType));
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
                    var generationAlgorithm = GetGeneratedDelegate(propertyInfo.PropertyType);
                    propertyInfo.SetValue(generatedObject, generationAlgorithm(propertyInfo.PropertyType));
                }
            }
        }

    }
}
