using myCleanArchitecture.Shared.FeatureModels.Categories;
using myCleanArchitecture.Shared.FeatureModels.Categories.Commands;
using myCleanArchitecture.Shared.FeatureModels.Categories.Queries;
using myCleanArchitecture.Shared.Results;
using myCleanArchitecture.UI.Services.Base;

namespace myCleanArchitecture.UI.Services
{
    public interface ICategoryService
    {
        Task<ObjectResult<CategoryDto>> Create(CreateCategoryCommand model);
        Task<ObjectResult<CategoryDto>> Update(UpdateCategoryCommand model);
        Task<ObjectResult<CategoryDto>> Get(Guid id);
        Task<ListResult<CategoryDto>> GetAll();
        Task<Result> Delete(Guid id);
    }
    public class CategoryService : ICategoryService
    {
        private readonly string ControllerName = "Category";
        private readonly IHttpClientHelper _httpClientHelper;
        public CategoryService(IHttpClientHelper httpClientHelper)
        {
            _httpClientHelper = httpClientHelper;
        }

        public async Task<ObjectResult<CategoryDto>> Create(CreateCategoryCommand model)
        {
            return await _httpClientHelper.PostAsync<ObjectResult<CategoryDto>>(ControllerName + "/Create", model);
        }

        public async Task<Result> Delete(Guid id)
        {
            return await _httpClientHelper.DeleteAsync<Result>(ControllerName + "/Delete/" + id);
        }

        public async Task<ObjectResult<CategoryDto>> Get(Guid id)
        {
            return await _httpClientHelper.GetAsync<ObjectResult<CategoryDto>>(ControllerName + "/Get/" + id);
        }

        public async Task<ListResult<CategoryDto>> GetAll()
        {
            return await _httpClientHelper.GetAsync<ListResult<CategoryDto>>(ControllerName + "/GetAll");
        }

        public async Task<ObjectResult<CategoryDto>> Update(UpdateCategoryCommand model)
        {
            return await _httpClientHelper.PutAsync<ObjectResult<CategoryDto>>(ControllerName + "/Update", model);
        }
    }
}
