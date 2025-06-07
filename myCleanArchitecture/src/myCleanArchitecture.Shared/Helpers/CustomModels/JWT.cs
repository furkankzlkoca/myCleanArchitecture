

namespace myCleanArchitecture.Shared.Helpers.CustomModels
{
    public class JWT
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string DurationDays { get; set; }
        public string RefreshTokenDurationDays { get; set; }
    }
}
