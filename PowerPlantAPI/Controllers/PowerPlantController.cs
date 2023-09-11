using Microsoft.AspNetCore.Mvc;
using PowerPlants.Contracts.Request;
using PowerPlants.Contracts.Response;
using PowerPlants.Interfaces;
using System.Text.Json;

namespace Powerplant.Controllers
{
    public class PowerPlantController : Controller
    {
        private IPowerPlantsService _powerPlantsService { get; set; }
        private ILogger<PowerPlantController> _logger { get; set; }
        public PowerPlantController(IPowerPlantsService powerPlantsService, ILogger<PowerPlantController> logger)
        {
            _powerPlantsService = powerPlantsService;
            _logger = logger;
        }

        
        /// <summary>
        /// Receive the total of available power, the description of fuels and the information of a list of powerplants.
        /// Process this data and get the power of each powerplant.
        /// </summary>
        /// <param name="powerPlantsRequest"></param>
        /// <returns>The list of powerPlant with the power should deliver.</returns>
        [HttpPost("productionplan")]
        public ActionResult<List<PowerPlantsDataResponse>> ProductionPlan([FromBody] PowerPlantsDataRequest powerPlantsRequest)
        {
            try
            {
                List<PowerPlantsDataResponse> response = _powerPlantsService.DealWithPowerPlantsRequest(powerPlantsRequest);
                _logger.LogInformation("Success - Request:" + JsonSerializer.Serialize(powerPlantsRequest) + " \n Response: " + JsonSerializer.Serialize(response));
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message + " Request: " + JsonSerializer.Serialize(powerPlantsRequest));
                return BadRequest(ex);
            }
        }
    }
}
