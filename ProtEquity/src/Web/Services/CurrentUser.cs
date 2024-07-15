using System.Security.Claims;
using ProtEquity.Application.Common.Interfaces;

namespace ProtEquity.Web.Services;

public class CurrentUser : IUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? Id => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
    public string? Name => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.GivenName) + " " + _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Surname);
}
