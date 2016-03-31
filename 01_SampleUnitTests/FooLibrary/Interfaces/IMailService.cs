using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FooLibrary.Interfaces
{
    public interface IMailService
    {
        bool Send(string text, string emailAddress); 
    }
}
