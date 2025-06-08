namespace myCleanArchitecture.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Category> Category { get; }
        DbSet<Product> Product { get; }
    }
}
