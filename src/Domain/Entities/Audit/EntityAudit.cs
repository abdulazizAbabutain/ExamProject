namespace Domain.Entities.Audit
{
    public class EntityAudit
    {
        public DateTimeOffset? LastModifiyedDate { get; set; }
        public DateTimeOffset? LastDeletedDate { get; set; }
        public DateTimeOffset CreataionDate { get; set; }
        public int VerstionNumber { get; set; }

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
