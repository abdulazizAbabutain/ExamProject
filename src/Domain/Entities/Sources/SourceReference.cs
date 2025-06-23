using Domain.Enums;

namespace Domain.Entities.Sources
{
    public class SourceReference
    {
        public SourceReference(string? notes)
        {
            Id = Guid.CreateVersion7();
            Notes = notes;
        }

        public void AddMetadata(string filedName, string value, bool isRequired, FiledType filedType)
        {
            Metadata ??= new List<Metadata>();
            var metadata = new Metadata(filedName, value, isRequired, filedType);
            Metadata.Add(metadata);
        }

        public void AddMetadata(IEnumerable<Metadata> metadata)
        {
            Metadata ??= new List<Metadata>();
            Metadata.AddRange(metadata);
        }

        public void AddMetadata(Metadata metadata)
        {
            Metadata ??= new List<Metadata>();
            Metadata.Add(metadata);
        }

        public Guid Id { get; private set; }
        public string? Notes { get; private set; }
        public List<Metadata>? Metadata { get; private set; }
    }
}
