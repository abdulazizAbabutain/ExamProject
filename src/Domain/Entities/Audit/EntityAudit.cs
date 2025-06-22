namespace Domain.Entities.Audit
{
    public class EntityAudit
    {
        public DateTimeOffset? LastModifiedDate { get; protected set; }
        public DateTimeOffset? LastArchiveDate { get; protected set; }
        public DateTimeOffset CreationDate { get; protected set; }
        public bool IsArchived { get; protected set; }
        public int VersionNumber { get; protected set; }

        protected void Created()
        {
            CreationDate = DateTimeOffset.Now;
            VersionNumber = 1;
            IsArchived = false;
        }
        protected void Updated()
        {
            LastModifiedDate = DateTimeOffset.Now;
            VersionNumber++;
        }

        protected void Archive()
        {
            IsArchived = true;
            LastArchiveDate = DateTimeOffset.Now;
            Updated();
        }

        protected void UnArchive()
        {
            IsArchived = false;
            Updated();
        }
    }
}
