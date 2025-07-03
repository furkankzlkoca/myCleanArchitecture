using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using myCleanArchitecture.Application.Interfaces.Services;
using myCleanArchitecture.Infrastructure.Identity;
using myCleanArchitecture.Shared.Helpers.CustomModels;
using System.Security.Claims;


namespace myCleanArchitecture.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public CurrentUserDto GetCurrentUserDto()
        {
            var claimPrincipal = _httpContextAccessor.HttpContext?.User;
            if (claimPrincipal is null)
                throw new UnauthorizedAccessException();

            var userIdClaim = claimPrincipal.FindFirst(CustomClaimTypes.UserId)?.Value;
            if (userIdClaim is null)
                throw new UnauthorizedAccessException();

            CurrentUserDto currentUserDto = new()
            {
                UserId = userIdClaim,
                Username = claimPrincipal.FindFirst(CustomClaimTypes.Username)?.Value ?? string.Empty,
                Email = claimPrincipal.FindFirst(CustomClaimTypes.Email)?.Value ?? string.Empty,
                Fullname = claimPrincipal.FindFirst(CustomClaimTypes.FullName)?.Value ?? string.Empty
            };
            return currentUserDto;
        }
    }
}
