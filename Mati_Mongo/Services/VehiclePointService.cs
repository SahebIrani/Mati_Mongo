using Mati_Mongo.Configuration;
using Mati_Mongo.Models;

using MongoDB.Bson;
using MongoDB.Driver;

namespace Mati_Mongo.Services;

public class VehiclePointService : IVehiclePointService
{
    private readonly IMongoCollection<VehiclePointMongo> _vehiclePointMongoCollection;
    private readonly IMongoCollection<BsonDocument> _vehiclePointMongoBsonCollection;

    public VehiclePointService(IMongoConfiguration _settings)
    {
        var client = new MongoClient(_settings.ConnectionString);
        var database = client.GetDatabase(_settings.DatabaseName);

        _vehiclePointMongoCollection = database.GetCollection<VehiclePointMongo>(_settings.VehiclePointMongoCollectionName);
        _vehiclePointMongoBsonCollection = database.GetCollection<BsonDocument>(_settings.VehiclePointMongoCollectionName);
    }

    public async ValueTask TestAsync(CancellationToken cancellationToken = default)
    {
        // Untyped Documents
        var data = new BsonDocument("Longitude", 13.13);
        await _vehiclePointMongoBsonCollection.InsertOneAsync(data, cancellationToken: cancellationToken);
        var list = await _vehiclePointMongoBsonCollection.FindAsync(data, cancellationToken: cancellationToken);
        var list2 = await _vehiclePointMongoBsonCollection.Find(data).ToListAsync(cancellationToken);

        // Typed Documents
        await _vehiclePointMongoCollection.InsertOneAsync(new VehiclePointMongo { Longitude = 17 }, cancellationToken: cancellationToken);
    }

    public async ValueTask<List<VehiclePointMongo>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await _vehiclePointMongoCollection.Find(c => true).ToListAsync(cancellationToken);

    public async ValueTask<VehiclePointMongo> GetByIdAsync(string id, CancellationToken cancellationToken = default) =>
        await _vehiclePointMongoCollection.Find(c => c.Id == id).FirstOrDefaultAsync(cancellationToken);

    public async ValueTask<VehiclePointMongo> CreateAsync(VehiclePointMongo VehiclePointMongo, CancellationToken cancellationToken = default)
    {
        await _vehiclePointMongoCollection.InsertOneAsync(VehiclePointMongo, cancellationToken: cancellationToken);
        return VehiclePointMongo;
    }

    public async ValueTask<long> CountAsync(CancellationToken cancellationToken = default) =>
        await _vehiclePointMongoCollection.CountDocumentsAsync(_ => true, cancellationToken: cancellationToken);

    public async ValueTask CreateManyAsync(IEnumerable<VehiclePointMongo> VehiclePointMongo, CancellationToken cancellationToken = default) =>
        await _vehiclePointMongoCollection.InsertManyAsync(VehiclePointMongo, cancellationToken: cancellationToken);

    public async ValueTask UpdateAsync(string id, VehiclePointMongo VehiclePointMongo, CancellationToken cancellationToken = default) =>
        await _vehiclePointMongoCollection.ReplaceOneAsync(c => c.Id == id, VehiclePointMongo, cancellationToken: cancellationToken);

    public async ValueTask DeleteAsync(string id, CancellationToken cancellationToken = default) =>
        await _vehiclePointMongoCollection.DeleteOneAsync(c => c.Id == id, cancellationToken);
}