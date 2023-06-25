using Mati_Mongo.Models;
using Mati_Mongo.Services;

using Microsoft.AspNetCore.Mvc;

namespace Mati_Mongo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VehiclePointController : ControllerBase
{
    private readonly IVehiclePointService _vehiclePointService;

    public VehiclePointController(IVehiclePointService customerService)
    {
        _vehiclePointService = customerService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _vehiclePointService.GetAllAsync());
    }

    [HttpGet("{id:length(24)}")]
    public async Task<IActionResult> Get(string id)
    {
        var vehiclePointMongo = await _vehiclePointService.GetByIdAsync(id);
        if (vehiclePointMongo == null)
        {
            return NotFound();
        }
        return Ok(vehiclePointMongo);
    }

    [HttpGet(nameof(CreateFake))]
    public async Task<IActionResult> CreateFake()
    {
        var fakeData = _vehiclePointService.TestAsync();

        var vehiclePointMongo = new VehiclePointMongo
        {
            DateTime = DateTime.Now,
            Altitude = 1,
            Batt = 2,
            Humidity = 3,
            Latitude = 4,
            Satellite = 5,
            Speed = 6,
            Status = 7,
            Temperature = 8,
            Longitude = 13
        };

        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        await _vehiclePointService.CreateAsync(vehiclePointMongo);

        var data = await _vehiclePointService.GetAllAsync();

        return Ok(vehiclePointMongo.Id);
    }

    [HttpPost]
    public async Task<IActionResult> Create(VehiclePointMongo vehiclePointMongo)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        await _vehiclePointService.CreateAsync(vehiclePointMongo);
        return CreatedAtAction(nameof(Get), new { id = vehiclePointMongo.Id }, vehiclePointMongo);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, VehiclePointMongo customerIn)
    {
        var vehiclePointMongo = await _vehiclePointService.GetByIdAsync(id);
        if (vehiclePointMongo == null)
        {
            return NotFound();
        }

        customerIn.Id = vehiclePointMongo.Id;

        await _vehiclePointService.UpdateAsync(id, customerIn);
        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var vehiclePointMongo = await _vehiclePointService.GetByIdAsync(id);
        if (vehiclePointMongo == null)
        {
            return NotFound();
        }
        await _vehiclePointService.DeleteAsync(vehiclePointMongo.Id);
        return NoContent();
    }
}