
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace myCleanArchitecture.Infrastructure.Identity
{
    public class AppUser : IdentityUser<Guid>// TODO: must be add IDetailedBaseEntity<Guid> to AppUser
    {
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
    public class RefreshToken : BaseEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
        public DateTime ExpiryDate { get; set; }
        public bool IsUsed { get; set; }
        public bool IsRevoked { get; set; }
        public string JwtId { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public virtual AppUser User { get; set; } = null!;

        [NotMapped]
        public bool IsActive => DateTime.Now < ExpiryDate && !IsRevoked;
    }
}
