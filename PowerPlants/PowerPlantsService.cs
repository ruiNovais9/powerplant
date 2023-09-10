using PowerPlants.Contracts.Objs;
using PowerPlants.Contracts.Request;
using PowerPlants.Contracts.Response;
using PowerPlants.Interfaces;
using PowerPlants.Utils;

namespace PowerPlants
{
    public class PowerPlantsService : IPowerPlantsService
    {
        /// <summary>
        /// This method is to process with the request received.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>
        /// The power used in each power plant.
        /// </returns>
        public List<PowerPlantsDataResponse> DealWithPowerPlantsRequest(PowerPlantsDataRequest request)
        {
            List<PowerPlantsDetails> detailPowerPlants = ConverterToPowerPlantsDetails.ConvertRequestToPowerPlantDetails(request);

            List<PowerPlantsDetails> orderPowerPlantsByCost_Efficiency = PowerPlantsDetails.ListOrderByCostThenByEfficiency(detailPowerPlants);
            
            List<PowerPlantsDataResponse> powerPlantsFinalResults = ProcessPowerPlantsBuildResponse(request.Fuels.WindPercentage,
                                                                                                    request.Load, detailPowerPlants,
                                                                                                    orderPowerPlantsByCost_Efficiency);

            return powerPlantsFinalResults;
        }

        /// <summary>
        /// calculate the Energy necessary by each Power Plant.
        /// If don't have enough energy so start because of the PMin,
        /// Is get the last power plant energy and try remove some energy from that last one and add some energy to this power plant to start,
        /// If the efficiency is not the same or lower is not removed energy from the last power plant.
        /// </summary>
        /// <param name="windPercentage"></param>
        /// <param name="requestLoad"></param>
        /// <param name="detailPowerPlants"></param>
        /// <param name="orderPowerPlantsByCost_Efficiency"></param>
        /// <returns>
        /// The power used in each power plant.
        /// </returns>
        private List<PowerPlantsDataResponse> ProcessPowerPlantsBuildResponse(int windPercentage, decimal requestLoad,
                                                                              List<PowerPlantsDetails> detailPowerPlants,
                                                                              List<PowerPlantsDetails> orderPowerPlantsByCost_Efficiency)
        {
            decimal totalEnergyAvailable = requestLoad;
            int powerPlantListsIndex = 0;

            var powerPlantsFinalResults = new List<PowerPlantsDataResponse>();

            foreach (PowerPlantsDetails powerPlantDetail in orderPowerPlantsByCost_Efficiency)
            {
                if (totalEnergyAvailable <= 0)
                {
                    powerPlantsFinalResults.Add(new PowerPlantsDataResponse(powerPlantDetail.Name, 0.0m));
                    powerPlantListsIndex++;
                    continue;
                }

                decimal powerPlantEnergyNecessary = UtilsPowerPlantsHelper.GetPowerAvailableByType(windPercentage, totalEnergyAvailable,
                                                                            powerPlantDetail.Type, powerPlantDetail.Pmax);

                if (powerPlantDetail.Pmin > powerPlantEnergyNecessary)
                {
                    if (CheckIfCanRemovePowerFromLastPowerPlant(powerPlantsFinalResults, detailPowerPlants,
                                                              powerPlantDetail, powerPlantEnergyNecessary,
                                                              ref totalEnergyAvailable, ref powerPlantListsIndex))
                    {
                        continue;
                    }

                    powerPlantsFinalResults.Add(new PowerPlantsDataResponse(powerPlantDetail.Name, 0.0m));
                    powerPlantListsIndex++;
                    continue;
                }

                totalEnergyAvailable -= powerPlantEnergyNecessary;

                powerPlantsFinalResults.Add(new PowerPlantsDataResponse(powerPlantDetail.Name, powerPlantEnergyNecessary));
                powerPlantListsIndex++;
            }

            return powerPlantsFinalResults;
        }

        /// <summary>
        /// Only usage this method when the PMin is bigger than the power available
        /// Check if can remove the power from last calculate Power plant,
        /// get the total power usage and get how much left to start the new power plant,
        /// to remove the power from the last one, need to the same efficiency and the new calculated power need to be bigger than PMin.
        /// If no power plant is calculated yet the return is false.
        /// </summary>
        /// <param name="powerPlantsDataResponse"></param>
        /// <param name="powerPlantDetails"></param>
        /// <param name="atualPowerPlantDetail"></param>
        /// <param name="powerPlantEnergyNecessary"></param>
        /// <param name="totalEnergyAvailable"></param>
        /// <param name="powerPlantListsIndex"></param>
        /// <returns>
        /// If false it means is not possible to start that power plant because don't have enough power,
        /// if true the power plant is started and the last power plant power is updated.
        /// </returns>
        private bool CheckIfCanRemovePowerFromLastPowerPlant(List<PowerPlantsDataResponse> powerPlantsDataResponse,
                                                  List<PowerPlantsDetails> powerPlantDetails,
                                                  PowerPlantsDetails atualPowerPlantDetail, decimal powerPlantEnergyNecessary,
                                                  ref decimal totalEnergyAvailable, ref int powerPlantListsIndex)
        {
            if (powerPlantListsIndex == 0 && powerPlantListsIndex > powerPlantsDataResponse.Count)
            {
                return false;
            }

            PowerPlantsDataResponse getLastPowerPlantProcessed = powerPlantsDataResponse[powerPlantListsIndex - 1];

            PowerPlantsDetails lastPowerPlantDetail = powerPlantDetails[powerPlantListsIndex - 1];

            decimal getPowerLeftStart = atualPowerPlantDetail.Pmin - powerPlantEnergyNecessary;

            decimal newPowerForLastPowerPlant = getLastPowerPlantProcessed.P - getPowerLeftStart;

            if (newPowerForLastPowerPlant > lastPowerPlantDetail.Pmin &&
                lastPowerPlantDetail.Efficiency == atualPowerPlantDetail.Efficiency)
            {
                UpdateAtualPowerPlantAndLastPowerPlant(powerPlantsDataResponse, atualPowerPlantDetail,
                                                       powerPlantEnergyNecessary, ref totalEnergyAvailable, ref powerPlantListsIndex,
                                                       getLastPowerPlantProcessed, newPowerForLastPowerPlant);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Update the last Power Plant power with the new used power.
        /// Add new power plant with the power used to the response list.
        /// Subtract the used power in power plant in total Energy Available.
        /// Add one more index.
        /// </summary>
        /// <param name="powerPlantsDataResponse"></param>
        /// <param name="atualPowerPlantDetail"></param>
        /// <param name="powerPlantCost"></param>
        /// <param name="totalEnergyAvailable"></param>
        /// <param name="powerPlantListsIndex"></param>
        /// <param name="getLastPowerPlantProcessed"></param>
        /// <param name="newValueOfPower"></param>
        private static void UpdateAtualPowerPlantAndLastPowerPlant(List<PowerPlantsDataResponse> powerPlantsDataResponse,
                                                                   PowerPlantsDetails atualPowerPlantDetail, decimal powerPlantCost,
                                                                   ref decimal totalEnergyAvailable, ref int powerPlantListsIndex,
                                                                   PowerPlantsDataResponse getLastPowerPlantProcessed, decimal newValueOfPower)
        {
            getLastPowerPlantProcessed.P = newValueOfPower;
            powerPlantsDataResponse.Add(new PowerPlantsDataResponse(atualPowerPlantDetail.Name,
                                                                    atualPowerPlantDetail.Pmin.ConvertIntToDecimalPowerPlantFormat()));
            totalEnergyAvailable -= powerPlantCost;
            powerPlantListsIndex++;
        }
    }
}