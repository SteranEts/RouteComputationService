using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouteComputationService
{
    public class Data
    {
        public float mqtt_time { get; set; }
        public DateTime mqtt_time_of_update { get; set; }
        public float external_time { get; set; }
        public DateTime external_time_of_update { get; set; }
        
    }
}
