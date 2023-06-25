using Mati_Mongo.Models;

namespace Mati_Mongo.Services
{
    public interface IVehiclePointService
    {
        ValueTask<VehiclePointMongo> CreateAsync(VehiclePointMongo VehiclePointMongo, CancellationToken cancellationToken = default);
        ValueTask CreateManyAsync(IEnumerable<VehiclePointMongo> VehiclePointMongo, CancellationToken cancellationToken = default);
        ValueTask<long> CountAsync(CancellationToken cancellationToken = default);
        ValueTask DeleteAsync(string id, CancellationToken cancellationToken = default);
        ValueTask<List<VehiclePointMongo>> GetAllAsync(CancellationToken cancellationToken = default);
        ValueTask<VehiclePointMongo> GetByIdAsync(string id, CancellationToken cancellationToken = default);
        ValueTask TestAsync(CancellationToken cancellationToken = default);
        ValueTask UpdateAsync(string id, VehiclePointMongo VehiclePointMongo, CancellationToken cancellationToken = default);
    }
}