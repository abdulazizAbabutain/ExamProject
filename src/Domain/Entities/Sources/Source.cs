using Domain.Auditing;
using Domain.Enums;
using Domain.Extentions;

namespace Domain.Entities.Sources
{
    public class Source : EntityAudit
    {
        public Source(SourceType type, string title, string? description,bool hasAttachment,string fileExtension,string filePath, IEnumerable<Guid>? tags)
        {
            Id = Guid.CreateVersion7();
            Type = type;
            Title = title;
            Description = description;
            Tags = tags.IsNotNull() ? tags.ToList() : null;
            FileExtension = fileExtension;
            FilePath = filePath;
            HasAttachment = hasAttachment;
            Created();
        }

        private Source() { }

        public void UpdateSource(SourceType type, string title, string? description, IEnumerable<Guid>? tags)
        {
            Type = type;
            Title = title;
            Description = description;
            Tags = tags.IsNotNull() ? tags.ToList() : null;
        }

        public void RemoveTag(Guid id)
        {
            Tags.Remove(id);

            if (Tags.Count == 0)
                Tags = null;
        }

        public void AddMetadata(string filedName, string value, bool isRequired, FiledType filedType)
        {
            Metadata ??= new List<Metadata>();
            var metadata = new Metadata(filedName, value, isRequired, filedType);
            Metadata.Add(metadata);
        }

        public void AddMetadata(Metadata metadata)
        {
            Metadata ??= new List<Metadata>();
            Metadata.Add(metadata);
        }


        public void AddMetadata(IEnumerable<Metadata> metadata)
        {
            Metadata ??= new List<Metadata>();
            Metadata.AddRange(metadata);
        }


        public void AddReference(SourceReference reference)
        {
            References ??= new List<SourceReference>();
            References.Add(reference);
        }

        public Guid Id { get; private set; }
        public SourceType Type { get; private set; }
        public string Title { get; private set; }
        public string? Description { get; private set; }
        public List<Guid>? Tags { get; private set; }
        public bool HasAttachment { get; private set; }
        public string? FileExtension { get; private set; }
        public string? FilePath { get; private set; }
        public List<Metadata>? Metadata { get; private set; }
        public List<SourceReference>? References { get; private set; }
    }
}
