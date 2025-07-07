using Domain.Constants;
using FluentValidation;

namespace Application.Tags.Commands.UpdateTag;

public class UpdateTagCommandValidator : AbstractValidator<UpdateTagCommand>
{
    public UpdateTagCommandValidator()
    {
        RuleFor(e => e.Name).NotEmpty();
        RuleFor(e => e.BackgroundColorCode).NotEmpty().Matches(RegexPattern.MatchHexCode).Length(MaxLength.TAG_COLOR_CODE_MAX_LENGTH);
        RuleFor(e => e.TextColorCode).NotEmpty().Matches(RegexPattern.MatchHexCode).Length(MaxLength.TAG_COLOR_CODE_MAX_LENGTH);
    }
}
