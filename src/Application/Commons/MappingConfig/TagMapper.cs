using Application.Commons.SharedModelResult;
using Application.Lookups.Queries.Tags.GetAllTags;
using Application.Tags.Commands.AddTag;
using Application.Tags.Queries.AutoCompleteTags;
using Application.Tags.Queries.GetTagDetails;
using Domain.Entities.EntityLookup;
using Mapster;

namespace Application.Commons.MappingConfig
{
    public class TagMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Tag, AddTagCommandResult>();

            config.NewConfig<Tag, TagResult>()
                .Map(src => src.ColorCode, dest => dest.ColorHexCode);

            config.NewConfig<Tag, GetTagDetailsQueryResult>();


            config.NewConfig<Tag, GetAllTagsQueryResult>()
                .Map(src => src.ColorCode, dest => dest.ColorHexCode)
                .Map(src => src.ColorCategory , dest => dest.ColorGroup);


            config.NewConfig<Tag, AutoCompleteTagsQueryResult>();
        }
    }
}
