namespace Domain.Entities.EntityLookup
{
    public class Tag 
    {          
        public Tag(string name) 
        {
            Id = Guid.CreateVersion7();
            Name = name;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
    }
}
