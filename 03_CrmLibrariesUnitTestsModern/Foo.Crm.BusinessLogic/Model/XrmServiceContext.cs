//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// Created via this command line: "C:\_EXTERNAL\TOOLBOX\CRM\EarlyBoundGenerator\Plugins\CrmSvcUtil Ref\crmsvcutil.exe" /url:"https://netwise.api.crm4.dynamics.com/XRMServices/2011/Organization.svc" /namespace:"Foo.Crm.BusinessLogic.Model" /out:"C:\_EXTERNAL\TOOLBOX\CRM\EarlyBoundGenerator\Plugins\CrmSvcUtil Ref\XrmServiceContext.cs" /servicecontextname:"XrmServiceContext" /codecustomization:"DLaB.CrmSvcUtilExtensions.Entity.CustomizeCodeDomService,DLaB.CrmSvcUtilExtensions" /codegenerationservice:"DLaB.CrmSvcUtilExtensions.Entity.CustomCodeGenerationService,DLaB.CrmSvcUtilExtensions" /codewriterfilter:"DLaB.CrmSvcUtilExtensions.Entity.CodeWriterFilterService,DLaB.CrmSvcUtilExtensions" /namingservice:"DLaB.CrmSvcUtilExtensions.Entity.OverridePropertyNames,DLaB.CrmSvcUtilExtensions" /username:"piotrg@netwise.pl" /password:"*************" 
//------------------------------------------------------------------------------

[assembly: Microsoft.Xrm.Sdk.Client.ProxyTypesAssemblyAttribute()]

namespace Foo.Crm.BusinessLogic.Model
{
	
	/// <summary>
	/// Represents a source of entities bound to a CRM service. It tracks and manages changes made to the retrieved entities.
	/// </summary>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("CrmSvcUtil", "7.1.0000.1071")]
	public partial class XrmServiceContext : Microsoft.Xrm.Sdk.Client.OrganizationServiceContext
	{
		
		/// <summary>
		/// Constructor.
		/// </summary>
		[System.Diagnostics.DebuggerNonUserCode()]
		public XrmServiceContext(Microsoft.Xrm.Sdk.IOrganizationService service) : 
				base(service)
		{
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="Foo.Crm.BusinessLogic.Model.Account"/> entities.
		/// </summary>
		public System.Linq.IQueryable<Foo.Crm.BusinessLogic.Model.Account> AccountSet
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.CreateQuery<Foo.Crm.BusinessLogic.Model.Account>();
			}
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="Foo.Crm.BusinessLogic.Model.Contact"/> entities.
		/// </summary>
		public System.Linq.IQueryable<Foo.Crm.BusinessLogic.Model.Contact> ContactSet
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.CreateQuery<Foo.Crm.BusinessLogic.Model.Contact>();
			}
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="Foo.Crm.BusinessLogic.Model.Opportunity"/> entities.
		/// </summary>
		public System.Linq.IQueryable<Foo.Crm.BusinessLogic.Model.Opportunity> OpportunitySet
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.CreateQuery<Foo.Crm.BusinessLogic.Model.Opportunity>();
			}
		}
	}
	
	internal sealed class EntityOptionSetEnum
	{
		
		[System.Diagnostics.DebuggerNonUserCode()]
		public static System.Nullable<int> GetEnum(Microsoft.Xrm.Sdk.Entity entity, string attributeLogicalName)
		{
			if (entity.Attributes.ContainsKey(attributeLogicalName))
			{
				Microsoft.Xrm.Sdk.OptionSetValue value = entity.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>(attributeLogicalName);
				if (value != null)
				{
					return value.Value;
				}
			}
			return null;
		}
	}
}