using Microsoft.AspNetCore.Authorization;

namespace nowe_Restaurant_API.Authorization
{
    public enum ResourceOperation
    {
        Create,
        Read,
        Update,
        Delete
    }
    public class ResourceOperationRequiment : IAuthorizationRequirement
    {

        public ResourceOperationRequiment(ResourceOperation resourceOperation)
        {
            ResourceOperation = resourceOperation;
        }
        public ResourceOperation ResourceOperation { get; }

    }
}
