

namespace myCleanArchitecture.Shared.Helpers.CustomModels
{
    public class JWT
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public double? DurationDays { get; set; }
        public double? RefreshTokenDurationDays { get; set; }
    }
    public class JwtSettings
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public double AccessTokenExpiryInMinutes { get; set; }
        public double RefreshTokenExpiryInDays { get; set; }
    }

}
