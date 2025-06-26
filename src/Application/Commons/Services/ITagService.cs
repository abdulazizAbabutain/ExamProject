using Application.Commons.Models.Results;
using Domain.Entities.EntityLookup;

namespace Application.Commons.Services
{
    public interface ITagService
    {
        public Result<Tag> AddTag(string name, string? colorCode = null);
        IEnumerable<Tag> GetAllTags();
        IEnumerable<Guid> GetTagsReference(IEnumerable<string> tags);
        public void UpdateTag(Guid id, string name, string? colorCode = null);
        void DeleteTag(Guid id);
        public void ArchiveTag(Guid id);
        public void UnArchiveTag(Guid id);
    }
}
