namespace Domain.Entities.Audit
{
    public class EntityAudit
    {
        public DateTimeOffset? LastModifiyedDate { get; private set; }
        public DateTimeOffset? LastDeletedDate { get; private set; }
        public DateTimeOffset CreataionDate { get; private set; }
        public int VerstionNumber { get; private set; }

        protected void Created()
        {
            CreataionDate = DateTimeOffset.Now;
            VerstionNumber = 1;
        }
        protected void Updateed()
        {
            LastModifiyedDate = DateTimeOffset.Now;
            VerstionNumber++;
        }

    }
}
