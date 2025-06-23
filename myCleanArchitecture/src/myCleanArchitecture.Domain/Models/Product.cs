

using myCleanArchitecture.Domain.Common;

namespace myCleanArchitecture.Domain.Models
{
    public class Product : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public Guid CategoryId { get; set; }
        public virtual Category? Category { get; set; }
    }
}
