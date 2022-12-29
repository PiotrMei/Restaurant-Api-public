using Microsoft.AspNetCore.Authorization;

namespace nowe_Restaurant_API.Authorization
{
    public class MinimumAgeRequiment : IAuthorizationRequirement
    {
        public int MinAge { get;  }
        public MinimumAgeRequiment(int minAge)
        {
            MinAge = minAge;    
        }
    }
}
