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
                .NotNull();

            When(e => e.ColorCode.IsNotNull(), () =>
            {
                RuleFor(e => e.ColorCode)
                    .Matches(RegexPattern.MatchHexCode)
                    .Length(6);
            });
        }
    }
}