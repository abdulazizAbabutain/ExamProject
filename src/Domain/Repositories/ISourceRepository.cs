using Domain.Entities.Sources;
using Domain.Repositories.RepositoryBase;

namespace Domain.Repositories
{
    public interface ISourceRepository : IBaseRepository<Source>
    {
        bool IsExist(Guid id);
        bool IsNotExist(Guid id);
    }
}
