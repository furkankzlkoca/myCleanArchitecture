using myCleanArchitecture.Application.ExpressionBuilders;
using myCleanArchitecture.Shared.FeatureModels.Products;
using myCleanArchitecture.Shared.FeatureModels.Products.Queries;

namespace myCleanArchitecture.Application.Features.Products.Queries.GetPage
{
    public class GetPageProductQueryHandler : IRequestHandler<GetPageProductQuery, PagingResult<ProductDto>>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public GetPageProductQueryHandler(IMapper mapper, IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<PagingResult<ProductDto>> Handle(GetPageProductQuery request, CancellationToken cancellationToken)
        {
            var pre = PredicateBuilder.New<Product>(true);
            if (request.PublicFilter is not null)
                pre.And(x => x.Name.Contains(request.PublicFilter, StringComparison.CurrentCultureIgnoreCase));
            

            return _mapper.Map<PagingResult<ProductDto>>(await _productRepository.GetPagingResultAsync(request.PagingParameters, pre));
        }
    }
}
