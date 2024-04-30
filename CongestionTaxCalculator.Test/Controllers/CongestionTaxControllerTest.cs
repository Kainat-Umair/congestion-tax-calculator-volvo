using CongestionTaxCalculator.Services;
using CongestionTaxCalculator.WebAPI.Controllers;
using CongestionTaxCalculator.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using System;
using Xunit;

namespace CongestionTaxCalculator.Test.Controllers
{
    public class CongestionTaxControllerTest
    {
        [Fact]
        public void CalculateCongestionTax_ValidRequest_ReturnsOkResult()
        {

            var logger = new Mock<ILogger<CongestionTaxController>>();
            var congestionTaxService = new Mock<ICongestionTaxService>();
            var controller = new CongestionTaxController(logger.Object, congestionTaxService.Object);
            var request = new CongestionTaxRequest
            {
                VehicleType = "Car",
                CheckInDateTime = new[] {new DateTime(2013, 2, 8, 15, 29, 0) },
                CityName = "Gothenburg"
            };

            congestionTaxService.Setup(x => x.GetTax(request.VehicleType, request.CheckInDateTime, request.CityName))
                .Returns(18);


            var result = controller.CalculateCongestionTax(request);
            Assert.NotNull(result);
            var okResult = Assert.IsType<ContentResult>(result.Result);
            Assert.Equal("application/json", okResult.ContentType);
            var jsonResponse = okResult.Content;
            var response = JsonConvert.DeserializeObject<CongestionTaxResponse>(jsonResponse);
            Assert.NotNull(response);
            Assert.Equal(18, response.CongestionTaxAmount);



        }
        [Fact]
        public void CalculateCongestionTax_InValidRequest_ReturnsBadRequest()
        {

            var logger = new Mock<ILogger<CongestionTaxController>>();
            var congestionTaxService = new Mock<ICongestionTaxService>();
            var controller = new CongestionTaxController(logger.Object, congestionTaxService.Object);
            var request = new CongestionTaxRequest
            {
                VehicleType = " ",
                CheckInDateTime = new[] { new DateTime(2013, 2, 8, 15, 29, 0) },
                CityName = "Gothenburg"
            };

            congestionTaxService.Setup(x => x.GetTax(request.VehicleType, request.CheckInDateTime, request.CityName))
                .Returns(18);


            var result = controller.CalculateCongestionTax(request);

            Assert.IsType<StatusCodeResult>(result.Result);          
            Assert.NotNull(result.Result);
            Assert.Equal(400, (result.Result as StatusCodeResult).StatusCode);
        }
        [Fact]
        public void CalculateCongestionTax_NullRequest_ReturnsBadRequest()
        {

            var logger = new Mock<ILogger<CongestionTaxController>>();
            var congestionTaxService = new Mock<ICongestionTaxService>();
            var controller = new CongestionTaxController(logger.Object, congestionTaxService.Object);
            var request = new CongestionTaxRequest
            {
                VehicleType = null,
                CheckInDateTime = null,
                CityName = null
            };

            congestionTaxService.Setup(x => x.GetTax(request.VehicleType, request.CheckInDateTime, request.CityName))
                .Returns(18);


            var result = controller.CalculateCongestionTax(request);
            Assert.IsType<StatusCodeResult>(result.Result);
            Assert.NotNull(result.Result);
            Assert.Equal(400, (result.Result as StatusCodeResult).StatusCode);
        }

    }
}
