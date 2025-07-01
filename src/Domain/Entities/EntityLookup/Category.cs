using Domain.Auditing;
using Domain.Extentions;
using LiteDB;

namespace Domain.Entities.EntityLookup
{
    public class Category : EntityAudit
    {
        public Category(string name, string? description, Guid parentId, int level)
        {
            Id = Guid.CreateVersion7();
            Name = name;
            ParentId = parentId;
            Description = description;
            Level = level + 1;
            Created();
        }

        public Category(string name, string? description)
        {
            Id = Guid.CreateVersion7();
            Name = name;
            Description = description;
            Level = 1;
            Created();
        }

        private Category() { }  

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public Guid? ParentId { get; private set; }
        public int Level {  get; private set; }
        [BsonIgnore]
        public bool IsRoot => ParentId.IsNull() && Level == 1;
    }
}
