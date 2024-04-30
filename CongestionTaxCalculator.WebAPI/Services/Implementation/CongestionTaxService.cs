using CongestionTaxCalculator.Services;
using CongestionTaxCalculator.WebAPI.Respositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace CongestionTaxCalculator.WebAPI.Services.Implementation
{
    public class CongestionTaxService : ICongestionTaxService
    {

        private readonly ICongestionTaxRepository congestionTaxRepository;
        private readonly ILogger<CongestionTaxService> logger;

        public CongestionTaxService(ILogger<CongestionTaxService> logger, ICongestionTaxRepository congestionTaxRepository)
        {
            this.congestionTaxRepository = congestionTaxRepository;
            this.logger = logger;
        }

        public int GetTax(string vehicleType, DateTime[] dates, string cityName)
        {
            try
            { 
                Array.Sort(dates);
                DateTime intervalStart = dates[0];
                int totalFee = 0;

                if (dates.Length > 1)
                {
                    foreach (DateTime date in dates)
                    {
                        int nextFee = GetTollFee(date, vehicleType, cityName);
                        int tempFee = GetTollFee(intervalStart, vehicleType, cityName);

                        TimeSpan timeDifference = date - intervalStart;
                        long diffInMin = (long)timeDifference.TotalMinutes;

                        if (diffInMin <= 60) 
                        {
                            if (nextFee > tempFee)
                            {
                                totalFee -= tempFee; 
                                tempFee = nextFee; 
                            }
                            
                            else if(nextFee < tempFee)
                            {
                                totalFee -= tempFee; 
                            }
                            totalFee += tempFee; 
                        }
                        else 
                        {
                           
                            totalFee += nextFee;
                        }

                        intervalStart = date;
                    }

                    if (IsSingleChargeRuleCity(cityName) && totalFee > 60)
                    {
                        totalFee = 60;
                    }

                    return totalFee;
                }
                else 
                {
                    return GetTollFee(dates[0], vehicleType, cityName);
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Error in GetTax method with VehicleType: {vehicleType}, Date: {string.Join(", ", dates)}, City: {cityName.ToLower()}", ex);
                return -1;
            }
        }



        private bool IsSingleChargeRuleCity(string cityName)
        {
            List<string> singleChargeRuleCity = congestionTaxRepository.GetSingleChargeRule(cityName.ToLower());
            return singleChargeRuleCity.Contains(cityName.ToLower());

        }
        private bool IsTollFreeDate(DateTime date, string cityName)
        {

            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) return true;
            List<DateTime> tollFreeDates = congestionTaxRepository.GetTollFreeDates(cityName.ToLower());
            return tollFreeDates.Contains(new DateTime(date.Year, date.Month, date.Day));

        }
        private bool IsTollFreeVehicle(string vehicleType, string cityName)
        {
            List<string> tollFreeVehicles = congestionTaxRepository.GetTollFreeVehicles(cityName.ToLower());
            return tollFreeVehicles.Contains(vehicleType.ToLower());

        }

        private int GetTollFee(DateTime date, string vehicleType, string cityName)
        {

            if (IsTollFreeDate(date, cityName.ToLower()) || IsTollFreeVehicle(vehicleType.ToLower(), cityName.ToLower())) return 0;
            return congestionTaxRepository.GetTollFee(date, cityName.ToLower());

        }


    }
}
