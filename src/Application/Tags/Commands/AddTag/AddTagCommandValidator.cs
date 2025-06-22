using FluentValidation;

namespace Application.Tags.Commands.AddTag
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
