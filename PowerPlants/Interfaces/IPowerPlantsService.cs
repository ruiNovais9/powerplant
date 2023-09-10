
using PowerPlants.Contracts.Response;

namespace PowerPlants.Interfaces
{
    public interface IPowerPlantsService
    {
        List<PowerPlantsDataResponse> DealWithPowerPlantsRequest(Contracts.Request.PowerPlantsDataRequest request);
    }
}
