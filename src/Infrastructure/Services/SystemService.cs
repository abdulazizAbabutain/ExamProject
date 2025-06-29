using Application.Commons.Extensions;
using Application.Commons.Models.SystemModels;
using Application.Commons.Services;
using LiteDB;
using System.Text;

namespace Infrastructure.Services;

public class SystemService : ISystemService
{
    private readonly string _databasePath;
    private readonly LiteDatabase _db;


    public SystemService(string databasePath)
    {
        _databasePath = databasePath;
        _db = new LiteDatabase(_databasePath);
    }

    public long GetDatabaseFileSizeBytes()
    {
        var file = new FileInfo("database.db");
        return file.Exists ? file.Length : 0;
    }

    public List<CollectionStorageStat> AnalyzeCollections(int sampleSize = 10)
    {
        var stats = new List<CollectionStorageStat>();
        var indexCollection = _db.GetCollection<BsonDocument>("__indexes"); // system index collection

        foreach (var collectionName in _db.GetCollectionNames())
        {
            var collection = _db.GetCollection(collectionName);
            var totalCount = collection.Count();

            double averageDocSize = 0;
            if (totalCount > 0)
            {
                var documents = collection.Find(Query.All(), limit: sampleSize).ToList();
                var totalSize = documents.Sum(doc =>
                {
                    var bson = BsonMapper.Global.ToDocument(doc);
                    var bsonBytes = BsonSerializer.Serialize(bson); 
                    return bsonBytes.Length;
                });

                averageDocSize = totalSize / (double)documents.Count;
            }

            var indexCount = indexCollection.Count(x => x["collection"].AsString == collectionName);

            stats.Add(new CollectionStorageStat
            {
                Name = collectionName,
                DocumentCount = totalCount,
                IndexCount = indexCount,
                AverageDocumentSizeBytes = StringExtension.ToReadableSize(averageDocSize),
                EstimatedTotalSizeBytes = StringExtension.ToReadableSize(averageDocSize * totalCount)
            });
        }

        return stats;
    }
}
