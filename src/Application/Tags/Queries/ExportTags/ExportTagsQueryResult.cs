namespace Application.Tags.Queries.ExportTags
{
    public class ExportTagsQueryResult
    {
        public string FileName { get; set; }
        public string ContentType { get; set; } 
        public byte[] FileContent { get; set; } = Array.Empty<byte>();
    }
}
