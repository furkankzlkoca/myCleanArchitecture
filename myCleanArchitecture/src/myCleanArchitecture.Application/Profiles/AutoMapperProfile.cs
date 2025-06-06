

using myCleanArchitecture.Shared.FeatureModels.Categories;
using myCleanArchitecture.Shared.FeatureModels.Categories.Commands;
using myCleanArchitecture.Shared.FeatureModels.Products;
using myCleanArchitecture.Shared.FeatureModels.Products.Commands;

namespace myCleanArchitecture.Application.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Category
            CreateMap<CreateCategoryCommand, Category>();
            CreateMap<UpdateCategoryCommand, Category>();
            CreateMap<PagingResult<Category>, PagingResult<CategoryDto>>();
            CreateMap<Category, CategoryDto>();
            #endregion

            #region Product
            CreateMap<CreateProductCommand, Product>();
            CreateMap<UpdateProductCommand, Product>();
            CreateMap<PagingResult<Product>, PagingResult<ProductDto>>();
            CreateMap<Product, ProductDto>();
            #endregion
        }
    }
}
