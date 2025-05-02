using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class AuthorizeRoleAttribute : Attribute, IAuthorizationFilter
{
    private readonly string _role;

    public AuthorizeRoleAttribute(UserRole role= UserRole.Customer)
    {
        _role = role.ToString();
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata
            .OfType<AllowAnonymousAttribute>().Any();

        if (allowAnonymous)
            return;

        var user = context.HttpContext.User;

        if (!user.Identity?.IsAuthenticated ?? false)
        {
            context.Result = new UnauthorizedResult(); // 401
            return;
        }

        if (!user.IsInRole(_role))
        {
            context.Result = new ForbidResult(); // 403
        }
    }
}
