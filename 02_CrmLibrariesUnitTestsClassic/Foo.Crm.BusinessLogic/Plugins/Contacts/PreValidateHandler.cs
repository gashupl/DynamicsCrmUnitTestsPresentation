using Foo.Crm.BusinessLogic.Consts;
using Foo.Crm.BusinessLogic.Model;
using Foo.Crm.BusinessLogic.Validators;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foo.Crm.BusinessLogic.Plugins.Contacts
{
    public class PreValidateHandler : IPlugin
    {
        /// <summary>
        /// Sprawdzenie czy numer Pesel nie jest pusty oraz walidacja poprawności numeru Pesel
        /// </summary>
        /// <param name="serviceProvider"></param>
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));

            if (context.InputParameters.Contains(CrmConst.Target) && context.InputParameters[CrmConst.Target] is Entity)
            {
                Entity target = (Entity)context.InputParameters[CrmConst.Target];

                if (!target.Attributes.Contains(Contact.Fields.foo_idnumber))
                {
                    throw new InvalidPluginExecutionException(CrmMessages.ContactMessages.IncorrectOrEmptyPesel); 
                }
                else
                {
                    PeselValidator peselValidator = new PeselValidator(target.Attributes[Contact.Fields.foo_idnumber] as string);
                    if (!peselValidator.IsValid())
                    {
                        throw new InvalidPluginExecutionException(CrmMessages.ContactMessages.IncorrectOrEmptyPesel); 
                    }

                }

                
            }
        }
    }
}
