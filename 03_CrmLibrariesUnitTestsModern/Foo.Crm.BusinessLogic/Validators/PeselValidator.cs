using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foo.Crm.BusinessLogic.Validators
{
    public class PeselValidator
    {
        private string idNumber { get; }

        public PeselValidator(string idNumber)
        {
            this.idNumber = idNumber; 
        }

        public bool IsValid()
        {
            if (idNumber.Equals("82051409274"))
            {
                return true; 
            }
            else
            {
                return false;
            }
        }
    }
}
