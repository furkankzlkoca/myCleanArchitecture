

using myCleanArchitecture.Shared.FeatureModels.Products.Commands;

namespace myCleanArchitecture.Application.Features.Products.Commands.Delete
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;

        public DeleteProductCommandHandler(IUnitOfWork unitOfWork, IProductRepository productRepository)
        {
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
        }

        public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var entity = await _productRepository.GetByIdAsync(request.Id);
            if (entity is null)
                return new Result(Meta.NotFound());

            _productRepository.Remove(entity);
            await _unitOfWork.SaveChangesAsync();

            return new Result(Meta.Success(ConstantMessages.RecordDeleted));
        }
    }
}
