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
                .Map(src => src.ColorCode, dest => dest.BackgroundColorCode);

            config.NewConfig<Tag, GetTagDetailsQueryResult>();


            config.NewConfig<Tag, GetAllTagsQueryResult>();


            config.NewConfig<Tag, AutoCompleteTagsQueryResult>();
        }
    }
}
