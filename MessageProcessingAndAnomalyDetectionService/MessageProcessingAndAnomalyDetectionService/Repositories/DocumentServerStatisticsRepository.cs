using MessageProcessingAndAnomalyDetectionService.Repositories.Interface;
using MongoDB.Bson;
using MongoDB.Driver;
using ServerStatisticsProcessingProcess.Models;

namespace MessageProcessingAndAnomalyDetectionService.Repositories
{
    public class DocumentServerStatisticsRepository : IServerStatisticsRepository
    {
        private readonly IMongoCollection<ServerStatistics> _serverStatisticsCollection;
        public DocumentServerStatisticsRepository(string connectionString)
        {
            _serverStatisticsCollection = new MongoClient(connectionString).GetDatabase("Servers").GetCollection<ServerStatistics>("ServerStatistics");
        }
        public void Add(ServerStatistics serverStatistics)
        {
            _serverStatisticsCollection.InsertOne
                (
                    serverStatistics
                );
        }
    }
}
