using Microsoft.EntityFrameworkCore.Query;
using myCleanArchitecture.Application.ExpressionBuilders;
using myCleanArchitecture.Shared.FeatureModels.Products;
using myCleanArchitecture.Shared.FeatureModels.Products.Queries;


namespace myCleanArchitecture.Application.Features.Products.Queries.GetList
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, ListResult<ProductDto>>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly Func<IQueryable<Product>, IIncludableQueryable<Product, object?>> _include;

        public GetProductsQueryHandler(IMapper mapper, IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _include = x => x.Include(x => x.Category);
        }

        public async Task<ListResult<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var pre = PredicateBuilder.New<Product>(true);
            // Add any filters based on request parameters here
            // any business logic can be applied here
            return new ListResult<ProductDto>(Meta.Success(), _mapper.Map<List<ProductDto>>(await _productRepository.GetListAsync(pre, x => x.Name, true, _include)));
        }
    }
}
