using Application.Commons.Models.Results;
using Domain.Entities.EntityLookup;

namespace Application.Commons.Services
{
    public interface ITagService
    {
        Result<Tag> AddTag(string name, string? colorCode = null);
        IEnumerable<Tag> GetAllTags();
        Result UpdateTag(Guid id, string name, string? colorCode = null);
        void DeleteTag(Guid id);
        Result ArchiveTag(Guid id);
        Result ArchiveAllTag();
        void UnArchiveTag(Guid id);
    }
}
