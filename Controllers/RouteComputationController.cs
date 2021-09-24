using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RouteComputationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteComputationController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public RouteComputationController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        // GET: api/<ValuesController>
        [HttpGet]
        public string Get()
        {
            float mqtt = calculMQTT().Result;
            float googleData = getGoogleMaps().Result;
            string json = JsonConvert.SerializeObject(new
            {
                results = new List<Data>()
    {
        new Data { id = "MQTT", tempsItineraire = mqtt, timeStamp = DateTime.Now },
        new Data { id = "GoogleMaps", tempsItineraire = googleData, timeStamp = DateTime.Now }
    }
            });
            return json;
        }


        public async Task<float> calculMQTT()
        {
            float distanceMQTT = 0;
            var httpClient = _httpClientFactory.CreateClient();
            var URL = "https://jsonplaceholder.typicode.com/todos/";
            var response = await httpClient.GetAsync(URL);
            var test = await response.Content.ReadAsStringAsync();
            List<Product> products = JsonConvert.DeserializeObject<List<Product>>(test);

            foreach (Product p in products)
            {
                distanceMQTT += p.Id;
            }
            return distanceMQTT;
        }

        public async Task<float> getGoogleMaps()
        {
            float distanceMQTT = 0;
            var httpClient = _httpClientFactory.CreateClient();
            var URL = "https://jsonplaceholder.typicode.com/todos/";
            var response = await httpClient.GetAsync(URL);
            var test = await response.Content.ReadAsStringAsync();
            List<Product> products = JsonConvert.DeserializeObject<List<Product>>(test);

            foreach (Product p in products)
            {
                distanceMQTT += p.Id;
            }
            return distanceMQTT;
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
