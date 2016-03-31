using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foo.Crm.BusinessLogic.Model
{
    public class Contact
    {
        public static string EntityLogicalName = "Contact";

        public static class Fields
        {
            public static string foo_idnumber = "foo_idnumber";
            public static string AccountId = "accountid";
            public static string foo_salesrepresentativeid = "foo_salesrepresentativeid";
        }
    }
}
