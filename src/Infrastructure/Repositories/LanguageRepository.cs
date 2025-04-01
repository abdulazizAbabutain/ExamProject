using Domain.Entities.EntityLookup;
using Domain.Repositories;
using Infrastructure.Repositories.BaseRepository;

namespace Infrastructure.Repositories
{
    public class LanguageRepository : BaseRepository<Language>, ILanguageRepository
    {
        public LanguageRepository(string databasePath) 
            : base(databasePath, nameof(Language))
        {
        }


        public bool IsLangCodeExists(string code)
            => GetCollection().Exists(e => e.Code == code);

        public Guid GetLanguageReference(string code)
            => GetCollection().Find(e => e.Code.Equals(code)).Select(e => e.Id).FirstOrDefault();

    }
}
