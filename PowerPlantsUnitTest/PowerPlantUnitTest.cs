using PowerPlants.Contracts.Objs;

namespace PowerPlantsUnitTest
{
    [TestClass]
    public class PowerPlantUnitTest : BaseTest
    {
        private PowerPlants.Interfaces.IPowerPlantsService _iPowerPlantService { get; set; }
        public PowerPlantUnitTest()
        {
            _iPowerPlantService = new PowerPlants.PowerPlantsService();
        }

        /// <summary>
        /// Test scenario: Verifies that the method should return two power plants working when the load is 910.
        /// </summary>
        [TestMethod]
        public void ShouldReturnWithFourPowerPlantsWorking_LoadingIs910()
        {
            var request = new PowerPlants.Contracts.Request.PowerPlantsDataRequest
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

            var response = _iPowerPlantService.DealWithPowerPlantsRequest(request);
            Assert.IsTrue(response != null && response.Count > 0);
            Assert.IsTrue(response.Count == 6);
            Assert.AreEqual(response[0].Name, "windpark1");
            Assert.AreEqual(response[0].P, 90.0m);

            Assert.AreEqual(response[1].Name, "windpark2");
            Assert.AreEqual(response[1].P, 21.6m);

            Assert.AreEqual(response[2].Name, "gasfiredbig1");
            Assert.AreEqual(response[2].P, 460.0m);

            Assert.AreEqual(response[3].Name, "gasfiredbig2");
            Assert.AreEqual(response[3].P, 338.4m);

            Assert.AreEqual(response[4].Name, "gasfiredsomewhatsmaller");
            Assert.AreEqual(response[4].P, 0.0m);

            Assert.AreEqual(response[5].Name, "tj1");
            Assert.AreEqual(response[5].P, 0.0m);
        }

        /// <summary>
        /// Test scenario: Verifies that the method should return two power plants working when the load is 480, 
        /// Remove power from the gasfiredbig1 to put working with MinPower the gasfiredbig2. 
        /// </summary>
        [TestMethod]
        public void ShouldReturnWithTwoPowerPlantsWorking_LoadingIs480()
        {
            var request = new PowerPlants.Contracts.Request.PowerPlantsDataRequest
            {
                Load = 480,
                Fuels = BuildFuelsRequest(20, 13.4m, 50.8m, 0),
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

            var response = _iPowerPlantService.DealWithPowerPlantsRequest(request);
            Assert.IsTrue(response != null && response.Count > 0);
            Assert.IsTrue(response.Count == 6);
            Assert.AreEqual(response[0].Name, "windpark1");
            Assert.AreEqual(response[0].P, 0.0m);

            Assert.AreEqual(response[1].Name, "windpark2");
            Assert.AreEqual(response[1].P, 0.0m);

            Assert.AreEqual(response[2].Name, "gasfiredbig1");
            Assert.AreEqual(response[2].P, 380.0m);

            Assert.AreEqual(response[3].Name, "gasfiredbig2");
            Assert.AreEqual(response[3].P, 100.0m);

            Assert.AreEqual(response[4].Name, "gasfiredsomewhatsmaller");
            Assert.AreEqual(response[4].P, 0.0m);

            Assert.AreEqual(response[5].Name, "tj1");
            Assert.AreEqual(response[5].P, 0.0m);
        }

        /// <summary>
        /// Test scenario: No loading power to put working the power Plants.
        /// </summary>
        [TestMethod]
        public void NoPowerPutPowerPlantsWorking_LoadingIs0()
        {
            var request = new PowerPlants.Contracts.Request.PowerPlantsDataRequest
            {
                Load = 0,
                Fuels = BuildFuelsRequest(20, 13.4m, 50.8m, 0),
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

            var response = _iPowerPlantService.DealWithPowerPlantsRequest(request);
            Assert.IsTrue(response != null && response.Count > 0);
            Assert.IsTrue(response.Count == 6);
            Assert.AreEqual(response[0].Name, "windpark1");
            Assert.AreEqual(response[0].P, 0.0m);

            Assert.AreEqual(response[1].Name, "windpark2");
            Assert.AreEqual(response[1].P, 0.0m);

            Assert.AreEqual(response[2].Name, "gasfiredbig1");
            Assert.AreEqual(response[2].P, 0.0m);

            Assert.AreEqual(response[3].Name, "gasfiredbig2");
            Assert.AreEqual(response[3].P, 0.0m);

            Assert.AreEqual(response[4].Name, "gasfiredsomewhatsmaller");
            Assert.AreEqual(response[4].P, 0.0m);

            Assert.AreEqual(response[5].Name, "tj1");
            Assert.AreEqual(response[5].P, 0.0m);
        }

        /// <summary>
        /// Test scenario: Verifies that the method should return the correct power distribution when the load is 480 and wind is high.
        /// </summary>
        [TestMethod]
        public void ShouldReturnCorrectPowerDistribution_WhenLoadIs480AndWindIsHigh()
        {
            var request = new PowerPlants.Contracts.Request.PowerPlantsDataRequest
            {
                Load = 480,
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

            var response = _iPowerPlantService.DealWithPowerPlantsRequest(request);
            Assert.IsTrue(response != null && response.Count > 0);
            Assert.IsTrue(response.Count == 6);
            Assert.AreEqual(response[0].Name, "windpark1");
            Assert.AreEqual(response[0].P, 90.0m);

            Assert.AreEqual(response[1].Name, "windpark2");
            Assert.AreEqual(response[1].P, 21.6m);

            Assert.AreEqual(response[2].Name, "gasfiredbig1");
            Assert.AreEqual(response[2].P, 368.4m);

            Assert.AreEqual(response[3].Name, "gasfiredbig2");
            Assert.AreEqual(response[3].P, 0.0m);

            Assert.AreEqual(response[4].Name, "gasfiredsomewhatsmaller");
            Assert.AreEqual(response[4].P, 0.0m);

            Assert.AreEqual(response[5].Name, "tj1");
            Assert.AreEqual(response[5].P, 0.0m);
        }

        /// <summary>
        /// Test scenario: No PowerPlants on request, returning empty list.
        /// </summary>
        [TestMethod]
        public void NoPowerPlants_LoadingIs480ReturningEmptyList()
        {
            var request = new PowerPlants.Contracts.Request.PowerPlantsDataRequest
            {
                Load = 480,
                Fuels = BuildFuelsRequest(20, 13.4m, 50.8m, 60),
                PowerPlants = new List<Plants>
                {
                }
            };

            var response = _iPowerPlantService.DealWithPowerPlantsRequest(request);
            Assert.IsTrue(response != null && response.Count == 0);
        }

        /// <summary>
        /// Test scenario: Put all power plants working full power, left 0.4 power to use.
        /// </summary>
        [TestMethod]
        public void ShouldReturnFullForAllPowerPlants_WhenLoadIs1340()
        {
            var request = new PowerPlants.Contracts.Request.PowerPlantsDataRequest
            {
                Load = 1258,
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

            var response = _iPowerPlantService.DealWithPowerPlantsRequest(request);
            Assert.IsTrue(response != null && response.Count > 0);
            Assert.IsTrue(response.Count == 6);
            Assert.AreEqual(response[0].Name, "windpark1");
            Assert.AreEqual(response[0].P, 90.0m);

            Assert.AreEqual(response[1].Name, "windpark2");
            Assert.AreEqual(response[1].P, 21.6m);

            Assert.AreEqual(response[2].Name, "gasfiredbig1");
            Assert.AreEqual(response[2].P, 460.0m);

            Assert.AreEqual(response[3].Name, "gasfiredbig2");
            Assert.AreEqual(response[3].P, 460.0m);

            Assert.AreEqual(response[4].Name, "gasfiredsomewhatsmaller");
            Assert.AreEqual(response[4].P, 210.0m);

            Assert.AreEqual(response[5].Name, "tj1");
            Assert.AreEqual(response[5].P, 16.0m);

            decimal leftEnergy = request.Load - (response[0].P + response[1].P + response[2].P + response[3].P + response[4].P + response[5].P);

            Assert.IsTrue(leftEnergy == 0.4m);
        }

        /// <summary>
        /// Test scenario: Verifies that the method should return the correct power distribution when the load is 480 and wind is 100%.
        /// </summary>
        [TestMethod]
        public void ShouldReturnCorrectPowerDistribution_WhenLoadIs480AndWindIs100()
        {
            var request = new PowerPlants.Contracts.Request.PowerPlantsDataRequest
            {
                Load = 480,
                Fuels = BuildFuelsRequest(20, 13.4m, 50.8m, 100),
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

            var response = _iPowerPlantService.DealWithPowerPlantsRequest(request);
            Assert.IsTrue(response != null && response.Count > 0);
            Assert.IsTrue(response.Count == 6);
            Assert.AreEqual(response[0].Name, "windpark1");
            Assert.AreEqual(response[0].P, 150.0m);

            Assert.AreEqual(response[1].Name, "windpark2");
            Assert.AreEqual(response[1].P, 36.0m);

            Assert.AreEqual(response[2].Name, "gasfiredbig1");
            Assert.AreEqual(response[2].P, 294.0m);

            Assert.AreEqual(response[3].Name, "gasfiredbig2");
            Assert.AreEqual(response[3].P, 0.0m);

            Assert.AreEqual(response[4].Name, "gasfiredsomewhatsmaller");
            Assert.AreEqual(response[4].P, 0.0m);

            Assert.AreEqual(response[5].Name, "tj1");
            Assert.AreEqual(response[5].P, 0.0m);
        }

        /// <summary>
        /// Test scenario: Verifies that the method should return the correct power distribution when the load is 480 and no efficiency equal on gasfired.
        /// </summary>
        [TestMethod]
        public void ShouldReturnCorrectPowerDistribution_WhenLoadIs480AndNoEfficiencyEqual()
        {
            var request = new PowerPlants.Contracts.Request.PowerPlantsDataRequest
            {
                Load = 480,
                Fuels = BuildFuelsRequest(20, 13.4m, 50.8m, 0),
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
                        Efficiency = 0.51m,
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

            var response = _iPowerPlantService.DealWithPowerPlantsRequest(request);
            Assert.IsTrue(response != null && response.Count > 0);
            Assert.IsTrue(response.Count == 6);
            Assert.AreEqual(response[0].Name, "windpark1");
            Assert.AreEqual(response[0].P, 0.0m);

            Assert.AreEqual(response[1].Name, "windpark2");
            Assert.AreEqual(response[1].P, 0.0m);

            Assert.AreEqual(response[2].Name, "gasfiredbig1");
            Assert.AreEqual(response[2].P, 460.0m);

            Assert.AreEqual(response[3].Name, "gasfiredbig2");
            Assert.AreEqual(response[3].P, 0.0m);

            Assert.AreEqual(response[4].Name, "gasfiredsomewhatsmaller");
            Assert.AreEqual(response[4].P, 0.0m);

            Assert.AreEqual(response[5].Name, "tj1");
            Assert.AreEqual(response[5].P, 16.0m);
        }
    }
}