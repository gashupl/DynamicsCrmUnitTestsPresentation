using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using System.ServiceModel;
using Foo.Crm.BusinessLogic.Model;

namespace Foo.Crm.BusinessLogic.Repositories
{
    public class RepositoryMessage : Repository, IRepositoryMessage
    {
        protected IOrganizationService Service { get; set; }
        protected XrmServiceContext XrmContext { get; set; }

        public RepositoryMessage() { }

        public void Initialize(IOrganizationService service)
        {
            this.Service = service;
            this.XrmContext = new XrmServiceContext(service);
            this.XrmContext.MergeOption = Microsoft.Xrm.Sdk.Client.MergeOption.NoTracking;
        }

        public SetStateResponse SetState(EntityReference entityId, int stateCode, int statusCode)
        {
            SetStateRequest request = new SetStateRequest();
            request.EntityMoniker = entityId;
            request.State = new OptionSetValue(stateCode);
            request.Status = new OptionSetValue(statusCode);
            return (SetStateResponse)this.Service.Execute(request);
        }

        public void Associate(EntityReference entity, Relationship relationship, EntityReferenceCollection relatedEntities)
        {
            this.Service.Associate(entity.LogicalName, entity.Id, relationship, relatedEntities);
        }

        public void Associate(EntityReference entity, string relationship, IList<EntityReference> relatedEntities)
        {
            EntityReferenceCollection collection = new EntityReferenceCollection();
            collection.AddRange(relatedEntities);

            this.Service.Associate(entity.LogicalName, entity.Id, new Relationship(relationship), collection);
        }

        public void DisAssociate(EntityReference entity, Relationship relationship, EntityReferenceCollection relatedEntities)
        {
            this.Service.Disassociate(entity.LogicalName, entity.Id, relationship, relatedEntities);
        }

        public void DisAssociate(EntityReference entity, string relationship, IList<EntityReference> relatedEntities)
        {
            EntityReferenceCollection collection = new EntityReferenceCollection();
            collection.AddRange(relatedEntities);

            this.Service.Disassociate(entity.LogicalName, entity.Id, new Relationship(relationship), collection);
        }

        public GrantAccessResponse GrantAccess(EntityReference entityId, EntityReference principal, AccessRights accessRights)
        {
            return (GrantAccessResponse)Service.Execute(new GrantAccessRequest()
            {
                Target = entityId,
                PrincipalAccess = new PrincipalAccess()
                {
                    Principal = principal,
                    AccessMask = accessRights,
                }
            });
        }

        public RevokeAccessResponse RevokeAccess(EntityReference entityId, EntityReference principal)
        {
            return (RevokeAccessResponse)Service.Execute(new RevokeAccessRequest()
            {
                Target = entityId,
                Revokee = principal,
            });
        }

        public bool IsAttributeUnique<T>(string attrName, T attrValue, string logicalName, Guid withoutId)
        {
            QueryExpression query = new QueryExpression(logicalName);
            string attrIdname = String.Format("{0}id", logicalName);
            query.ColumnSet = new ColumnSet(attrIdname);
            query.TopCount = 1;
            query.Criteria.AddCondition(attrName, ConditionOperator.Equal, attrValue);
            query.Criteria.AddCondition(attrIdname, ConditionOperator.NotEqual, withoutId);
            return this.Service.RetrieveMultiple(query).Entities.Count == 0;
        }



        public AssignResponse AssignUser(EntityReference entityId, EntityReference ownerId)
        {
            AssignRequest req = new AssignRequest();
            req.Assignee = ownerId;
            req.Target = entityId;
            return (AssignResponse)this.Service.Execute(req);
        }

       
        public Entity RetrieveRecordById(EntityReference id, ColumnSet columns = null)
        {
            return RetrieveRecordById(id.LogicalName, id.Id, columns);
        }

        public Entity RetrieveRecordById(string logicalName, Guid id, ColumnSet columns = null)
        {
            return this.Service.Retrieve(logicalName, id, columns ?? new ColumnSet(true));
        }


        public IEnumerable<Entity> RetrieveMultiple(QueryExpression query)
        {
            return this.Service.RetrieveMultiple(query).Entities;
        }

        public void Update(Entity entity)
        {
            this.Service.Update(entity);
        }

        public Guid Create(Entity entity)
        {
            return this.Service.Create(entity);
        }

        public PickFromQueueResponse PickFromQueue(Guid queueItemId, Guid workerId, bool removeQueueItem = true)
        {
            PickFromQueueRequest pickFromQueueRequest = new PickFromQueueRequest
            {
                QueueItemId = queueItemId,
                WorkerId = workerId,
                RemoveQueueItem = removeQueueItem
            };

            return this.Service.Execute(pickFromQueueRequest) as PickFromQueueResponse;
        }

        public ReleaseToQueueResponse ReleaseToQueue(Guid queueItemId)
        {
            ReleaseToQueueRequest releaseToQueueRequest = new ReleaseToQueueRequest
            {
                QueueItemId = queueItemId
            };

            return this.Service.Execute(releaseToQueueRequest) as ReleaseToQueueResponse;
        }

        public OrganizationResponse Execute(OrganizationRequest request)
        {
            return this.Service.Execute(request);
        }

        public RetrieveEntityResponse RetrieveEntity(RetrieveEntityRequest request)
        {
            return (RetrieveEntityResponse)this.Service.Execute(request);
        }

        public EntityMetadata GetEntityMetadata(RetrieveEntityRequest request)
        {
            RetrieveEntityResponse response = (RetrieveEntityResponse)this.Service.Execute(request);
            return (EntityMetadata)response.EntityMetadata;
        }

        public void SharePrivileges(EntityReference targetEntity, EntityReference assigneeRef, bool readAccess,
            bool writeAccess, bool appendAccess)
        {
            try
            {
                AccessRights accessRights = new AccessRights();
                accessRights = AccessRights.None;
                //Read Access           
                if (readAccess == true)
                    accessRights = AccessRights.ReadAccess;
                //Write Access (or) Read, Write Access        
                if (writeAccess == true)
                    if (accessRights == AccessRights.None)
                        accessRights = AccessRights.WriteAccess;
                    else
                        accessRights = accessRights | AccessRights.WriteAccess;
                //Append Access or all or any two accesses         
                if (appendAccess == true)
                    if (accessRights == AccessRights.None)
                        accessRights = AccessRights.AppendToAccess | AccessRights.AppendAccess;
                    else
                        accessRights = accessRights | AccessRights.AppendToAccess | AccessRights.AppendAccess;
                var grantAccess = new GrantAccessRequest
                {
                    PrincipalAccess = new PrincipalAccess
                    {
                        AccessMask = accessRights,
                        Principal = assigneeRef
                    },
                    Target = targetEntity
                };
                // Execute the Request      
                this.Service.Execute(grantAccess);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occured while applying Sharing rules for the record." + ex.Message +
                                    assigneeRef.Id + "|| " + targetEntity.Id);
            }
        }

        public ExecuteWorkflowResponse ExecuteWorkflow(Guid workflowId, EntityReference entityId)
        {
            var executeWorkflowRequest = new ExecuteWorkflowRequest
            {
                EntityId = entityId.Id,
                WorkflowId = workflowId
            };

            return this.Service.Execute(executeWorkflowRequest) as ExecuteWorkflowResponse;
        }


        public CalculateRollupFieldResponse CalculateRollupField(EntityReference target, string attributeName)
        {
            CalculateRollupFieldRequest recalculateRequest = new CalculateRollupFieldRequest()
            {
                FieldName = attributeName,
                Target = target
            };

            return this.Service.Execute(recalculateRequest) as CalculateRollupFieldResponse;
        }

        public AddUserToRecordTeamResponse AddUserToRecordTeam(EntityReference recordId, Guid userId, Guid templateId)
        {
            var req = new AddUserToRecordTeamRequest() { Record = recordId, SystemUserId = userId, TeamTemplateId = templateId };
            return (AddUserToRecordTeamResponse)this.Service.Execute(req);
        }

        public RemoveUserFromRecordTeamResponse RemoveUserFromRecordTeam(EntityReference recordId, Guid userId, Guid templateId)
        {
            var req = new RemoveUserFromRecordTeamRequest() { Record = recordId, SystemUserId = userId, TeamTemplateId = templateId };
            return (RemoveUserFromRecordTeamResponse)this.Service.Execute(req);
        }

        public void AddUserToRecordTeam(IEnumerable<EntityReference> recordsIds, Guid userId, Guid templateId)
        {
            //TODO: Dopiero w Update 1
            //var multipleRequest = new ExecuteTransactionRequest()
            //{
            //    Requests = new OrganizationRequestCollection(),
            //    ReturnResponses = false
            //};

            //foreach (var recordId in recordsIds)
            //{
            //    multipleRequest.Requests.Add(new AddUserToRecordTeamRequest() { Record = recordId, SystemUserId = userId, TeamTemplateId = templateId });
            //}

            //return (ExecuteTransactionResponse)this.Service.Execute(multipleRequest);

            foreach (var recordId in recordsIds)
            {
                this.Service.Execute(new AddUserToRecordTeamRequest() { Record = recordId, SystemUserId = userId, TeamTemplateId = templateId });
            }
        }

        public void RemoveUserFromRecordTeam(IEnumerable<EntityReference> recordsIds, Guid userId, Guid templateId)
        {
            //TODO: Dopiero w Update 1
            //var multipleRequest = new ExecuteTransactionRequest()
            //{
            //    Requests = new OrganizationRequestCollection(),
            //    ReturnResponses = false
            //};

            //foreach (var recordId in recordsIds)
            //{
            //    multipleRequest.Requests.Add(new RemoveUserFromRecordTeamRequest() { Record = recordId, SystemUserId = userId, TeamTemplateId = templateId });
            //}

            //return (ExecuteTransactionResponse)this.Service.Execute(multipleRequest);

            foreach (var recordId in recordsIds)
            {
                this.Service.Execute(new RemoveUserFromRecordTeamRequest() { Record = recordId, SystemUserId = userId, TeamTemplateId = templateId });
            }
        }
    }
}
