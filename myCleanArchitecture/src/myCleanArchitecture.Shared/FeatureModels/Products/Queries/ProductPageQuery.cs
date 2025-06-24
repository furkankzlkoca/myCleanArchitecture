
namespace myCleanArchitecture.Shared.FeatureModels.Products.Queries
{
    public class ProductPageQuery : IRequest<PagingResult<ProductDto>>
    {
        public PagingParameters PagingParameters{ get; set; }
    }
}
