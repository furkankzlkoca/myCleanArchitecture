
using Microsoft.AspNetCore.Identity;

namespace myCleanArchitecture.Infrastructure.Identity
{
    public class AppUser : IdentityUser<Guid>// TODO: must be add IDetailedBaseEntity<Guid> to AppUser
    {
    }
}
