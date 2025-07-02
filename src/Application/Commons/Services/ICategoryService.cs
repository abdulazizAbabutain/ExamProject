using Application.Commons.Models.Results;
using Domain.Entities.EntityLookup;

namespace Application.Commons.Services
{
    public interface ICategoryService
    {
        Result<Category> AddCategory(string name, string description, Guid? parentId);
        Result UpdateCategory(Guid categoryId, string name, string description);
    }
}
