namespace myCleanArchitecture.Shared.FeatureModels.Products.Commands
{
    public class UpdateProductCommand:IRequest<ObjectResult<ProductDto>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public Guid CategoryId { get; set; }
    }
}
