using System.Security.Claims;

namespace nowe_Restaurant_API.services
{
    public interface IUserContextService
    {
        int? getUserId { get; }
        ClaimsPrincipal User { get; }
    }
}