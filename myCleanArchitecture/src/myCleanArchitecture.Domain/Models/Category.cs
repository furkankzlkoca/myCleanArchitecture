

using myCleanArchitecture.Domain.Common;

namespace myCleanArchitecture.Domain.Models
{
    public class Category : DetailedBaseEntity<Guid>
    {
        public string Name { get; set; }

    }

}
