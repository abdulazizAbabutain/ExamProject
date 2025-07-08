namespace Domain.Auditing;


/// <summary>
/// Represents a base class for auditable entities, providing standard audit properties such as creation date,
/// last modification timestamp, version tracking, and soft-archive support.
/// </summary>
public class EntityAudit
{
    /// <summary>
    /// Gets or sets the unique identifier of the entity.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets the timestamp of the last modification made to the entity.
    /// </summary>
    public DateTimeOffset? LastModifiedDate { get; protected set; }

    /// <summary>
    /// Gets the timestamp when the entity was archived.
    /// </summary>
    public DateTimeOffset? LastArchiveDate { get; protected set; }

    /// <summary>
    /// Gets the timestamp when the entity was created.
    /// </summary>
    public DateTimeOffset CreationDate { get; protected set; }

    /// <summary>
    /// Gets a value indicating whether the entity is archived (soft-deleted).
    /// </summary>
    public bool IsArchived { get; protected set; }

    /// <summary>
    /// Gets the current version number of the entity. Incremented with each update.
    /// </summary>
    public int VersionNumber { get; protected set; }

    /// <summary>
    /// Sets the entity as newly created. Initializes <see cref="CreationDate"/>, sets <see cref="VersionNumber"/> to 1,
    /// and ensures the entity is not archived.
    /// </summary>
    protected void Created()
    {
        CreationDate = DateTimeOffset.Now;
        VersionNumber = 1;
        IsArchived = false;
    }

    /// <summary>
    /// Updates the <see cref="LastModifiedDate"/> to the current timestamp and increments the <see cref="VersionNumber"/>.
    /// Should be called whenever the entity is modified.
    /// </summary>
    protected void Updated()
    {
        LastModifiedDate = DateTimeOffset.Now;
        VersionNumber++;
    }

    /// <summary>
    /// Archives the entity by setting <see cref="IsArchived"/> to <c>true</c> and updating the <see cref="LastArchiveDate"/>.
    /// Also updates the modification timestamp and version.
    /// </summary>
    protected void Archive()
    {
        IsArchived = true;
        LastArchiveDate = DateTimeOffset.Now;
        Updated();
    }

    /// <summary>
    /// Unarchives the entity by setting <see cref="IsArchived"/> to <c>false</c> and updating the modification state.
    /// </summary>
    protected void UnArchive()
    {
        IsArchived = false;
        Updated();
    }
}

