using FooLibrary.Const;
using FooLibrary.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FooLibrary
{
    public class TextGenerator
    {

        public string GenerateBirhtdayGreetings(int age, int languageCode = 1024)
        {
            if(languageCode == 666)
            {
                throw new StupidJokeException(GreetingsTexts.AAARRRGHHH_SATAN); 
            }

            string greetingText = String.Empty; 

            if(age < 5)
            {
                greetingText = GreetingsTexts.GREETINGS_KID; 
            }
            else if(age >= 5 && age < 20)
            {
                greetingText = GreetingsTexts.GREETINGS_MAN; 
            }
            else
            {
                greetingText = GreetingsTexts.GREETINGS_OLDTIMER; 
            }

            return greetingText; 
        }
    }
}
