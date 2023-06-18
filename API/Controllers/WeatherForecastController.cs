using Microsoft.AspNetCore.Mvc;
using DataLayer.Models;
using DataLayer.Repositories;
using DataLayer;

namespace API.Controllers
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
        private readonly AddressRepository addressRepository;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, AddressRepository address)
        {
            addressRepository = address;
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
        [HttpGet("/test")]
        public IEnumerable<Address> GetAddresses(int pageNumber, int pageSize)
        {
            Paging page = new Paging()
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return addressRepository.GetAddress(x => x.AccountId == 2, page);
        }
    }
}