using Domain.Extentions;

namespace Domain.Entities.EntityLookup
{
    public class Category 
    {
        public Category(string name, Guid parentId, int level)
        {
            Id = Guid.CreateVersion7();
            Name = name;
            ParentId = parentId;
            Level = level+1;
        }

        public Category(string name)
        {
            Id = Guid.CreateVersion7();
            Name = name;
            Level = 1;
        }

        private Category() { }  

        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? ParentId { get; set; }
        public int Level {  get; set; }


        public bool IsRoot => ParentId.IsNull() && Level == 1;

    }
}
