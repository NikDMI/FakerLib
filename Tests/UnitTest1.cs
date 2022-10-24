using NUnit.Framework;
using FakerLib.Faker;
using FakerLib.AssemblyLoader;
using FakerLib.Generator;
using System;

namespace TestFaker
{
    public class Tests
    {
        //Test basic generator with predefined values
        public IGenerator _generator;

        public Faker _faker;

        [SetUp]
        public void Setup()
        {
            AssemblyLoader loader = new AssemblyLoader();
            var generators = loader.GetAssemblyTypesByInterfaceInSpecialAssembly<IGenerator>("D:\\ÁÃÓÈÐ 3 ÊÓÐÑ\\ÑÏÏ\\Labs\\LabWork_2\\BasicGenerator\\bin\\Debug\\netcoreapp3.1\\BasicGenerator.dll");
            _generator = (IGenerator)Activator.CreateInstance(generators[0]);
            _faker = new Faker(_generator);
        }

        [Test]
        public void TestByteGeneration()
        {
            byte b = 0;
            Assert.DoesNotThrow(()=>b = _faker.Create<byte>(),"Byte thrown while generated");
            Assert.AreNotEqual(0, b, "Byte can't generated");
        }


        [Test]
        public void TestShortGeneration()
        {
            short s = 0;
            Assert.DoesNotThrow(() => s = _faker.Create<short>(), "Short thrown while generated");
            Assert.AreNotEqual(0, s, "Short can't generated");
        }


        [Test]
        public void TestIntGeneration()
        {
            int i = 0;
            Assert.DoesNotThrow(() => i = _faker.Create<int>(), "Int thrown while generated");
            Assert.AreNotEqual(0, i, "Int can't generated");
        }


        [Test]
        public void TestLongGeneration()
        {
            long l = 0;
            Assert.DoesNotThrow(() => l = _faker.Create<long>(), "Long thrown while generated");
            Assert.AreNotEqual(0, l, "Long can't generated");
        }


        [Test]
        public void TestDoubleGeneration()
        {
            double v = 0;
            Assert.DoesNotThrow(() => v = _faker.Create<double>(), "Double thrown while generated");
            Assert.LessOrEqual(v, 1.01f, "Double generation was incorrect");
        }


        [Test]
        public void TestFloatGeneration()
        {
            float v = 0;
            Assert.DoesNotThrow(() => v = _faker.Create<float>(), "Float thrown while generated");
            Assert.LessOrEqual(v, 1.01f, "Float generation was incorrect");

        }


        [Test]
        public void TestBoolGeneration()
        {
            bool v = false;
            Assert.DoesNotThrow(() => v = _faker.Create<bool>(), "Bool thrown while generated");
            Assert.AreEqual(true, v, "Bool was generated incorrectly");
        }


        [Test]
        public void TestSimpleClass()
        {
            SimpleClassDTO v = null;
            if (v == null) Assert.DoesNotThrow(() => v = _faker.Create<SimpleClassDTO>(), "SimpleClassDTO thrown while generated");
            Assert.AreEqual(0, v._readOnlyField, "SimpleClassDTO was generated read-only field");
            Assert.AreEqual(null, v._recursion, "SimpleClassDTO was generated recursion class");
            Assert.Greater(v._listInt.Count, 0, "SimpleClassDTO can't generate list");
            Assert.AreEqual(null, v._complexRecursion._complexDTO._simpleDTO);
        }


        [Test]
        public void TestComplexClass()
        {
            ComplexDTO v = null;
            Assert.DoesNotThrow(() => v = _faker.Create<ComplexDTO>(), "SimpleClassDTO thrown while generated");
            Assert.AreNotEqual(null, v._simpleDTO);
        }


        [Test]
        public void TestComplexRecursion()
        {
            ComplexRecursionDTO v = null;
            Assert.DoesNotThrow(() => v = _faker.Create<ComplexRecursionDTO>(), "SimpleClassDTO thrown while generated");
            Assert.AreEqual(null, v._complexDTO._simpleDTO._complexRecursion);
        }


        [Test]
        public void TestNotDTO()
        {
            NotDTO v = null;
            Assert.DoesNotThrow(() => v = _faker.Create<NotDTO>(), "SimpleClassDTO thrown while generated");
            Assert.AreEqual(null, v);
        }
    }
}