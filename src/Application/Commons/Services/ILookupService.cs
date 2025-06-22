using Domain.Entities.EntityLookup;
using Domain.Entities.Sources;

namespace Application.Commons.Services
{
    public interface ILookupService : IDisposable
    {
        #region language services 
        void AddLanguage(Language language);
        IEnumerable<Language> GetLanguages();
        Guid GetLanguageReference(string code);
        #endregion

        void AddTag(string name, string? colorCode = null);
        IEnumerable<Tag> GetAllTags();
        IEnumerable<Guid> GetTagsReference(IEnumerable<string> tags);
        public void UpdateTag(Guid id,string name, string? colorCode = null);
        void DeleteTag(Guid id);
        public void ArchiveTag(Guid id);
        public void UnArchiveTag(Guid id);
        #region Source services
        void AddSource(Source source);
        Source GetSource(Guid id);
        void UpdateSource(Source source);
        #endregion

        #region category service 
        void AddCategory(string name, Guid? parentId);
        #endregion
    }
}
