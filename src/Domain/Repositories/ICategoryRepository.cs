using Domain.Entities.EntityLookup;
using Domain.Repositories.RepositoryBase;

namespace Domain.Repositories
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        public bool IsNotExists(Guid id);
    }
}
