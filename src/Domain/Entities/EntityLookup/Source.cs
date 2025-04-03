using Domain.Entities.Audit;
using Domain.Enums;
using Domain.Extentions;

namespace Domain.Entities.EntityLookup
{
    public class Source : EntityAudit
    {
        public Source(SourceType type, string title, string? description, string uRL, IEnumerable<Guid>? tags)
        {
            Id = Guid.CreateVersion7();
            Type = type;
            Title = title;
            Description = description;
            URL = uRL;
            Tags = tags.IsNotNull() ? tags.ToList() : null;
            Created();
        }

        private Source() { }

        public void UpdateSource(SourceType type, string title, string? description, string uRL, IEnumerable<Guid>? tags)
        {
            Type = type;
            Title = title;
            Description = description;
            URL = uRL;
            Tags = tags.IsNotNull() ? tags.ToList() : null;
        }

        public void RemoveTag(Guid id)
        {
            Tags.Remove(id);
            
            if (Tags.Count == 0)
                Tags = null;
        }

        public Guid Id { get; private set; }
        public SourceType Type { get; private set; }
        public string Title { get; private set; }
        public string? Description { get; private set; }
        public string URL { get; private set; }
        public List<Guid>? Tags { get; private set; }
    }
}
