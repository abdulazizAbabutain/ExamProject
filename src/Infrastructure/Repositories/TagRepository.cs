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

        public bool IsExist(Guid id)
          => GetCollection().Exists(e => e.Id.Equals(id));

        public bool IsNotExist(Guid id)
        => !GetCollection().Exists(e => e.Id.Equals(id));

        public IEnumerable<Guid> GetTagsReference(IEnumerable<string> tags)
          => GetCollection().Find(t => tags.Contains(t.Name)).Select(e => e.Id);


        public Guid GetTagReference(string tag)
          => GetCollection().FindOne(t => t.Name.Equals(tag)).Id;

        public void ArchiveTag(Guid id)
        {
            if (IsNotExist(id))
                return;
            
            var tag = GetCollection().FindById(id);
            tag.ArchiveTag();
            Update(tag);
        }

        public void UnArchiveTag(Guid id)
        {
            if (IsNotExist(id))
                return;

            var tag = GetCollection().FindById(id);
            tag.UnArchiveTag();
            Update(tag);
        }
        public IEnumerable<string> GetAllAvailableTags(IEnumerable<string> tags)
        {
            return _collection.Find(e => tags.Contains(e.Name)).Select(t => t.Name).ToList();
        }

    }
}
