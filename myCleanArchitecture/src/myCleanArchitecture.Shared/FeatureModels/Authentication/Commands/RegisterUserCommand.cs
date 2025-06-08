
namespace myCleanArchitecture.Shared.FeatureModels.Authentication.Commands
{
    public class RegisterUserCommand : IRequest<ObjectResult<AuthModel>>
    {
        public RegisterUserCommand(string email, string password, string userName)
        {
            Email = email;
            Password = password;
            UserName = userName;
        }

        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
    }
}
