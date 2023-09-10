using PowerPlants.Contracts.Objs;

namespace PowerPlants.Utils
{
    internal static class UtilsPowerPlantsHelper
    {
        private const string GasFired = "gasfired";
        private const string TurboJet = "turbojet";
        /// <summary>
        /// Get the min value between the powerMax from power plant detail and total energy (Power) available.
        /// </summary>
        /// <param name="totalEnergyAvailable"></param>
        /// <param name="powerMax"></param>
        /// <returns>
        /// If the powerMax is lower or equal than energy available use the max of energy,
        /// otherwise is used the total energy left.
        /// </returns>
        internal static decimal GetMinValueBetweenTotalEnergy_PowerMax(decimal totalEnergyAvailable, int powerMax)
        {
            decimal powerPowerPlant;
            if (powerMax <= totalEnergyAvailable)
            {
                powerPowerPlant = powerMax;
            }
            else
            {
                powerPowerPlant = totalEnergyAvailable;
            }

            return powerPowerPlant;
        }

        /// <summary>
        /// Get the Cost by each power plant depending of the type.
        /// </summary>
        /// <param name="fuels"></param>
        /// <param name="powerPlantEfficiency"></param>
        /// <param name="powerPlantType"></param>
        /// <returns>
        /// The cost of usage by power plant.
        /// </returns>
        internal static decimal GetTotalCostByType(Fuels fuels, decimal powerPlantEfficiency, string powerPlantType)
        {
            switch (powerPlantType)
            {
                case GasFired:
                    {
                        return (fuels.GasEuroCostByMWh / powerPlantEfficiency) + fuels.CO2EuroPerTon * 0.3m;
                    }
                case TurboJet:
                    {
                        return fuels.KerosineEuroPerMWh / powerPlantEfficiency;
                    }
                default:
                    {
                        return 0;
                    }
            }
        }

        /// <summary>
        /// Get the power available to start the power plant.
        /// </summary>
        /// <param name="windPercentage"></param>
        /// <param name="totalEnergyAvailable"></param>
        /// <param name="powerPlantType"></param>
        /// <param name="pMax"></param>
        /// <returns>
        /// The power available to start the power plant.
        /// </returns>
        internal static decimal GetPowerAvailableByType(int windPercentage, decimal totalEnergyAvailable, string powerPlantType, int pMax)
        {
            decimal powerAvailable;

            switch (powerPlantType)
            {
                case GasFired:
                    {
                        decimal minPowerAvailable = GetMinValueBetweenTotalEnergy_PowerMax(totalEnergyAvailable, pMax);
                        powerAvailable = (minPowerAvailable / 0.1m) * 0.1m;
                    }
                    break;
                case TurboJet:
                    {
                        decimal minPowerAvailable = GetMinValueBetweenTotalEnergy_PowerMax(totalEnergyAvailable, pMax);
                        powerAvailable = (minPowerAvailable / 0.1m) * 0.1m;
                    }
                    break;
                default:
                    {
                        powerAvailable = GetMaxPowerFromWind(windPercentage, pMax);
                    }
                    break;
            }

            return powerAvailable.ConvertDecimalToDecimalPowerPlantFormat();
        }

        /// <summary>
        /// Get the power of power plant of the type windturbine.
        /// </summary>
        /// <param name="windPercentage"></param>
        /// <param name="maxPower"></param>
        /// <returns>
        /// The max power depending of the wind.
        /// </returns>
        private static decimal GetMaxPowerFromWind(int windPercentage, int maxPower)
        {
            return (windPercentage / 100m) * maxPower;
        }
    }
}
