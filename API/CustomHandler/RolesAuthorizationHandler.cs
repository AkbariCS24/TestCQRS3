using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using TestCQRS3.Application.Command.Common;

namespace TestCQRS3.API.CustomHandler
{
    public class RolesAuthorizationHandler : AuthorizationHandler<RolesAuthorizationRequirement>, IAuthorizationHandler
    {
        private readonly ITestCQRS3DBContext _context;

        public RolesAuthorizationHandler(ITestCQRS3DBContext context)
        {
            _context = context;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       RolesAuthorizationRequirement requirement)
        {
            if (context.User == null || !context.User.Identity.IsAuthenticated)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            var validRole = false;
            if (requirement.AllowedRoles == null ||
                requirement.AllowedRoles.Any() == false)
            {
                validRole = true;
            }
            else
            {
                var claims = context.User.Claims;
                var userName = claims.FirstOrDefault(c => c.Type == "UserName").Value;
                var roles = requirement.AllowedRoles;

                //validRole = new Users().GetUsers().Where(p => roles.Contains(p.Role) && p.UserName == userName).Any();
                validRole =  _context.Users.Where(p => roles.Contains(p.UserRole.Title) && p.UserName == userName).Any();
            }

            if (validRole)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
            return Task.CompletedTask;
        }
    }
}
