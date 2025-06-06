


namespace myCleanArchitecture.Shared.FeatureModels.Categories.Commands
{
    public class UpdateCategoryCommand:IRequest<ObjectResult<CategoryDto>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
