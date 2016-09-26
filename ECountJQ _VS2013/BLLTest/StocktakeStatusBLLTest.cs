using SGM.ECount.BLL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SGM.ECount.DataModel;
using System.Collections.Generic;

namespace BLLTest
{
    
    
    /// <summary>
    ///This is a test class for StocktakeStatusBLLTest and is intended
    ///to contain all StocktakeStatusBLLTest Unit Tests
    ///</summary>
    [TestClass()]
    public class StocktakeStatusBLLTest
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
        ///A test for GetStocktakeStatus
        ///</summary>
        [TestMethod()]
        public void GetStocktakeStatusTest()
        {
            StocktakeStatusBLL target = new StocktakeStatusBLL(); // TODO: Initialize to an appropriate value
            List<StocktakeStatus> expected = null; // TODO: Initialize to an appropriate value
            List<StocktakeStatus> actual;
            actual = target.GetStocktakeStatus();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for StocktakeStatusBLL Constructor
        ///</summary>
        [TestMethod()]
        public void StocktakeStatusBLLConstructorTest()
        {
            StocktakeStatusBLL target = new StocktakeStatusBLL();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for GetStocktakeStatus
        ///</summary>
        [TestMethod()]
        public void GetStocktakeStatusTest1()
        {
            StocktakeStatusBLL target = new StocktakeStatusBLL(); // TODO: Initialize to an appropriate value
            List<StocktakeStatus> expected = null; // TODO: Initialize to an appropriate value
            List<StocktakeStatus> actual;
            actual = target.GetStocktakeStatus();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for StocktakeStatusBLL Constructor
        ///</summary>
        [TestMethod()]
        public void StocktakeStatusBLLConstructorTest1()
        {
            StocktakeStatusBLL target = new StocktakeStatusBLL();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
