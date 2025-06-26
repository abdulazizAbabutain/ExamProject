using Application.Commons.Models.Results;
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
