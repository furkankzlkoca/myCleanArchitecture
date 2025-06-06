

using AutoMapper;
using MediatR;
using myCleanArchitecture.Shared.FeatureModels.Categories;
using myCleanArchitecture.Shared.FeatureModels.Categories.Commands;



namespace myCleanArchitecture.Application.Features.Categories.Commands.Create
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, ObjectResult<CategoryDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryRepository _categoryRepository;

        public CreateCategoryCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
        }

        public async Task<ObjectResult<CategoryDto>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var model = _mapper.Map<Category>(request);
            _categoryRepository.Add(model);
            await _unitOfWork.SaveChangesAsync();

            if (model is not null)
                return new ObjectResult<CategoryDto>(Meta.Success(ConstantMessages.RecordCreated), _mapper.Map<CategoryDto>(model));

            return new ObjectResult<CategoryDto>(Meta.Error());
        }
    }
}