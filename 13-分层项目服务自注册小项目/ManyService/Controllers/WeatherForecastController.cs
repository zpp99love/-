using ClassLibrary1;
using ClassLibrary2;
using Microsoft.AspNetCore.Mvc;

namespace ManyService.Controllers
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
        private readonly Class1 _class1;
        private readonly Class2 _class2;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, Class1 class1, Class2 class2)
        {
            _logger = logger;
            _class1 = class1;
            _class2 = class2;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            _class1.Hello();
            _class2.Hello();

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}