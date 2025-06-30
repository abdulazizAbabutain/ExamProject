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

        public bool IsExist(string name, Guid id)
            => GetCollection().Exists(e => e.Name.Equals(name) && e.Id != id);

        public bool IsExist(Guid id)
            => GetCollection().Exists(e => e.Id.Equals(id));

        public bool IsExist(IEnumerable<Guid> ids)
            => GetCollection().Exists(e => ids.Contains(e.Id));

        public bool IsNotExist(Guid id)
            => !GetCollection().Exists(e => e.Id.Equals(id));

        public bool IsNotExist(IEnumerable<Guid> ids)
            => !GetCollection().Exists(e => ids.Contains(e.Id));

        public IEnumerable<Guid> GetNotFoundTags(IEnumerable<Guid> inputIds)
        {
            var existingIds = GetCollection().Find(e => inputIds.Contains(e.Id)).Select(e => e.Id).ToList();
            return inputIds.Except(existingIds).ToList();
        }

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
