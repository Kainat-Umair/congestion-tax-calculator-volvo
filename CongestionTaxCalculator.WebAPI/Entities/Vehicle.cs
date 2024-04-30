using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.WebAPI.Entities
{
    public class Vehicle

    {
        [JsonIgnore]
        public int VehicleId { get; set; }
        public string VehicleType { get; set; }
        [JsonIgnore]
        public bool IsTollFreeVehicle { get; set; }
        [JsonIgnore]
        public int CityId { get; set; }

        


    }
}