﻿using Domain.Entities.EntityLookup;
using Domain.Repositories.RepositoryBase;

namespace Domain.Repositories
{
    public interface ITagRepository : IBaseRepository<Tag>
    {
        bool IsExist(string name);
        public bool IsExist(string name, Guid id);
        bool IsExist(Guid id);
        bool IsExist(IEnumerable<Guid> ids);
        bool IsNotExist(Guid id);
        bool IsNotExist(IEnumerable<Guid> ids);
        IEnumerable<Guid> GetNotFoundTags(IEnumerable<Guid> inputIds);
        public void ArchiveTag(Guid id);
        public void UnArchiveTag(Guid id);
        IEnumerable<Guid> GetTagsReference(IEnumerable<string> tags);
        Guid GetTagReference(string tag);
        IEnumerable<string> GetAllAvailableTags(IEnumerable<string> tags);
        IEnumerable<Guid> GetDuplication(string normalizedName);
        IEnumerable<Guid> GetDuplication(string normalizedName, Guid excludeId);
    }
}
