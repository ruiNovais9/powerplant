using PowerPlants.Contracts.Objs;

namespace PowerPlants.Contracts.Request
{
    public class PowerPlantsDataRequest
    {
        public PowerPlantsDataRequest()
        {
            Fuels = new Fuels();
            PowerPlants = new List<Plants>();
        }
        public int Load { get; set; }
        public Fuels Fuels { get; set; }
        public List<Plants> PowerPlants { get; set; }
    }
}
