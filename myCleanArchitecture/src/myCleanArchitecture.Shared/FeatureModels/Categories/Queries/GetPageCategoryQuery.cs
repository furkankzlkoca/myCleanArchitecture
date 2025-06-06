

namespace myCleanArchitecture.Shared.FeatureModels.Categories.Queries
{
    public class GetPageCategoryQuery : IRequest<PagingResult<CategoryDto>>
    {
        public PagingParameters PagingParameters { get; set; }
        public bool? IsActive { get; set; }
        public string? PublicFilter { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
