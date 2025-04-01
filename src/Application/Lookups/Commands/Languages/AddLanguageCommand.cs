using MediatR;

namespace Application.Lookups.Commands.Languages
{
    public class AddLanguageCommand : IRequest
    {
        public required string Code { get; set; }
        public required string DisplayName { get; set; }
    }
}
