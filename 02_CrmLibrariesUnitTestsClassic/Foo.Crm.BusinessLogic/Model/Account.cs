using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foo.Crm.BusinessLogic.Model
{
    public class Account
    {
        public static string EntityLogicalName = "Account";

        public static class Fields
        {
            public static string foo_salesrepresentativeid = "foo_salesrepresentativeid"; 
            public static string AccountId = "accountid";
        }
    }
}
