using PowerPlants.Contracts.Objs;
using PowerPlants.Utils;
using PowerPlants.Contracts.Request;

namespace PowerPlantsUnitTest
{
    [TestClass]
    public class OrderPowerPlantsDetailsTest : BaseTest
    {
        /// <summary>
        /// Test scenario: Order a list from request by Cost and efficiency.
        /// </summary>
        [TestMethod]
        public void ShouldReturnListOrderByCostThenEfficiency_GetCostByPowerPlantsRequest()
        {
            var request = new PowerPlantsDataRequest
            {
                Load = 910,
                Fuels = BuildFuelsRequest(20, 13.4m, 50.8m, 60),
                PowerPlants = new List<Plants>
                {
                    new Plants
                    {
                        Name = "gasfiredbig1",
                        Type = "gasfired",
                        Efficiency = 0.53m,
                        Pmin = 100,
                        Pmax = 460
                    },
                    new Plants
                    {
                        Name = "gasfiredbig2",
                        Type = "gasfired",
                        Efficiency = 0.53m,
                        Pmin = 100,
                        Pmax = 460
                    },
                    new Plants
                    {
                        Name = "gasfiredsomewhatsmaller",
                        Type = "gasfired",
                        Efficiency = 0.37m,
                        Pmin = 40,
                        Pmax = 210
                    },
                    new Plants
                    {
                        Name = "tj1",
                        Type = "turbojet",
                        Efficiency = 0.3m,
                        Pmin = 0,
                        Pmax = 16
                    },
                    new Plants
                    {
                        Name = "windpark1",
                        Type = "windturbine",
                        Efficiency = 1,
                        Pmin = 0,
                        Pmax = 150
                    },
                    new Plants
                    {
                        Name = "windpark2",
                        Type = "windturbine",
                        Efficiency = 1,
                        Pmin = 0,
                        Pmax = 36
                    },
                }
            };

            List<PowerPlantsDetails> powerDetailsList = ConverterToPowerPlantsDetails.ConvertRequestToPowerPlantDetails(request);
            Assert.IsTrue(powerDetailsList != null && powerDetailsList.Count > 0);
            Assert.IsTrue(powerDetailsList.Count == 6);

            ComparePowerDetailWithRequest(powerDetailsList[0], request.PowerPlants[0], 31.283018867924528301886792453M);

            ComparePowerDetailWithRequest(powerDetailsList[1], request.PowerPlants[1], 31.283018867924528301886792453M);

            ComparePowerDetailWithRequest(powerDetailsList[2], request.PowerPlants[2], 42.216216216216216216216216216M);

            ComparePowerDetailWithRequest(powerDetailsList[3], request.PowerPlants[3], 169.33333333333333333333333333M);

            ComparePowerDetailWithRequest(powerDetailsList[4], request.PowerPlants[4], 0);

            ComparePowerDetailWithRequest(powerDetailsList[5], request.PowerPlants[5], 0);

            List<PowerPlantsDetails> powerDetailsListOrdered = PowerPlantsDetails.ListOrderByCostThenByEfficiency(powerDetailsList);

            ComparePowerDetailWithRequest("windturbine", powerDetailsListOrdered[0], request.PowerPlants[4], 0);

            ComparePowerDetailWithRequest("windturbine", powerDetailsListOrdered[1], request.PowerPlants[5], 0);

            ComparePowerDetailWithRequest("gasfired", powerDetailsListOrdered[2], request.PowerPlants[0], 31.283018867924528301886792453M);

            ComparePowerDetailWithRequest("gasfired", powerDetailsListOrdered[3], request.PowerPlants[1], 31.283018867924528301886792453M);

            ComparePowerDetailWithRequest("gasfired", powerDetailsListOrdered[4], request.PowerPlants[2], 42.216216216216216216216216216M);

            ComparePowerDetailWithRequest("turbojet", powerDetailsListOrdered[5], request.PowerPlants[3], 169.33333333333333333333333333M);
        }

        /// <summary>
        /// Test scenario: Order a list from request by Cost and efficiency.
        /// </summary>
        [TestMethod]
        public void ShouldReturnListOrderByCostThenEfficiency_GetCostByPowerPlantsRequestDifferentsEfficiency()
        {
            var request = new PowerPlantsDataRequest
            {
                Load = 910,
                Fuels = BuildFuelsRequest(20, 13.4m, 50.8m, 60),
                PowerPlants = new List<Plants>
                {
                    new Plants
                    {
                        Name = "gasfiredbig1",
                        Type = "gasfired",
                        Efficiency = 0.53m,
                        Pmin = 100,
                        Pmax = 460
                    },
                    new Plants
                    {
                        Name = "gasfiredbig2",
                        Type = "gasfired",
                        Efficiency = 0.20m,
                        Pmin = 100,
                        Pmax = 460
                    },
                    new Plants
                    {
                        Name = "gasfiredsomewhatsmaller",
                        Type = "gasfired",
                        Efficiency = 0.37m,
                        Pmin = 40,
                        Pmax = 210
                    },
                    new Plants
                    {
                        Name = "tj1",
                        Type = "turbojet",
                        Efficiency = 0.3m,
                        Pmin = 0,
                        Pmax = 16
                    },
                    new Plants
                    {
                        Name = "windpark1",
                        Type = "windturbine",
                        Efficiency = 1,
                        Pmin = 0,
                        Pmax = 150
                    },
                    new Plants
                    {
                        Name = "windpark2",
                        Type = "windturbine",
                        Efficiency = 1,
                        Pmin = 0,
                        Pmax = 36
                    },
                }
            };

            List<PowerPlantsDetails> powerDetailsList = ConverterToPowerPlantsDetails.ConvertRequestToPowerPlantDetails(request);
            Assert.IsTrue(powerDetailsList != null && powerDetailsList.Count > 0);
            Assert.IsTrue(powerDetailsList.Count == 6);

            ComparePowerDetailWithRequest(powerDetailsList[0], request.PowerPlants[0], 31.283018867924528301886792453M);

            ComparePowerDetailWithRequest(powerDetailsList[1], request.PowerPlants[1], 73.0m);

            ComparePowerDetailWithRequest(powerDetailsList[2], request.PowerPlants[2], 42.216216216216216216216216216M);

            ComparePowerDetailWithRequest(powerDetailsList[3], request.PowerPlants[3], 169.33333333333333333333333333M);

            ComparePowerDetailWithRequest(powerDetailsList[4], request.PowerPlants[4], 0);

            ComparePowerDetailWithRequest(powerDetailsList[5], request.PowerPlants[5], 0);

            List<PowerPlantsDetails> powerDetailsListOrdered = PowerPlantsDetails.ListOrderByCostThenByEfficiency(powerDetailsList);

            ComparePowerDetailWithRequest("windturbine", powerDetailsListOrdered[0], request.PowerPlants[4], 0);

            ComparePowerDetailWithRequest("windturbine", powerDetailsListOrdered[1], request.PowerPlants[5], 0);

            ComparePowerDetailWithRequest("gasfired", powerDetailsListOrdered[2], request.PowerPlants[0], 31.283018867924528301886792453M);

            ComparePowerDetailWithRequest("gasfired", powerDetailsListOrdered[3], request.PowerPlants[2], 42.216216216216216216216216216M);

            ComparePowerDetailWithRequest("gasfired", powerDetailsListOrdered[4], request.PowerPlants[1], 73.0m);

            ComparePowerDetailWithRequest("turbojet", powerDetailsListOrdered[5], request.PowerPlants[3], 169.33333333333333333333333333M);
        }

        private void ComparePowerDetailWithRequest(PowerPlantsDetails powerDetails, Plants plants, decimal cost)
        {
            Assert.AreEqual(powerDetails.Name, plants.Name);
            Assert.AreEqual(powerDetails.Pmin, plants.Pmin);
            Assert.AreEqual(powerDetails.Pmax, plants.Pmax);
            Assert.AreEqual(powerDetails.Efficiency, plants.Efficiency);
            Assert.AreEqual(powerDetails.Cost, cost);
            Assert.AreEqual(powerDetails.Type, plants.Type);
        }

        private void ComparePowerDetailWithRequest(string typePowerPlants, PowerPlantsDetails powerDetails, Plants plants, decimal cost)
        {
            Assert.AreEqual(powerDetails.Name, plants.Name);
            Assert.AreEqual(powerDetails.Pmin, plants.Pmin);
            Assert.AreEqual(powerDetails.Pmax, plants.Pmax);
            Assert.AreEqual(powerDetails.Efficiency, plants.Efficiency);
            Assert.AreEqual(powerDetails.Cost, cost);
            Assert.AreEqual(powerDetails.Type, typePowerPlants);
        }

    }
}
