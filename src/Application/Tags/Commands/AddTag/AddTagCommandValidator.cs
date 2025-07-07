using Domain.Constants;
using Domain.Extentions;
using FluentValidation;

namespace Application.Tags.Commands.AddTag
{
    public class AddTagCommandValidator : AbstractValidator<AddTagCommand>
    {
        public AddTagCommandValidator()
        {
            RuleFor(e => e.Name)
                .MaximumLength(MaxLength.TAG_NAME_MAX_LENGTH)
                .NotNull();

            When(e => e.BackgroundColorCode.IsNotNull(), () =>
            {
                RuleFor(e => e.BackgroundColorCode)
                    .Matches(RegexPattern.MatchHexCode)
                    .Length(MaxLength.TAG_COLOR_CODE_MAX_LENGTH);
            });

            When(e => e.TextColorCode.IsNotNull(), () =>
            {
                RuleFor(e => e.TextColorCode)
                    .Matches(RegexPattern.MatchHexCode)
                    .Length(MaxLength.TAG_COLOR_CODE_MAX_LENGTH);
            });
        }
    }
}