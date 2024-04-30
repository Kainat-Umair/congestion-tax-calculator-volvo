using System.Collections.Generic;
using System;

namespace CongestionTaxCalculator.WebAPI.Respositories
{
    public interface ICongestionTaxRepository
    {
        List<DateTime> GetTollFreeDates(string city);
        List<string> GetTollFreeVehicles(string city);
        int GetTollFee(DateTime date, string city);
        List<string> GetSingleChargeRule(string city);
    }
}
