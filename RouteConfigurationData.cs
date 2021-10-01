using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouteComputationService
{
    public class RouteConfigurationData
    {

        public string detector { get; set; }
        public int[] east_lane { get; set; }
        public int[] west_lane { get; set; }
        public float coordX { get; set; }
        public float coordY { get; set; }

    }
}
