using Application.Commons.Attributes;
using Application.Commons.Models.Icons;
using Application.Commons.Models.Results;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace Application.Tags.Commands.AddTag
{
    [SwaggerSchema(Description = "a command for creating a new tag", Nullable = false, Required = ["name"])]
    public partial class AddTagCommand : IRequest<Result<AddTagCommandResult>>
    {
        [SwaggerSchema(Description = "the Value for tag", Nullable = false, Title = "Tag Color")]
        [SwaggerExample("Math-101")]
        public string Name { get; set; }
        [SwaggerSchema(Description = "Hexcode color format should start with #", Nullable = true, Title = "Tag Color")]
        [SwaggerExample("#1756a7")]
        public string? BackgroundColorCode { get; set; }
        [SwaggerSchema(Description = "Hexcode color format should start with #", Nullable = true, Title = "Tag Color")]
        [SwaggerExample("#1756a7")]
        public string? TextColorCode { get; set; }
        public IconCommand? IconCommand { get; set; }
    }
}
