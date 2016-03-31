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

namespace Foo.Crm.BusinessLogic.Repositories
{
    public interface IRepositoryMessage
    {
        SetStateResponse SetState(EntityReference entityId, int stateCode, int statusCode);

        void Associate(EntityReference entity, Relationship relationship,
            EntityReferenceCollection relatedEntities);

        void Associate(EntityReference entity, string relationship, IList<EntityReference> relatedEntities);

        void DisAssociate(EntityReference entity, Relationship relationship,
            EntityReferenceCollection relatedEntities);

        void DisAssociate(EntityReference entity, string relationship, IList<EntityReference> relatedEntities);

        GrantAccessResponse GrantAccess(EntityReference entityId, EntityReference principal,
            AccessRights accessRights);

        RevokeAccessResponse RevokeAccess(EntityReference entityId, EntityReference principal);

        bool IsAttributeUnique<T>(string attrName, T attrValue, string logicalName, Guid withoutId);

        AssignResponse AssignUser(EntityReference entityId, EntityReference ownerId);

        IEnumerable<Entity> RetrieveMultiple(QueryExpression query);

        void Update(Entity entity);

        Guid Create(Entity entity);

        PickFromQueueResponse PickFromQueue(Guid queueItemId, Guid workerId, bool removeQueueItem = true);

        ReleaseToQueueResponse ReleaseToQueue(Guid queueItemId);

        Entity RetrieveRecordById(string logicalName, Guid id, ColumnSet columns = null);

        OrganizationResponse Execute(OrganizationRequest request);

        CalculateRollupFieldResponse CalculateRollupField(EntityReference target, string attributeName);

        RetrieveEntityResponse RetrieveEntity(RetrieveEntityRequest request);

        EntityMetadata GetEntityMetadata(RetrieveEntityRequest request);

        void SharePrivileges(EntityReference targetEntity, EntityReference assigneeRef, bool readAccess,
            bool writeAccess, bool appendAccess);

        ExecuteWorkflowResponse ExecuteWorkflow(Guid workflowId, EntityReference entityId);

        AddUserToRecordTeamResponse AddUserToRecordTeam(EntityReference recordId, Guid userId, Guid templateId);
        RemoveUserFromRecordTeamResponse RemoveUserFromRecordTeam(EntityReference recordId, Guid userId, Guid templateId);
        void AddUserToRecordTeam(IEnumerable<EntityReference> recordsIds, Guid userId, Guid templateId);
        void RemoveUserFromRecordTeam(IEnumerable<EntityReference> recordsIds, Guid userId, Guid templateId);
    }
}
