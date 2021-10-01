using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace RouteComputationService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<string> Get()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var URL = "https://jsonplaceholder.typicode.com/todos/1";
            var response = await httpClient.GetAsync(URL);
            var test = await response.Content.ReadAsStringAsync();
            var json = JsonConvert.DeserializeObject<Product>(test);

            return test;
        }
    }
}
