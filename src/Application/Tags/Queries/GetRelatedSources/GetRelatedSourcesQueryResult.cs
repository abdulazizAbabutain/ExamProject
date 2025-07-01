using Application.Commons.Attributes;
using Domain.Enums;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Application.Tags.Queries.GetRelatedSources;

public class GetRelatedSourcesQueryResult
{
    [SwaggerSchema(Description = "An UUID unique id for source", ReadOnly = true, Format = "UUID", Nullable = false, Title = "Source Unique Identifier")]
    [SwaggerExample("01979c44-b446-7380-ad8b-df6b2a2be0bc")]
    public Guid Id { get; set; }
    [SwaggerSchema(Description = "An type of a source", ReadOnly = true, Format = "UUID", Nullable = false, Title = "Source Unique Identifier")]
    [SwaggerExample(SourceType.Article)]
    [StringLength(50, MinimumLength = 1)]
    public SourceType Type { get; set; }
    [SwaggerSchema(Description = "A source Title", Nullable = false, Title = "Source title")]
    [SwaggerExample("Intro programming")]
    [StringLength(100, MinimumLength = 1)]
    public string Title { get; set; }
    [SwaggerSchema(Description = "flag if the source is archived", Nullable = false, Title = "Source archived flag")]
    [SwaggerExample(false)]
    public bool IsArchived { get; set; }
}
