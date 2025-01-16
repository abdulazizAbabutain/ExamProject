namespace Domain.Entities.Audit
{
    public class EntityAudit
    {
        public DateTimeOffset? LastModifiyedDate { get; set; }
        public DateTimeOffset? LastDeletedDate { get; set; }
        public required DateTimeOffset CreataionDate { get; set; }
        public required int VerstionNumber { get; set; }
    }
}
