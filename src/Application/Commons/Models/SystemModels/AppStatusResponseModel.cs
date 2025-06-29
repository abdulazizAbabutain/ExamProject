namespace Application.Commons.Models.SystemModels
{
    public class AppStatusResponseModel
    {
        public string AppVersion { get; set; }
        public TimeSpan Uptime { get; set; }
        public string OSVersion { get; set; }
        public string CpuArchitecture { get; set; }
        public string MemoryUsageMB { get; set; }
        public string DatabaseSizeBytes { get; set; }
        public List<CollectionStorageStat> Collections { get; set; }
    }
}
