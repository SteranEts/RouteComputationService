using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using System.Threading;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RouteComputationService.Controllers
{
    [Route("api/routecomputation")]
    [ApiController]
    public class RouteComputationController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public RouteComputationController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        // GET: api/<ValuesController>
        [HttpPost]
        public string Get([FromBody] List<dataIntersection> item)
        {
            //List<dataIntersection> item = LoadIntersectionsComputation();
            float mqtt = calculMQTT(item);
            float googleData = getGoogleMaps().Result;
            
            string json = JsonConvert.SerializeObject(new Data { mqtt_time = mqtt, mqtt_time_of_update = DateTime.Now, external_time = googleData, external_time_of_update = DateTime.Now });
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.PostAsync("", json);
            return json;
        }

        public float calculMQTT(List<dataIntersection> item)
        {
            float tempsMqtt = 0;
            //var httpClient = _httpClientFactory.CreateClient();
            //var URL = "https://jsonplaceholder.typicode.com/todos/";
            //var response = await httpClient.GetAsync(URL);
            //var test = await response.Content.ReadAsStringAsync();
            //List<Product> products = JsonConvert.DeserializeObject<List<Product>>(test);

            foreach (dataIntersection p in item)
            {
                tempsMqtt += p.time_west;
            }
            return tempsMqtt;
        }

        public async Task<float> getGoogleMaps()
        {
            float distanceGoogle = 0;
            var httpClient = _httpClientFactory.CreateClient();
            var URL = "https://jsonplaceholder.typicode.com/todos/";
            var response = await httpClient.GetAsync(URL);
            var test = await response.Content.ReadAsStringAsync();
            List<Product> products = JsonConvert.DeserializeObject<List<Product>>(test);

            foreach (Product p in products)
            {
                distanceGoogle += p.Id;
            }
            return distanceGoogle;
        }
        public Item LoadJson()
        {
            Item item = null;
            using (StreamReader r = new StreamReader("mock.json"))
            {
                string json = r.ReadToEnd();
                item = JsonConvert.DeserializeObject<Item>(json);
            }
            return item;
        }
        public List<dataIntersection> LoadIntersectionsComputation()
        {
            List<dataIntersection> item = null;
            using (StreamReader r = new StreamReader("mockIntersectionsComputation.json"))
            {
                string json = r.ReadToEnd();
                item = JsonConvert.DeserializeObject<List<dataIntersection>>(json);
            }
            return item;
        }
        public List<RouteConfigurationData> LoadRouteConfigurationData()
        {
            List<RouteConfigurationData> item = null;
            using (StreamReader r = new StreamReader("RouteConfigurationData.json"))
            {
                string json = r.ReadToEnd();
                item = JsonConvert.DeserializeObject<List<RouteConfigurationData>>(json);
            }
            return item;
        }

        // GET api/<ValuesController>/5
        [Route("data")]
        [HttpGet, HttpHead]
        public string GetData()
        {
            List<RouteConfigurationData> item = LoadRouteConfigurationData();
            string json = JsonConvert.SerializeObject(item);
            Console.WriteLine("data");
            Response.StatusCode = 200;

            return json;
        }

        // POST api/<ValuesController>
        [Route("ping")]
        [HttpGet]
        public async Task<string> pingAsync()
        {
            var httpClient = _httpClientFactory.CreateClient();
            HttpResponseMessage response =
   await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Head, "https://equipe08-routecomputation.herokuapp.com/api/routecomputation/data"));
            return JsonConvert.SerializeObject(response);
        }

    }
}
