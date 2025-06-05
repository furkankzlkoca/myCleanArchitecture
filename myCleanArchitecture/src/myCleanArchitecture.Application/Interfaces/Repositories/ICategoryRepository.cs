

using myCleanArchitecture.Application.Interfaces.Repositories.Base;
using myCleanArchitecture.Domain.Models;

namespace myCleanArchitecture.Application.Interfaces.Repositories
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task<bool> IsNameExist(string name);
        Task<bool> IsNameExist(string name, Guid id);
    }
}
