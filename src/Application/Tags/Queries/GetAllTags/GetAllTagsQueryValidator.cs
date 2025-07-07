using Application.Commons.Validation.Pagination;
using Application.Lookups.Queries.Tags.GetAllTags;
using Domain.Constants;
using Domain.Extentions;
using FluentValidation;

namespace Application.Tags.Queries.GetAllTags
{
    public class GetAllTagsQueryValidator : AbstractValidator<GetAllTagsQuery>
    {
        public GetAllTagsQueryValidator()
        {
            Include(new PageRequestValidator());

            When(e => !string.IsNullOrWhiteSpace(e.Search), () =>
            {
                RuleFor(e => e.Search)
                    .MaximumLength(MaxLength.TAG_NAME_MAX_LENGTH);
            });


            When(e => e.BackgroundColorGroup.IsNotNull(), () =>
            {
                RuleFor(e => e.BackgroundColorGroup)
                    .IsInEnum();
            });
        }
    }
}
