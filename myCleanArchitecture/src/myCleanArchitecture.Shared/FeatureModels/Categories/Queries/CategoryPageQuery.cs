
namespace myCleanArchitecture.Shared.FeatureModels.Categories.Queries
{
    public class CategoryPageQuery:IRequest<PagingResult<CategoryDto>>
    {
        public PagingParameters PagingParameters{ get; set; }
    }
}
