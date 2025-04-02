using Domain.Enums;
using Domain.Lookups;

namespace Application.Lookups.Queries.Sources.GetAllSources
{
    public class GetAllSourceQueryResult
    {
        public Guid Id { get; set; }
        public SourceTypeLookup Type { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string URL { get; set; }
        public List<TagDto>? Tags { get; set; }
    }

    public class TagDto
    {
        public string Name { get; set; }
        public string ColorCode { get; set; }
    }
    
}
