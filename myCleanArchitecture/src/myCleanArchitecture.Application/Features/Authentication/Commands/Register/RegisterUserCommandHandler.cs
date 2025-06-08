
using myCleanArchitecture.Application.Interfaces.Services;
using myCleanArchitecture.Shared.FeatureModels.Authentication;
using myCleanArchitecture.Shared.FeatureModels.Authentication.Commands;
using System.IdentityModel.Tokens.Jwt;

namespace myCleanArchitecture.Application.Features.Authentication.Commands.Register
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, ObjectResult<AuthModel>>
    {
        private readonly IAuthService _authService;
        public RegisterUserCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<ObjectResult<AuthModel>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            ObjectResult<AuthModel> user = await _authService.Register(request);
            return user;
        }        
    }
}
