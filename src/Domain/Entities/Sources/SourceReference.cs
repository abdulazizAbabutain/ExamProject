using Domain.Auditing;
using Domain.Enums;

namespace Domain.Entities.Sources;

public class SourceReference : EntityAudit 
{
    public SourceReference(string? notes, Guid sourceId)
    {
        Id = Guid.CreateVersion7();
        Notes = notes;
        SourceId = sourceId;
        Created();
    }

    public void AddMetadata(string filedName, string value, FiledType filedType)
    {
        Metadata ??= new List<Metadata>();
        var metadata = new Metadata(filedName, value, filedType);
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
    public Guid SourceId { get; private set; }
    public List<Metadata>? Metadata { get; private set; }
}
