using Microsoft.AspNetCore.Authorization;
using nowe_Restaurant_API.Entities;
using System.Security.Claims;

namespace nowe_Restaurant_API.Authorization
{
    public class ResourceOperationRequimentHandler : AuthorizationHandler<ResourceOperationRequiment, Restaurant>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequiment requirement, Restaurant resource)
        {

            if (requirement.ResourceOperation == ResourceOperation.Read || requirement.ResourceOperation == ResourceOperation.Create)
            {
                context.Succeed(requirement);
            }

            var userid = int.Parse(context.User.FindFirst(u => u.Type == ClaimTypes.NameIdentifier).Value);
            var crateuserid = resource.CreatedById;
            if (userid == crateuserid)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;  
        }
    }
}
