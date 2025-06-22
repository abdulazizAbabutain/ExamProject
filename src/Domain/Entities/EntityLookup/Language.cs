using Domain.Entities.Audit;

namespace Domain.Entities.EntityLookup
{
    /// <summary>
    /// translate 
    /// </summary>
    public class Language 
    {
        #region constractor
        public Language(string code, string displayName)
        {
            Id = Guid.CreateVersion7();
            Code = code;
            DisplayName = displayName;
        }
        #endregion


        public Guid Id { get; private set; }
        public string Code { get; private set; }
        public string DisplayName { get; private set; }
    }
}
