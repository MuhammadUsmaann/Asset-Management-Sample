using System.Security.Claims;

namespace AssetManagement.Server.Infrastructure.BusinessLogic.Services
{
    public interface IIdentityService
    {
        int GetUserId();
        string? GetUserEmail();
        string? GetUsername();
        string? GetAccessToken();
    }

    public class IdentityService : IIdentityService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public IdentityService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetUserId() =>
            Convert.ToInt32(_httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

        public string? GetUserEmail() =>
            _httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

        public string? GetUsername() =>
            _httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

        public string? GetAccessToken()
        {
            var authHeader = _httpContextAccessor?.HttpContext?.Request.Headers["Authorization"] ?? string.Empty;
            if (!string.IsNullOrEmpty(authHeader))
            {
                var rawToken = authHeader.ToString().Split(" ")[1];
                if (string.IsNullOrWhiteSpace(rawToken))
                    throw new UnauthorizedAccessException($"Access Denied: Token is null");

                return rawToken;
            }

            return string.Empty;
        }
    }
}
