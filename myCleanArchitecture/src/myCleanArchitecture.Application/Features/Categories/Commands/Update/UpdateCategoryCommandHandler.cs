
using myCleanArchitecture.Shared.FeatureModels.Categories;
using myCleanArchitecture.Shared.FeatureModels.Categories.Commands;

namespace myCleanArchitecture.Application.Features.Categories.Commands.Update
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, ObjectResult<CategoryDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryRepository _categoryRepository;

        public UpdateCategoryCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
        }

        public async Task<ObjectResult<CategoryDto>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = await _categoryRepository.GetByIdAsync(request.Id);
            if (entity is null)
                return new ObjectResult<CategoryDto>(Meta.NotFound());

            entity.Name = request.Name.Trim();
            entity.Description = request.Description?.Trim() ?? string.Empty;

            var model = _categoryRepository.Update(entity);
            await _unitOfWork.SaveChangesAsync();
            if (model is not null)
                return new ObjectResult<CategoryDto>(Meta.Success(ConstantMessages.RecordUpdated), _mapper.Map<CategoryDto>(model));

            return new ObjectResult<CategoryDto>(Meta.Error());
        }
    }
}
