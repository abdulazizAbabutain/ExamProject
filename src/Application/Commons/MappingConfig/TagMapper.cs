using Application.Tags.Commands.AddTag;
using Domain.Entities.EntityLookup;
using Mapster;

namespace Application.Commons.MappingConfig
{
    public class TagMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Tag, AddTagCommandResult>(); 
        }
    }
}
