using System.Security.Claims;

namespace nowe_Restaurant_API.services
{
    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public ClaimsPrincipal User => httpContextAccessor.HttpContext?.User;
        public int? getUserId => User is null ? null : (int?)int.Parse(User.FindFirst(u => u.Type == ClaimTypes.NameIdentifier).Value);
    }
}
