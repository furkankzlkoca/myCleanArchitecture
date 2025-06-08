

using myCleanArchitecture.Application.Interfaces.Services;
using myCleanArchitecture.Shared.FeatureModels.Authentication;
using myCleanArchitecture.Shared.FeatureModels.Authentication.Commands;
using System.IdentityModel.Tokens.Jwt;

namespace myCleanArchitecture.Application.Features.Authentication.Commands.Login
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, ObjectResult<AuthModel>>
    {
        private readonly IAuthService _authService;
        public LoginUserCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<ObjectResult<AuthModel>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            ObjectResult<AuthModel> dnn = await _authService.Login(request);
            return dnn;
        }        
    }
}
