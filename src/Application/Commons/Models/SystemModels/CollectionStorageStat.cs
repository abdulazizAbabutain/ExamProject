namespace Application.Commons.Models.SystemModels;

public class CollectionStorageStat
{
    public string Name { get; set; } = default!;
    public int DocumentCount { get; set; }
    public string AverageDocumentSizeBytes { get; set; }
    public string EstimatedTotalSizeBytes { get; set; }
    public int IndexCount { get; set; }
}
