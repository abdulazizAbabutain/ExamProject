using Domain.Entities.EntityLookup;
using Domain.Repositories;
using Infrastructure.Repositories.BaseRepository;

namespace Infrastructure.Repositories
{
    public class TagRepository : BaseRepository<Tag>, ITagRepository
    {
        public TagRepository(string databasePath) 
            : base(databasePath, nameof(Tag))
        {

        }

        public bool IsExist(string name)
            => GetCollection().Exists(e => e.Name.Equals(name));

        public IEnumerable<Guid> GetTagsRefrence(IEnumerable<string> tags)
          => GetCollection().Find(t => tags.Contains(t.Name)).Select(e => e.Id);


        public Guid GetTagReference(string tag)
          => GetCollection().FindOne(t => t.Name.Equals(tag)).Id;

        public IEnumerable<string> GetAllAvailableTags(IEnumerable<string> tags)
        {
            return _collection.Find(e => tags.Contains(e.Name)).Select(t => t.Name).ToList();
        }

    }
}
