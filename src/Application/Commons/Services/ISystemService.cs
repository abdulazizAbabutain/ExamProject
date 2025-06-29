using Application.Commons.Models.SystemModels;

namespace Application.Commons.Services
{
    public interface ISystemService
    {
        public long GetDatabaseFileSizeBytes();
        public List<CollectionStorageStat> AnalyzeCollections(int sampleSize = 10);
    }
}
