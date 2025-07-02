using Application.Categories.Commands.AddCategory;
using Application.Categories.Queries.GetAllCategory;
using Application.Categories.Queries.GetCategoryById;
using Domain.Entities.EntityLookup;
using Mapster;

namespace Application.Commons.MappingConfig
{
    public class CategoryMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Category, AddCategoryCommandResult>();
            config.NewConfig<Category, GetCategoryByIdQueryResult>();
            config.NewConfig<Category, GetAllCategoryQuery>();
        }
    }
}
