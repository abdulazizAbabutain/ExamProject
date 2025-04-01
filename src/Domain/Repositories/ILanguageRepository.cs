using Domain.Entities.EntityLookup;
using Domain.Repositories.RepositoryBase;

namespace Domain.Repositories;

public interface ILanguageRepository : IBaseRepository<Language>
{
    bool IsLangCodeExists(string code);
    Guid GetLanguageReference(string code);
}
