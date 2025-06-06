namespace myCleanArchitecture.Shared.FeatureModels.Products.Commands
{
    public class DeleteProductCommand:IRequest<Result>
    {
        public DeleteProductCommand(Guid ıd)
        {
            Id = ıd;
        }

        public Guid Id { get; set; }
    }
}
