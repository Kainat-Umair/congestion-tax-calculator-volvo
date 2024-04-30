using CongestionTaxCalculator.Services;
using CongestionTaxCalculator.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;

namespace CongestionTaxCalculator.WebAPI.Controllers
{
    [ApiController]
    [Route("~/api/congestionTaxController")]
    public class CongestionTaxController : ControllerBase
    {

        private readonly ILogger<CongestionTaxController> logger;
        private readonly ICongestionTaxService congestionTaxService;
        public CongestionTaxController(ILogger<CongestionTaxController> logger, ICongestionTaxService congestionTaxService)
        {
            this.logger = logger;
            this.congestionTaxService = congestionTaxService;
        }


        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public ActionResult<CongestionTaxResponse> CalculateCongestionTax(CongestionTaxRequest request)
        {
            try
            {

                if (!IsValidRequest(request))
                {
                    return StatusCode(StatusCodes.Status400BadRequest);

                }


                logger.LogInformation($"Calling service method GetTax with request : {request}");
                int result = congestionTaxService.GetTax(request.VehicleType, request.CheckInDateTime, request.CityName);
                logger.LogInformation($"CongestionTax : {result}");
                var response = new CongestionTaxResponse
                {
                    CongestionTaxAmount = result
                };
                string jsonResponse = JsonConvert.SerializeObject(response);
                return Content(jsonResponse, "application/json");
            }
            catch (BadHttpRequestException ex)
            {

                logger.LogError(ex, $"Bad Request Error occured with request : {request}");
                return StatusCode(StatusCodes.Status400BadRequest);


            }
            catch (Exception ex)
            {

                logger.LogError(ex, $"Internal Server Error Occured with request : {request}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        private bool IsValidRequest(CongestionTaxRequest request)
        {
            if (request == null)
            {
                logger.LogError($"Request is null : {request}");
                return false;
            }
            if (!ModelState.IsValid || request.CheckInDateTime == null || string.IsNullOrWhiteSpace(request.VehicleType) || string.IsNullOrWhiteSpace(request.CityName))
            {
                logger.LogError($"Invalid request : {request}");
                return false;
            }

            return true;
        }

    }
}
