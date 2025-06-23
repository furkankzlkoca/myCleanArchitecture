using myCleanArchitecture.Shared.FeatureModels.Products;
using myCleanArchitecture.Shared.FeatureModels.Products.Commands;
using myCleanArchitecture.Shared.Results;
using myCleanArchitecture.UI.Services.Base;

namespace myCleanArchitecture.UI.Services
{
    public interface IProductService
    {
        Task<ObjectResult<ProductDto>> Create(CreateProductCommand model);
        Task<ObjectResult<ProductDto>> Update(UpdateProductCommand model);
        Task<ObjectResult<ProductDto>> Get(int id);
        Task<ListResult<ProductDto>> GetAll();
        Task<ListResult<ProductDto>> GetByCategoryId(int categoryId);
        Task<Result> Delete(int id);
    }
    public class ProductService : IProductService
    {
        private readonly string ControllerName = "Product";
        private readonly IHttpClientHelper _httpClientHelper;

        public ProductService(IHttpClientHelper httpClientHelper)
        {
            _httpClientHelper = httpClientHelper;
        }

        public async Task<ObjectResult<ProductDto>> Create(CreateProductCommand model)
        {
            return await _httpClientHelper.PostAsync<ObjectResult<ProductDto>>(ControllerName + "/Create", model);
        }

        public async Task<Result> Delete(int id)
        {
            return await _httpClientHelper.DeleteAsync<Result>(ControllerName + "/Delete/" + id);
        }

        public async Task<ObjectResult<ProductDto>> Get(int id)
        {
            return await _httpClientHelper.GetAsync<ObjectResult<ProductDto>>(ControllerName + "/Get/" + id);
        }

        public async Task<ListResult<ProductDto>> GetAll()
        {
            return await _httpClientHelper.GetAsync<ListResult<ProductDto>>(ControllerName + "/GetAll");
        }

        public async Task<ListResult<ProductDto>> GetByCategoryId(int categoryId)
        {
            return await _httpClientHelper.GetAsync<ListResult<ProductDto>>(ControllerName + "/GetByCategoryId/" + categoryId);
        }

        public async Task<ObjectResult<ProductDto>> Update(UpdateProductCommand model)
        {
            return await _httpClientHelper.PutAsync<ObjectResult<ProductDto>>(ControllerName + "/Update", model);
        }
    }
}
