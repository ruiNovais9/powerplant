using PowerPlants.Contracts.Objs;
using PowerPlants.Contracts.Request;

namespace PowerPlants.Utils
{
    public static class ConverterToPowerPlantsDetails
    {
        /// <summary>
        /// Convert the request received to Power Plants Details, here it's get the Cost by each PowerPlant.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>
        /// The details by each Power Plant.
        /// </returns>
        public static List<PowerPlantsDetails> ConvertRequestToPowerPlantDetails(PowerPlantsDataRequest request)
        {
            var detailPowerPlants = new List<PowerPlantsDetails>();

            if (request == null)
            {
                return detailPowerPlants;
            }

            foreach (Plants details in request.PowerPlants)
            {
                detailPowerPlants.Add(new PowerPlantsDetails
                {
                    Name = details.Name,
                    Pmin = details.Pmin,
                    Pmax = details.Pmax,
                    Efficiency = details.Efficiency,
                    Cost = UtilsPowerPlantsHelper.GetTotalCostByType(request.Fuels, details.Efficiency, details.Type),
                    Type = details.Type
                });
            }

            return detailPowerPlants;
        }
    }
}
