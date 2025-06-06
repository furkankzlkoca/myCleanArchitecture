


namespace myCleanArchitecture.Shared.FeatureModels.Categories.Commands
{
    public class CreateCategoryCommand:IRequest<ObjectResult<CategoryDto>>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
