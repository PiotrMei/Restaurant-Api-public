using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using NLog;
using System.Security.Claims;

namespace nowe_Restaurant_API.Authorization
{
    public class MinimumAgeRequimentHandler : AuthorizationHandler<MinimumAgeRequiment>
    {
        private readonly ILogger<MinimumAgeRequimentHandler> logger;

        public MinimumAgeRequimentHandler(ILogger<MinimumAgeRequimentHandler> logger)
        {
            this.logger = logger;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequiment requirement)
        {
            var dateofbirth = DateTime.Parse(context.User.FindFirst(u => u.Type == "Birthdate").Value);
            var a = dateofbirth.Year + requirement.MinAge;
            var email = context.User.FindFirst(c => c.Type == ClaimTypes.Name).Value.ToString();
            logger.LogInformation($"logging user {email} with dateodbirth {dateofbirth}");
            if (a<= DateTime.Now.Year)
            {
                logger.LogInformation("Age requirement Ok");
                context.Succeed(requirement);
            }
            else
            {
                logger.LogInformation("Age requirement Nok");
            }
            return Task.CompletedTask;
        }
    }
}
