using CongestionTaxCalculator.WebAPI.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CongestionTaxCalculator.WebAPI.Models
{
    public class CongestionTaxRequest
    {

        [Required]
        public string VehicleType { get; set;}
        [Required]
        public DateTime[] CheckInDateTime { get; set; }
        [Required]
        public string CityName { get;set; }

      


    }
}
