using Microsoft.AspNetCore.Mvc;
using _03_WEB_beforetest.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;
using _03_WEB_beforetest.Modules;

namespace _03_WEB_beforetest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }


public static class PCComponentsEndpoints
{
	public static void MapPCComponentsEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/PCComponents").WithTags(nameof(PCComponents));

        group.MapGet("/", async (ComponentsDbContext db) =>
        {
            return await db.Components.ToListAsync();
        })
        .WithName("GetAllPCComponents")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<PCComponents>, NotFound>> (string componentsname, ComponentsDbContext db) =>
        {
            return await db.Components.AsNoTracking()
                .FirstOrDefaultAsync(model => model.ComponentsName == componentsname)
                is PCComponents model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetPCComponentsById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (string componentsname, PCComponents pCComponents, ComponentsDbContext db) =>
        {
            var affected = await db.Components
                .Where(model => model.ComponentsName == componentsname)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.ComponentsName, pCComponents.ComponentsName)
                  .SetProperty(m => m.ComponentUsage, pCComponents.ComponentUsage)
                  .SetProperty(m => m.Description, pCComponents.Description)
                  );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdatePCComponents")
        .WithOpenApi();

        group.MapPost("/", async (PCComponents pCComponents, ComponentsDbContext db) =>
        {
            db.Components.Add(pCComponents);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/PCComponents/{pCComponents.ComponentsName}",pCComponents);
        })
        .WithName("CreatePCComponents")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (string componentsname, ComponentsDbContext db) =>
        {
            var affected = await db.Components
                .Where(model => model.ComponentsName == componentsname)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeletePCComponents")
        .WithOpenApi();
    }
}}
