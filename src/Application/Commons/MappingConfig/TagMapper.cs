using Application.Commons.SharedModelResult;
using Application.Lookups.Queries.Tags.GetAllTags;
using Application.Tags.Commands.AddTag;
using Application.Tags.Queries.AutoCompleteTags;
using Application.Tags.Queries.GetTagDetails;
using Domain.Entities.EntityLookup;
using Domain.Extentions;
using Mapster;

namespace Application.Commons.MappingConfig
{
    public class TagMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Tag, AddTagCommandResult>()
                .Map(src => src.NeedReview, dest => dest.DuplicationReview.IsNotNull() && dest.DuplicationReview.IsDuplicated);

            config.NewConfig<Tag, TagResult>()
                .Map(src => src.ColorCode, dest => dest.BackgroundColorCode);

            config.NewConfig<Tag, GetTagDetailsQueryResult>()
                .Map(src => src.NeedReview, dest => dest.DuplicationReview.IsNotNull() && dest.DuplicationReview.IsDuplicated)
                .Map(src => src.ReviewResult, dest => dest.DuplicationReview);



            config.NewConfig<Tag, GetAllTagsQueryResult>()
                .Map(src => src.NeedReview, dest => dest.DuplicationReview.IsNotNull() && dest.DuplicationReview.IsDuplicated);


            config.NewConfig<Tag, AutoCompleteTagsQueryResult>();
        }
    }
}
