using CongestionTaxCalculator.WebAPI.Entities;
using System;

namespace CongestionTaxCalculator.Services
{
    public interface ICongestionTaxService
    {
        int GetTax(string vehicleType, DateTime[] dates, string cityName);
        
    }
}
