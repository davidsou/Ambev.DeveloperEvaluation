using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Ambev.DeveloperEvaluation.Application.Users.Services;

public class CurrentUserService : ICurrentUserService
{
    public string? UserId { get; }
    public string? Email { get; }
    public string? Username { get; }
    public string? Role { get; }
    public bool IsAuthenticated { get; }

    private readonly ClaimsPrincipal? _user;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _user = httpContextAccessor.HttpContext?.User;

        if (_user?.Identity?.IsAuthenticated == true)
        {
            IsAuthenticated = true;
            UserId = _user.FindFirstValue(ClaimTypes.NameIdentifier);
            Email = _user.FindFirstValue(ClaimTypes.Email);
            Username = _user.FindFirstValue(ClaimTypes.Name);
            Role = _user.FindFirstValue(ClaimTypes.Role);
        }
    }

    public bool IsInRole(string role) => _user?.IsInRole(role) ?? false;
}
