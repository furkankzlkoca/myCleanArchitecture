

using myCleanArchitecture.Application.ExpressionBuilders;
using myCleanArchitecture.Shared.FeatureModels.Categories;
using myCleanArchitecture.Shared.FeatureModels.Categories.Queries;

namespace myCleanArchitecture.Application.Features.Categories.Queries.GetList
{
    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, ListResult<CategoryDto>>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;

        public GetCategoriesQueryHandler(IMapper mapper, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<ListResult<CategoryDto>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var pre = PredicateBuilder.New<Category>(true);
            // Add any filters based on request parameters here
            // any business logic can be applied here
            return new ListResult<CategoryDto>(Meta.Success(), _mapper.Map<List<CategoryDto>>(await _categoryRepository.GetListAsync(pre, x => x.Name, true)));
        }
    }
}
