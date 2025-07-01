using Application.Categories.Commands.AddCategory;
using Domain.Entities.EntityLookup;
using Mapster;

namespace Application.Commons.MappingConfig
{
    public class CategoryMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Category, AddCategoryCommandResult>();
        }
    }
}
