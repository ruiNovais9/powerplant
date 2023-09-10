using System.Text.Json.Serialization;

namespace PowerPlants.Contracts.Response
{
    public class PowerPlantsDataResponse
    {
        public PowerPlantsDataResponse(string name, decimal p) 
        {
            Name = name;
            P = p;
        }

        [JsonPropertyName("name")]
        public string Name { get; set; }
        
        [JsonPropertyName("p")]
        public decimal P { get; set; }
    }
}
