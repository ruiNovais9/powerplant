
namespace PowerPlants.Contracts.Objs
{
    public class Plants
    {
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public decimal Efficiency { get; set; }
        public int Pmin { get; set; }
        public int Pmax { get; set; }
    }
}
