namespace Domain.Enums
{
    public enum ReviewDuplicationStatus
    {
        Pending = 0,       
        Confirmed = 1,      
        FalsePositive = 2,  
        Ignored = 3,     
        ResolvedByUpdate = 4, 
        ResolvedByDelete = 5  
    }
}
