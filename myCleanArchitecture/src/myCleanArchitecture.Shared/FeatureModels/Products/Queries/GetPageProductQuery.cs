namespace myCleanArchitecture.Shared.FeatureModels.Products.Queries
{
    public class GetPageProductQuery : IRequest<PagingResult<ProductDto>>
    {
        public PagingParameters PagingParameters { get; set; }
        public string? PublicFilter { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
