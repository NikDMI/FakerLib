using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Text;
using FakerLib.Faker;
using FakerLib.FakerConfig;
using FakerLib.Generator;

namespace TestFaker
{

    public class TestGenerator1: CommonGenrator
    {
        public TestGenerator1()
        {
            RegisterTypeGeneratorFunction(typeof(int), ()=>10);
            RegisterTypeGeneratorFunction(typeof(string), ()=>"Hello");
        }
    }

    public class TestGenerator2 : CommonGenrator
    {
        public TestGenerator2()
        {
            RegisterTypeGeneratorFunction(typeof(int), () => 20);
            RegisterTypeGeneratorFunction(typeof(string), () => "Hi");
        }
    }

    internal class TestDTO
    {
        public int _intField;
        public string _stringField;
    }

    public class TestFakerConfig
    {
        TestGenerator1 _generator1 = new TestGenerator1();
        TestGenerator2 _generator2 = new TestGenerator2();

        [Test]
        public void TestWithNoConfig()
        {
            FakerConfig config = new FakerConfig();
            Faker faker = new Faker(_generator1);
            TestDTO valueDTO = null;
            Assert.DoesNotThrow(() => valueDTO = faker.Create<TestDTO>(config), "Thrown while generated");
            Assert.AreEqual(10, valueDTO._intField, "Int is invalid");
            Assert.AreEqual("Hello", valueDTO._stringField, "String is invalid");
        }

        [Test]
        public void TestWithConfig()
        {
            FakerConfig config = new FakerConfig();
            config.Add<TestDTO, int>(obj => obj._intField, _generator2);
            //config.Add<TestDTO, string>(obj => obj._stringField, _generator2);
            Faker faker = new Faker(_generator1);
            TestDTO valueDTO = null;
            Assert.DoesNotThrow(() => valueDTO = faker.Create<TestDTO>(config), "Thrown while generated");
            Assert.AreEqual(20, valueDTO._intField, "Int is invalid");
            Assert.AreEqual("Hello", valueDTO._stringField, "String is invalid");
        }
    }
}
