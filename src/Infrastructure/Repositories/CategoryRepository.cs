using Domain.Entities.EntityLookup;
using Domain.Repositories;
using Infrastructure.Repositories.BaseRepository;

namespace Infrastructure.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(string databasePath) 
            : base(databasePath, nameof(Category))
        {
        }

        public bool IsNotExists(Guid id)
        {
            return !_collection.Exists(e => e.Id == id);
        }
    }
}
