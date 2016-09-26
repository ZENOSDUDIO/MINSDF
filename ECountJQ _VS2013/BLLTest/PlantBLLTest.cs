using SGM.Ecount.BLL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SGM.ECount.DataModel;
using System.Collections.Generic;

namespace BLLTest
{
    
    
    /// <summary>
    ///This is a test class for PlantBLLTest and is intended
    ///to contain all PlantBLLTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PlantBLLTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for UpdatePlant
        ///</summary>
        [TestMethod()]
        public void UpdatePlantTest()
        {
            PlantBLL target = new PlantBLL(); // TODO: Initialize to an appropriate value
            Plant plant = null; // TODO: Initialize to an appropriate value
            target.UpdatePlant(plant);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for GetPlants
        ///</summary>
        [TestMethod()]
        public void GetPlantsTest()
        {
            PlantBLL target = new PlantBLL(); // TODO: Initialize to an appropriate value
            List<Plant> expected = null; // TODO: Initialize to an appropriate value
            List<Plant> actual;
            actual = target.GetPlants();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DeletePlant
        ///</summary>
        [TestMethod()]
        public void DeletePlantTest()
        {
            PlantBLL target = new PlantBLL(); // TODO: Initialize to an appropriate value
            Plant plant = null; // TODO: Initialize to an appropriate value
            target.DeletePlant(plant);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for AddPlant
        ///</summary>
        [TestMethod()]
        public void AddPlantTest()
        {
            PlantBLL target = new PlantBLL(); // TODO: Initialize to an appropriate value
            Plant plant = null; // TODO: Initialize to an appropriate value
            target.AddPlant(plant);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for PlantBLL Constructor
        ///</summary>
        [TestMethod()]
        public void PlantBLLConstructorTest()
        {
            PlantBLL target = new PlantBLL();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for UpdatePlant
        ///</summary>
        [TestMethod()]
        public void UpdatePlantTest1()
        {
            PlantBLL target = new PlantBLL(); // TODO: Initialize to an appropriate value
            Plant plant = null; // TODO: Initialize to an appropriate value
            target.UpdatePlant(plant);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for GetPlants
        ///</summary>
        [TestMethod()]
        public void GetPlantsTest1()
        {
            PlantBLL target = new PlantBLL(); // TODO: Initialize to an appropriate value
            List<Plant> expected = null; // TODO: Initialize to an appropriate value
            List<Plant> actual;
            actual = target.GetPlants();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DeletePlant
        ///</summary>
        [TestMethod()]
        public void DeletePlantTest1()
        {
            PlantBLL target = new PlantBLL(); // TODO: Initialize to an appropriate value
            Plant plant = null; // TODO: Initialize to an appropriate value
            target.DeletePlant(plant);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for AddPlant
        ///</summary>
        [TestMethod()]
        public void AddPlantTest1()
        {
            PlantBLL target = new PlantBLL(); // TODO: Initialize to an appropriate value
            Plant plant = null; // TODO: Initialize to an appropriate value
            target.AddPlant(plant);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for PlantBLL Constructor
        ///</summary>
        [TestMethod()]
        public void PlantBLLConstructorTest1()
        {
            PlantBLL target = new PlantBLL();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
