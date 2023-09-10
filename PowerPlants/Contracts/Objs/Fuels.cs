using System.Text.Json.Serialization;

namespace PowerPlants.Contracts.Objs
{
    public class Fuels
    {
        [JsonPropertyName("gas(euro/MWh)")]
        public decimal GasEuroCostByMWh { get; set; }
        [JsonPropertyName("kerosine(euro/MWh)")]
        public decimal KerosineEuroPerMWh { get; set; }
        [JsonPropertyName("co2(euro/ton)")]
        public int CO2EuroPerTon { get; set; }
        [JsonPropertyName("wind(%)")]
        public int WindPercentage { get; set; }

    }
}
