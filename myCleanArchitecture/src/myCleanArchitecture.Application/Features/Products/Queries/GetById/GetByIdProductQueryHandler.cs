

using myCleanArchitecture.Shared.FeatureModels.Products;
using myCleanArchitecture.Shared.FeatureModels.Products.Queries;

namespace myCleanArchitecture.Application.Features.Products.Queries.GetById
{
    public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQuery, ObjectResult<ProductDto>>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public GetByIdProductQueryHandler(IMapper mapper, IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<ObjectResult<ProductDto>> Handle(GetByIdProductQuery request, CancellationToken cancellationToken)
        {
            var entity = await _productRepository.GetByIdAsync(request.Id);
            if (entity is null)
                return new ObjectResult<ProductDto>(Meta.NotFound());

            return new ObjectResult<ProductDto>(Meta.Success(), _mapper.Map<ProductDto>(entity));
        }
    }
}
