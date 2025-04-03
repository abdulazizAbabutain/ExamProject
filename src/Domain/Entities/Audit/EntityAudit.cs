namespace Domain.Entities.Audit
{
    public class EntityAudit
    {

        public DateTimeOffset? LastModifiedDate { get; protected set; }
        public DateTimeOffset? LastDeletedDate { get; protected set; }
        public DateTimeOffset CreationDate { get; protected set; }
        public bool IsDeleted { get; protected set; }
        public int VersionNumber { get; protected set; }

        protected void Created()
        {
            CreationDate = DateTimeOffset.Now;
            VersionNumber = 1;
            IsDeleted = false;
        }
        protected void Updated()
        {
            LastDeletedDate = DateTimeOffset.Now;
            VersionNumber++;
        }

    }
}
