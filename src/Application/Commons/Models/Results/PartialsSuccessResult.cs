namespace Application.Commons.Models.Results
{
    public class PartialsSuccessResult
    {
        public List<Guid> Successed { get; set; } = new();
        public List<string> Errors { get; set; } = new();
    }
}
