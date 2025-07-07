using Application.Commons.Models.Results;
using Domain.Entities.EntityLookup;

namespace Application.Commons.Services
{
    public interface ITagService
    {
        Result<Tag> AddTag(string name, string? backgroundColorCode = null, string textColorCode = null);
        IEnumerable<Tag> GetAllTags();
        Result UpdateTag(Guid id, string name, string backgroundColorCode, string textColorCode );
        Result DeleteTag(Guid id);
        Result ArchiveTag(Guid id);
        Result ArchiveAllTag();
        Result UnArchiveTag(Guid id);
    }
}
