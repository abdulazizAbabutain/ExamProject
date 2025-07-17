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
                .Map(dest => dest.NeedReview, src => src.DuplicationReview.IsNotNull() && src.DuplicationReview.IsDuplicated);

            config.NewConfig<Tag, TagResult>()
                .Map(dest => dest.ColorCode, src => src.BackgroundColorCode);

            config.NewConfig<Tag, GetTagDetailsQueryResult>()
                .Map(dest => dest.NeedReview, src => src.DuplicationReview.IsNotNull() && src.DuplicationReview.IsDuplicated)
                .Map(dest => dest.ReviewResult, src => src.DuplicationReview);



            config.NewConfig<Tag, GetAllTagsQueryResult>()
                .Map(dest => dest.NeedReview, src => src.DuplicationReview.IsNotNull() && src.DuplicationReview.IsDuplicated);


            config.NewConfig<Tag, AutoCompleteTagsQueryResult>();
        }
    }
}
