
using myCleanArchitecture.Shared.FeatureModels.Products;
using myCleanArchitecture.Shared.FeatureModels.Products.Commands;

namespace myCleanArchitecture.Application.Features.Products.Commands.Update
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ObjectResult<ProductDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;

        public UpdateProductCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IProductRepository productRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
        }

        public async Task<ObjectResult<ProductDto>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var entity = await _productRepository.GetByIdAsync(request.Id);
            if (entity is null)
                return new ObjectResult<ProductDto>(Meta.NotFound());

            entity.Name = request.Name.Trim();
            entity.Description = request.Description?.Trim() ?? string.Empty;
            entity.Price = request.Price;
            entity.StockQuantity = request.StockQuantity;
            entity.CategoryId = request.CategoryId;

            var model = _productRepository.Update(entity);
            await _unitOfWork.SaveChangesAsync();
            if (model is not null)
                return new ObjectResult<ProductDto>(Meta.Success(ConstantMessages.RecordUpdated), _mapper.Map<ProductDto>(model));

            return new ObjectResult<ProductDto>(Meta.Error());
        }
    }
}
