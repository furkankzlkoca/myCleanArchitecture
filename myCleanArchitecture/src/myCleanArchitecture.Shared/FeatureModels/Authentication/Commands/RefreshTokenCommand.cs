
namespace myCleanArchitecture.Shared.FeatureModels.Authentication.Commands
{
    public class RefreshTokenCommand : IRequest<ObjectResult<AuthModel>>
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}
