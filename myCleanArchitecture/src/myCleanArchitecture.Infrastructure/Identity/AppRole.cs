
using Microsoft.AspNetCore.Identity;

namespace myCleanArchitecture.Infrastructure.Identity
{
    public class AppRole : IdentityRole<Guid>// TODO: must be add IDetailedBaseEntity<Guid> to AppUser
    {
    }
}
