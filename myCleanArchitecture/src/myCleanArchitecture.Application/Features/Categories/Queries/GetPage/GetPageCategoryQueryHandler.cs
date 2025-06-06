

using myCleanArchitecture.Application.ExpressionBuilders;
using myCleanArchitecture.Shared.FeatureModels.Categories;
using myCleanArchitecture.Shared.FeatureModels.Categories.Queries;

namespace myCleanArchitecture.Application.Features.Categories.Queries.GetPage
{
    public class GetPageCategoryQueryHandler : IRequestHandler<GetPageCategoryQuery, PagingResult<CategoryDto>>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;

        public GetPageCategoryQueryHandler(IMapper mapper, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<PagingResult<CategoryDto>> Handle(GetPageCategoryQuery request, CancellationToken cancellationToken)
        {
            var pre = PredicateBuilder.New<Category>(true);
            if (request.PublicFilter is not null)
                pre.And(x => x.Name.Contains(request.PublicFilter, StringComparison.CurrentCultureIgnoreCase));

            if (request.IsActive is not null)
                pre.And(x => x.IsActive == request.IsActive);

            if (request.StartDate is not null)
                pre.And(x => x.Created >= request.StartDate);
            if (request.EndDate is not null)
                pre.And(x => x.Created <= request.EndDate.Value.AddDays(1).AddTicks(-1));

            return _mapper.Map<PagingResult<CategoryDto>>(await _categoryRepository.GetPagingResultAsync(request.PagingParameters, pre));
        }
    }
}
