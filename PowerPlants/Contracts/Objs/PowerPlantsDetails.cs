
namespace PowerPlants.Contracts.Objs
{
    public class PowerPlantsDetails
    {
        public string Name { get; set; } = string.Empty;
        public decimal Cost { get; set; } 
        public int Pmin { get; set; }
        public int Pmax { get; set; }
        public decimal Efficiency { get; set; }
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Get the powerPlants details and order the power plants by Cost and by Efficiency.
        /// If don't find any power plant is returned a empty list.
        /// </summary>
        /// <param name="powerPlants"></param>
        /// <returns>
        /// A list order by Cost and Efficiency.
        /// </returns>
        public static List<PowerPlantsDetails> ListOrderByCostThenByEfficiency(List<PowerPlantsDetails> powerPlants)
        {
            if (powerPlants == null)
            {
                return new List<PowerPlantsDetails>();
            }

            List<PowerPlantsDetails> listOfPlants = powerPlants;

            int totalPowerPlants = listOfPlants.Count;

            for (int i = 0; i < totalPowerPlants; i++)
            {
                PowerPlantsDetails firstDetailPowerPlant = listOfPlants[i];
                for (int y = i + 1; y < totalPowerPlants; y++)
                {
                    PowerPlantsDetails detailPowertPlantToBeCompared = listOfPlants[y];

                    if (firstDetailPowerPlant.Cost > detailPowertPlantToBeCompared.Cost ||
                        (firstDetailPowerPlant.Cost == detailPowertPlantToBeCompared.Cost &&
                         firstDetailPowerPlant.Efficiency < detailPowertPlantToBeCompared.Efficiency))
                    {
                        PowerPlantsDetails tempAtualDetailPlant = firstDetailPowerPlant;
                        listOfPlants[i] = detailPowertPlantToBeCompared;
                        listOfPlants[y] = tempAtualDetailPlant;
                        firstDetailPowerPlant = detailPowertPlantToBeCompared;
                    }
                }
            }
            return listOfPlants;
        }
    }
}
