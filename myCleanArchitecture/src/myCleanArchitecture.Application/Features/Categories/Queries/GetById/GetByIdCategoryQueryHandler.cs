
using myCleanArchitecture.Shared.FeatureModels.Categories;
using myCleanArchitecture.Shared.FeatureModels.Categories.Queries;

namespace myCleanArchitecture.Application.Features.Categories.Queries.GetById
{
    public class GetByIdCategoryQueryHandler : IRequestHandler<GetByIdCategoryQuery, ObjectResult<CategoryDto>>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;

        public GetByIdCategoryQueryHandler(IMapper mapper, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<ObjectResult<CategoryDto>> Handle(GetByIdCategoryQuery request, CancellationToken cancellationToken)
        {
            var entity = await _categoryRepository.GetByIdAsync(request.Id);
            if (entity is null)
                return new ObjectResult<CategoryDto>(Meta.NotFound());

            return new ObjectResult<CategoryDto>(Meta.Success(), _mapper.Map<CategoryDto>(entity));
        }
    }
}
