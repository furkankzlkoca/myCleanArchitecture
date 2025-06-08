

namespace myCleanArchitecture.Shared.FeatureModels.Authentication.Commands
{
    public class LoginUserCommand:IRequest<ObjectResult<AuthModel>>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
