using FooLibrary.Const;
using FooLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FooLibrary
{
    public class GretingsSender
    {
        #region Constructor - version A (bad approach :))
        //MailService mailService;
        //public GretingsSender()
        //{
        //    this.mailService = new MailService();
        //}
        #endregion

        #region Constructor - version B
        IMailService mailService;
        public GretingsSender(IMailService mailService)
        {
            this.mailService = mailService;
        }
        #endregion

        public void Send(string greetingsText, string emailAddress)
        {
            if (String.IsNullOrEmpty(greetingsText) || String.IsNullOrEmpty(emailAddress)) 
            {
                throw new ArgumentException(GreetingsTexts.INCORRECT_INPUT_TEXT); 
            }
            this.mailService.Send(greetingsText, emailAddress); 
        }
    }
}
