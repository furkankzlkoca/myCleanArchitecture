

using Microsoft.IdentityModel.Tokens;
using myCleanArchitecture.Application.Interfaces.Services;
using myCleanArchitecture.Shared.FeatureModels.Authentication;
using myCleanArchitecture.Shared.FeatureModels.Authentication.Commands;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace myCleanArchitecture.Application.Features.Authentication.Commands.RefreshToken
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, ObjectResult<AuthModel>>
    {
        private readonly IAuthService _authService;
        public RefreshTokenCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<ObjectResult<AuthModel>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            ObjectResult<AuthModel> dnn = await _authService.RefreshToken(request);
            return dnn;            
        }
    }
}
