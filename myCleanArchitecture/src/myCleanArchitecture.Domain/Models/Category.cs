

using myCleanArchitecture.Domain.Common;

namespace myCleanArchitecture.Domain.Models
{
    public class Category : DetailedBaseEntity<Guid>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; } 
    }
}
