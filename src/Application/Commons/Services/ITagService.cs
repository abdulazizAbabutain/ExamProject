using Application.Commons.Models.Icons;
using Application.Commons.Models.Results;
using Domain.Entities.EntityLookup;
using Domain.Enums;

namespace Application.Commons.Services
{
    public interface ITagService
    {
        Result<Tag> AddTag(string name, string? backgroundColorCode = null, string textColorCode = null, IconCommand icon = null);
        IEnumerable<Tag> GetAllTags();
        Result UpdateTag(Guid id, string name, string backgroundColorCode, string textColorCode);
        Result DeleteTag(Guid id);
        Result ArchiveTag(Guid id);
        Result ArchiveAllTag();
        Result UnArchiveTag(Guid id);
        Result ResolveDuplication(Guid id, ReviewDuplicationStatus reviewStatus);
    }
}
