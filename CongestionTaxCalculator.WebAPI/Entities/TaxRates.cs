using System;

namespace CongestionTaxCalculator.WebAPI.Entities
{
    public class TaxRates
    {
        public int PkId { get; set; }
        public DateTime DateTime { get; set; } 
        public int TaxRate { get; set;}
        public int CityId { get; set; }
        public int VehicleId { get; set; }
    }
}
