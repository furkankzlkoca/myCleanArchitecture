

namespace myCleanArchitecture.Shared.FeatureModels.Categories.Queries
{
    public class GetByIdCategoryQuery : IRequest<ObjectResult<CategoryDto>>
    {
        public GetByIdCategoryQuery(Guid ıd)
        {
            Id = ıd;
        }

        public Guid Id { get; set; }
    }
}
