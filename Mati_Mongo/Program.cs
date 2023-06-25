using Mati_Mongo.Configuration;
using Mati_Mongo.Services;

using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<MongoConfiguration>(
    builder.Configuration.GetSection(nameof(MongoConfiguration)));

builder.Services.AddSingleton<IMongoConfiguration>(provider =>
    provider.GetRequiredService<IOptions<MongoConfiguration>>().Value);

builder.Services.AddSingleton<IVehiclePointService, VehiclePointService>();
//BsonClassMap.RegisterClassMap<VehiclePointService>();

builder.Services.AddControllersWithViews()
    .AddJsonOptions(
        options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

await app.RunAsync();
