using FluentValidation;

namespace Application.Lookups.Commands.Tags.AddTag
{
    public class AddTagCommandValidator : AbstractValidator<AddTagCommand>
    {
        public AddTagCommandValidator()
        {
            RuleFor(e => e.Name)
                .NotNull();

            RuleFor(e => e.ColorCode)
                .Must(e => e.StartsWith("#"))
                .Length(7);
        }
    }
}
