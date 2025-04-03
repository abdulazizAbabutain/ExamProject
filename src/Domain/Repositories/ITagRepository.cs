using Domain.Entities.EntityLookup;
using Domain.Repositories.RepositoryBase;

namespace Domain.Repositories
{
    public interface ITagRepository : IBaseRepository<Tag>
    {
        bool IsExist(string name);
        bool IsExist(Guid id);
        bool IsNotExist(Guid id);
        IEnumerable<Guid> GetTagsRefrence(IEnumerable<string> tags);
        Guid GetTagReference(string tag);
        IEnumerable<string> GetAllAvailableTags(IEnumerable<string> tags);
    }
}
