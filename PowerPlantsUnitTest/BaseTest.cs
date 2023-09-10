using PowerPlants.Contracts.Objs;

namespace PowerPlantsUnitTest
{
    public class BaseTest
    {
        internal Fuels BuildFuelsRequest(int cO2EuroPerTon, decimal gasEuroCostByMWh, decimal kerosineEuroPerMWh, int windPercentage)
        {
            return new Fuels
            {
                CO2EuroPerTon = cO2EuroPerTon,
                GasEuroCostByMWh = gasEuroCostByMWh,
                KerosineEuroPerMWh = kerosineEuroPerMWh,
                WindPercentage = windPercentage
            };
        }
    }
}
