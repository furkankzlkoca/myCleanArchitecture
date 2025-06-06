

using MediatR;
using myCleanArchitecture.Shared.FeatureModels.Categories.Commands;

namespace myCleanArchitecture.Application.Features.Categories.Commands.Delete
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryRepository _categoryRepository;

        public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork, ICategoryRepository categoryRepository)
        {
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
        }

        public async Task<Result> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = await _categoryRepository.GetByIdAsync(request.Id);
            if (entity is null)
                return new Result(Meta.NotFound());

            _categoryRepository.Remove(entity);
            await _unitOfWork.SaveChangesAsync();
            return new Result(Meta.Success(ConstantMessages.RecordDeleted));
        }
    }
}
