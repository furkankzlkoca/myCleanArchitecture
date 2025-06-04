

using myCleanArchitecture.Domain.Common;

namespace myCleanArchitecture.Domain.Models
{
    public class Product : DetailedBaseEntity<Guid>
    {
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        public virtual Category? Category { get; set; }
    }
}
