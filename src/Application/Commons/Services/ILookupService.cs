using Domain.Entities.EntityLookup;

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
