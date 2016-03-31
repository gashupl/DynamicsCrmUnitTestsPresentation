using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FooLibrary.Exceptions
{
    public class StupidJokeException : Exception
    {
        public StupidJokeException(string message) : base(message) { }
    }
}
