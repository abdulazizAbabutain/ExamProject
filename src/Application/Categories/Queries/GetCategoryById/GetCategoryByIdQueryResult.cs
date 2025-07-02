namespace Application.Categories.Queries.GetCategoryById;

public class GetCategoryByIdQueryResult
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public Guid? ParentId { get; set; }
    public bool IsRoot { get; set; }
    public int Level { get; set; }
    public DateTimeOffset? LastModifiedDate { get; set; }
    public DateTimeOffset? LastArchiveDate { get; set; }
    public DateTimeOffset CreationDate { get; set; }
    public bool IsArchived { get; set; }
    public int VersionNumber { get; set; }
    public bool HasChildren { get; set; }
}
