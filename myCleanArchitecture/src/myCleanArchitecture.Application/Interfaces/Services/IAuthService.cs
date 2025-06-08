using myCleanArchitecture.Shared.FeatureModels.Authentication;
using myCleanArchitecture.Shared.FeatureModels.Authentication.Commands;

namespace myCleanArchitecture.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<ObjectResult<AuthModel>> Register(RegisterUserCommand command);
        Task<ObjectResult<AuthModel>> Login(LoginUserCommand command);
        Task<ObjectResult<AuthModel>> RefreshToken(RefreshTokenCommand command);
        Task<Result> RevokeTokenAsync(string refreshToken);
    }
    
}
