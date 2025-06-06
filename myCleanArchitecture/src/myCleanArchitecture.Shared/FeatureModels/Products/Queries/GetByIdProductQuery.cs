namespace myCleanArchitecture.Shared.FeatureModels.Products.Queries
{
    public class GetByIdProductQuery : IRequest<ObjectResult<ProductDto>>
    {
        public GetByIdProductQuery(Guid ıd)
        {
            Id = ıd;
        }

        public Guid Id { get; set; }
    }
}
