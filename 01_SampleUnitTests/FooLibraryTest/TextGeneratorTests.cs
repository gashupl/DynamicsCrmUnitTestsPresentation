using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FooLibrary.Const;
using FooLibrary;
using FooLibrary.Exceptions;

namespace FooLibraryTest
{
    [TestClass]
    public class TextGeneratorTests
    {
        [TestMethod]
        public void GenerateBirhtdayGreetings_AgeIs4_ReturnsKidsGreetings()
        {
            string expectedGreetings = GreetingsTexts.GREETINGS_KID;
            int age = 4; 

            TextGenerator textGenerator = new TextGenerator();
            string greetings = textGenerator.GenerateBirhtdayGreetings(age);

            Assert.AreEqual(expectedGreetings, greetings); 
        }

        [TestMethod]
        public void GenerateBirhtdayGreetings_AgeIs15_ReturnsGrownupsGreetings()
        {
            string expectedGreetings = GreetingsTexts.GREETINGS_MAN; 
            int age = 15;

            TextGenerator textGenerator = new TextGenerator();
            string greetings = textGenerator.GenerateBirhtdayGreetings(age);

            Assert.AreEqual(expectedGreetings, greetings);
        }

        [TestMethod]
        public void GenerateBirhtdayGreetings_AgeIs30_ReturnsOldmanGreetings()
        {
            string expectedGreetings = GreetingsTexts.GREETINGS_OLDTIMER;
            int age = 30;

            TextGenerator textGenerator = new TextGenerator();
            string greetings = textGenerator.GenerateBirhtdayGreetings(age);

            Assert.AreEqual(expectedGreetings, greetings);
        }

        [TestMethod]
        [ExpectedException(typeof(StupidJokeException))]
        public void GenerateBirhtdayGreetings_HellLanguageCode_ThrowsException()
        {
            string expectedGreetings = GreetingsTexts.GREETINGS_OLDTIMER;
            int age = 30;

            TextGenerator textGenerator = new TextGenerator();
            string greetings = textGenerator.GenerateBirhtdayGreetings(age, 666);

            Assert.AreEqual(expectedGreetings, greetings);
        }
    }
}
