using FooLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FooLibrary
{
    public class MailService : IMailService
    {
        public bool Send(string text, string emailAddress)
        {
            //throw new NotImplementedException();
            return true; 
        }
    }
}
