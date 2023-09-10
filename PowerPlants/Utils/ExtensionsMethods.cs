
namespace PowerPlants.Utils
{
    internal static class ExtensionsMethods
    {
        internal static decimal ConvertDecimalToDecimalPowerPlantFormat(this decimal powerPlantCost)
        {
            return decimal.Parse(powerPlantCost.ToString("0.0"));
        }


        internal static decimal ConvertIntToDecimalPowerPlantFormat(this int powerPlantCost)
        {
            return decimal.Parse(powerPlantCost.ToString("0.0"));
        }
    }
}
