


namespace myCleanArchitecture.Shared.FeatureModels.Categories.Commands
{
    public class DeleteCategoryCommand:IRequest<Result>
    {
        public DeleteCategoryCommand(Guid ıd)
        {
            Id = ıd;
        }

        public Guid Id { get; set; }
    }
}
