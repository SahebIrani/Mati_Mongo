namespace Mati_Mongo.Configuration
{
    public class MongoConfiguration : IMongoConfiguration
    {
        public string VehiclePointMongoCollectionName { get; set; } = null!;
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
    }
    public interface IMongoConfiguration
    {
        string VehiclePointMongoCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}