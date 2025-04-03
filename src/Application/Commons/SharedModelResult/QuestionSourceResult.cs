using Domain.Lookups;

namespace Application.Commons.SharedModelResult
{
    public class QuestionSourceResult
    {
        public Guid Id { get; set; }
        public SourceTypeLookup Type { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string URL { get; set; }
    }
}
