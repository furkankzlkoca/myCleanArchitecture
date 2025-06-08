
namespace myCleanArchitecture.Shared.FeatureModels.Authentication.Commands
{
    public class LogoutUserCommand: IRequest
    {
        public string RefreshToken { get; set; } = string.Empty;
    }
}
