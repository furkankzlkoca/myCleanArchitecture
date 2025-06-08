
namespace myCleanArchitecture.Shared.FeatureModels.Authentication
{
    public class AuthModel
    {
        public bool IsAuthenticated { get; set; }
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? ExpiresOn { get; set; }
        public DateTime? RefreshTokenExpiration { get; set; }
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
}
