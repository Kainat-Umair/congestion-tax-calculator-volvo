using CongestionTaxCalculator.WebAPI.Respositories;
using CongestionTaxCalculator.WebAPI.Services.Implementation;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Xunit;

namespace CongestionTaxCalculator.Test.Services
{
    public class CongestionTaxServiceTest
    {
        [Fact]
        public void GetTax_ValidResponse_ReturnsIntegerValue()
        {
            var logger = new Mock<ILogger<CongestionTaxService>>();
            var congestionTaxRepository = new Mock<ICongestionTaxRepository>();
            var congestionTaxService = new CongestionTaxService(logger.Object, congestionTaxRepository.Object);
           
            congestionTaxRepository.Setup(x => x.GetTollFreeDates("gothenburg")).Returns(GetTollFreeDatesList());
            congestionTaxRepository.Setup(x => x.GetTollFreeVehicles("gothenburg")).Returns(GetTollFreeVehiclesList());
            congestionTaxRepository.Setup(x => x.GetSingleChargeRule("gothenburg")).Returns(GetSingleChargeRuleCities());
            congestionTaxRepository.Setup(x => x.GetTollFee(GetTestDates().FirstOrDefault(), "gothenburg")).Returns(8);
            congestionTaxRepository.Setup(x => x.GetTollFee(GetTestDates().FirstOrDefault(), "gothenburg")).Returns(18);
            var taxValue = congestionTaxService.GetTax("car", GetTestDates().ToArray(), "gothenburg");
            Assert.IsType<int>(taxValue);
            Assert.Equal(18, taxValue);
        }

        [Fact]
        public void GetTax_InValidResponse_ReturnsIntegerValue()
        {
            var logger = new Mock<ILogger<CongestionTaxService>>();
            var congestionTaxRepository = new Mock<ICongestionTaxRepository>();
            var congestionTaxService = new CongestionTaxService(logger.Object, congestionTaxRepository.Object);
            
            congestionTaxRepository.Setup(x => x.GetTollFreeDates("gothenburg")).Returns(GetTollFreeDatesList());
            congestionTaxRepository.Setup(x => x.GetTollFreeVehicles("gothenburg")).Returns(GetTollFreeVehiclesList());
            congestionTaxRepository.Setup(x => x.GetSingleChargeRule("gothenburg")).Returns(GetSingleChargeRuleCities());
            congestionTaxRepository.Setup(x => x.GetTollFee(GetTestDates().First(), "gothenburg")).Returns(13);
            congestionTaxRepository.Setup(x => x.GetTollFee(GetTestDates().Last(), "gothenburg")).Returns(13);
            var taxValue = congestionTaxService.GetTax("military", GetTestDates().ToArray(), "gothenburg");
            Assert.IsType<int>(taxValue);
            Assert.NotEqual(0, taxValue);


        }

    
       

        private List<string> GetTollFreeVehiclesList()
        {
            return new List<string>() { "Military", "Bus", "Foriegn" };
        }

        private List<string> GetSingleChargeRuleCities()
        {
            return new List<string>() { "Gothenburg" };
        }

        private List<DateTime> GetTestDates()

        {
            return new List<DateTime>
            {
        new DateTime(2013, 2, 8, 6, 20, 0), 
        new DateTime(2013,2,8,7,30,0)
        
            };
        }

        private List<DateTime> GetTollFreeDatesList()

        {
            return new List<DateTime>
            {
        new DateTime(2013, 12, 24, 15, 29, 0),
        new DateTime(2013, 12, 25, 15, 47, 0),
        new DateTime(2013, 12, 26, 16, 1, 0),
        new DateTime(2013, 12, 31, 16, 48, 0),
        new DateTime(2013, 11, 1, 17, 49, 0)
            };
        }
    }
}
