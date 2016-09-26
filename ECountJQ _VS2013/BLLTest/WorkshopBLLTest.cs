using SGM.ECount.BLL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SGM.ECount.DataModel;
using System.Collections.Generic;

namespace BLLTest
{
    
    
    /// <summary>
    ///This is a test class for WorkshopBLLTest and is intended
    ///to contain all WorkshopBLLTest Unit Tests
    ///</summary>
    [TestClass()]
    public class WorkshopBLLTest
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
        ///A test for GetWorkshops
        ///</summary>
        [TestMethod()]
        public void GetWorkshopsTest()
        {
            WorkshopBLL target = new WorkshopBLL(); // TODO: Initialize to an appropriate value
            List<Workshop> expected = null; // TODO: Initialize to an appropriate value
            List<Workshop> actual;
            actual = target.GetWorkshops();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetWorkshopbyPlantID
        ///</summary>
        [TestMethod()]
        public void GetWorkshopbyPlantIDTest()
        {
            WorkshopBLL target = new WorkshopBLL(); // TODO: Initialize to an appropriate value
            int plantID = 0; // TODO: Initialize to an appropriate value
            List<Workshop> expected = null; // TODO: Initialize to an appropriate value
            List<Workshop> actual;
            actual = target.GetWorkshopbyPlantID(plantID);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetWorkshopbyPlant
        ///</summary>
        [TestMethod()]
        public void GetWorkshopbyPlantTest()
        {
            WorkshopBLL target = new WorkshopBLL(); // TODO: Initialize to an appropriate value
            Plant plant = null; // TODO: Initialize to an appropriate value
            List<Workshop> expected = null; // TODO: Initialize to an appropriate value
            List<Workshop> actual;
            actual = target.GetWorkshopbyPlant(plant);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for WorkshopBLL Constructor
        ///</summary>
        [TestMethod()]
        public void WorkshopBLLConstructorTest()
        {
            WorkshopBLL target = new WorkshopBLL();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }


        /// <summary>
        ///A test for WorkshopBLL Constructor
        ///</summary>
        [TestMethod()]
        public void UpdateWorkshopTest()
        {
            WorkshopBLL target = new WorkshopBLL();
            Workshop workshop = new Workshop { WorkshopID = 21, Plant = new Plant { PlantID = 6 }, Available = true, WorkshopCode = "dd", WorshopName = "ddd" };
            target.UpdateWorkshop(workshop);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for WorkshopBLL Constructor
        ///</summary>
        [TestMethod()]
        public void DeleteWorkshopTest()
        {      
            WorkshopBLL target = new WorkshopBLL();
            Workshop workshop = new Workshop { WorkshopID = 24};
            target.DeleteWorkshop(workshop);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
