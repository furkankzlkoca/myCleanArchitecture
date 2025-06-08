

using myCleanArchitecture.Application.Interfaces.Services;
using myCleanArchitecture.Shared.FeatureModels.Authentication.Commands;

namespace myCleanArchitecture.Application.Features.Authentication.Commands.Logout
{
    public class LogoutUserCommandHandler : IRequestHandler<LogoutUserCommand>
    {
        private readonly IAuthService _authService;
        public LogoutUserCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task Handle(LogoutUserCommand request, CancellationToken cancellationToken)
        {
            var dnn = await _authService.RevokeTokenAsync(request.RefreshToken);
        }
    }
}
