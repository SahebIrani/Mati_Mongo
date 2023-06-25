using System.Diagnostics;

using Bogus;

using Mati_Mongo.Models;
using Mati_Mongo.Services;

using Microsoft.AspNetCore.Mvc;

namespace Mati_Mongo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> IndexAsync([FromServices] IVehiclePointService vehiclePointService, CancellationToken cancellationToken = default)
        {
            //var fakeData = vehiclePointService.TestAsync(cancellationToken);

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

            var entity = await vehiclePointService.CreateAsync(vehiclePointMongo, cancellationToken);

            var sw = Stopwatch.StartNew();

            var dataCount = await vehiclePointService.CountAsync(cancellationToken);

            sw.Stop();

            var totalSeconds = sw.Elapsed.TotalMilliseconds / 1000.2;

            var message = $"Data row count: {dataCount:n} and Executed time: {totalSeconds:f4} seconds";

            ViewData["Message"] = message;

            ViewData["RowId"] = entity!.Id!.ToString();

            _logger.LogInformation(message);

            _logger.LogDebug(
                 "Data row count: {dataRowCount} and Executed time: {totalSeconds} seconds ..",
                 dataCount, totalSeconds
             );

            var countData = 1_000_000;

            var fakerSinjulMati = new Faker<VehiclePointMongo>()
               .RuleFor(m => m.Longitude, f => f.Random.Float(13, 130))
               .RuleFor(m => m.Latitude, f => f.Random.Float(13, 130))
               .RuleFor(m => m.Altitude, f => f.Random.Short(13, 130))
               .RuleFor(m => m.Speed, f => f.Random.Short(13, 130))
               .RuleFor(m => m.Temperature, f => f.Random.Float(13, 130))
               .RuleFor(m => m.Satellite, f => f.Random.Byte(13, 130))
               .RuleFor(m => m.Batt, f => f.Random.Byte(13, 130))
               .RuleFor(m => m.Status, f => f.Random.Byte(13, 130))
               .RuleFor(m => m.Humidity, f => f.Random.Short(13, 130))
               .RuleFor(m => m.DateTime, f => f.Date.Between(
                   new DateTime(1990, 1, 1),
                   new DateTime(2023, 6, 25))
               )
            ;

            var entities = fakerSinjulMati.Generate(countData);

            await vehiclePointService.CreateManyAsync(entities, cancellationToken);

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}