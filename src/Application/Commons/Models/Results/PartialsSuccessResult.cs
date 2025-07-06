namespace Application.Commons.Models.Results
{
    public class PartialsSuccessResult
    {
        public List<Guid> Successed { get; set; } = new();
        public Dictionary<string,string[]> Errors { get; set; } = new();
    }
}
