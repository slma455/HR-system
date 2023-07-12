using System.Web.Mvc;
using System.Web;
using HrProject.Filter;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Authorization;

namespace HrProject.Attributes
{
    //public class UnAuthoritizeCustomeAttriibute : AuthorizeAttribute
    //{
    //    protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
    //    {
    //        //if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
    //        //{
    //        //    // User is not authenticated, redirect to login page
    //        //    filterContext.Result = new RedirectResult("~/Account/Login");
    //        //}
    //        //else

    //        // User is authenticated but not authorized, redirect to custom error page
    //        filterContext.Result = new RedirectResult("~/Error/NotAllowed");

    //    }
    //}


    //public class CustomAuthorizeAttribute : Attribute, IAsyncAuthorizationFilter
    //{
    //    private readonly string _permission;

    //    public CustomAuthorizeAttribute(string permission)
    //    {
    //        _permission = permission;
    //    }

    //    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    //    {
    //        var authorizationService = context.HttpContext.RequestServices.GetRequiredService<IAuthorizationService>();
    //        var authorizationResult = await authorizationService.AuthorizeAsync(context.HttpContext.User, null, new PermissionRequirement(_permission));

    //        if (!authorizationResult.Succeeded)
    //        {
    //            if (!context.HttpContext.User.Identity.IsAuthenticated)
    //            {
    //                // User is not authenticated, redirect to login page or custom error page
    //                context.Result = new RedirectResult("~/Account/Login");
    //            }
    //            else
    //            {
    //                // User is authenticated but does not have the required permission, redirect to custom error page
    //                context.Result = new RedirectResult("~/Error/NotAllowed");
    //            }
    //        }
    //    }
    //}
}
