
using myCleanArchitecture.Shared.FeatureModels.Products;
using myCleanArchitecture.Shared.FeatureModels.Products.Commands;

namespace myCleanArchitecture.Application.Features.Products.Commands.Create
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ObjectResult<ProductDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;

        public CreateProductCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IProductRepository productRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
        }

        public async Task<ObjectResult<ProductDto>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var model = _mapper.Map<Product>(request);
            model.Name = model.Name.Trim();
            model.Description = model.Description?.Trim() ?? string.Empty;
            _productRepository.Add(model);
            await _unitOfWork.SaveChangesAsync();

            if (model is not null)
                return new ObjectResult<ProductDto>(Meta.Success(ConstantMessages.RecordCreated), _mapper.Map<ProductDto>(model));

            return new ObjectResult<ProductDto>(Meta.Error());
        }
    }
}
