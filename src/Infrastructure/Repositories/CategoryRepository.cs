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
    }
}
