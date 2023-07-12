using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace HrProject.Filter
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirment>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PermissionAuthorizationHandler(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirment requirement)
        {
            if (context.User == null)
                return;
            //var userPermissions = context.User.Claims.Where(c => c.Type == "Permission" && c.Issuer == "LOCAL AUTHORITY").Select(c => c.Value).ToList();
            //var permissions = requirement.Permission.Split(',');
            //var canAccess = userPermissions.Intersect(permissions).Any();
            //var canAccess = context.User.Claims.Any(c => c.Type == "Permission" && permissions.Contains(c.Value) && c.Issuer == "LOCAL AUTHORITY");
            //var canAccess = context.User.HasClaim(c => c.Type == "Permission" && c.Value == requirement.Permission && c.Issuer == "LOCAL AUTHORITY");
            //var canAccess = context.User.Claims.FirstOrDefault(c => c.Type == "Permission" && c.Value == requirement.Permission && c.Issuer == "LOCAL AUTHORITY")!=null;


            var canAccess = context.User.Claims.Any(c => c.Type == "Permission" && c.Value == requirement.Permission && c.Issuer == "LOCAL AUTHORITY");
            if (canAccess)
            {
                context.Succeed(requirement);
                return;
            }
            //else
            //{
            //    if (context.User.Claims.Any(c => c.Type == "Permission" && c.Value == requirement.Permission && c.Issuer == "LOCAL AUTHORITY"))
            //    {
            //        var httpContext = _httpContextAccessor.HttpContext;
            //        httpContext?.Response.Redirect("/Error/NotAllowed");
            //        context.Fail();
            //        return;
            //    }
            //}
        }
    }
}
