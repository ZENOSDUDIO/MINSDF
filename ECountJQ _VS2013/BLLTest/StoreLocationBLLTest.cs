using SGM.Ecount.BLL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SGM.ECount.DataModel;
using System.Collections.Generic;

namespace BLLTest
{
    
    
    /// <summary>
    ///This is a test class for StoreLocationBLLTest and is intended
    ///to contain all StoreLocationBLLTest Unit Tests
    ///</summary>
    [TestClass()]
    public class StoreLocationBLLTest
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
        ///A test for UpdateStoreLocation
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ECountBLL.dll")]
        public void UpdateStoreLocationTest()
        {
            StoreLocationBLL_Accessor target = new StoreLocationBLL_Accessor(); // TODO: Initialize to an appropriate value
            StoreLocation location = null; // TODO: Initialize to an appropriate value
            target.UpdateStoreLocation(location);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for GetStoreLocationbyKey
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ECountBLL.dll")]
        public void GetStoreLocationbyKeyTest()
        {
            StoreLocationBLL_Accessor target = new StoreLocationBLL_Accessor(); // TODO: Initialize to an appropriate value
            StoreLocation location = null; // TODO: Initialize to an appropriate value
            StoreLocation expected = null; // TODO: Initialize to an appropriate value
            StoreLocation actual;
            actual = target.GetStoreLocationbyKey(location);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetStoreLocation
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ECountBLL.dll")]
        public void GetStoreLocationTest()
        {
            StoreLocationBLL_Accessor target = new StoreLocationBLL_Accessor(); // TODO: Initialize to an appropriate value
            List<StoreLocation> expected = null; // TODO: Initialize to an appropriate value
            List<StoreLocation> actual;
            actual = target.GetStoreLocation();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DeleteStoreLocation
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ECountBLL.dll")]
        public void DeleteStoreLocationTest()
        {
            StoreLocationBLL_Accessor target = new StoreLocationBLL_Accessor(); // TODO: Initialize to an appropriate value
            StoreLocation location = null; // TODO: Initialize to an appropriate value
            target.DeleteStoreLocation(location);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for AddStoreLocation
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ECountBLL.dll")]
        public void AddStoreLocationTest()
        {
            StoreLocationBLL_Accessor target = new StoreLocationBLL_Accessor(); // TODO: Initialize to an appropriate value
            StoreLocation location = null; // TODO: Initialize to an appropriate value
            target.AddStoreLocation(location);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for StoreLocationBLL Constructor
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ECountBLL.dll")]
        public void StoreLocationBLLConstructorTest()
        {
            StoreLocationBLL_Accessor target = new StoreLocationBLL_Accessor();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for UpdateStoreLocation
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ECountBLL.dll")]
        public void UpdateStoreLocationTest1()
        {
            StoreLocationBLL_Accessor target = new StoreLocationBLL_Accessor(); // TODO: Initialize to an appropriate value
            StoreLocation location = null; // TODO: Initialize to an appropriate value
            target.UpdateStoreLocation(location);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for GetStoreLocationbyKey
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ECountBLL.dll")]
        public void GetStoreLocationbyKeyTest1()
        {
            StoreLocationBLL_Accessor target = new StoreLocationBLL_Accessor(); // TODO: Initialize to an appropriate value
            StoreLocation location = null; // TODO: Initialize to an appropriate value
            StoreLocation expected = null; // TODO: Initialize to an appropriate value
            StoreLocation actual;
            actual = target.GetStoreLocationbyKey(location);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetStoreLocation
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ECountBLL.dll")]
        public void GetStoreLocationTest1()
        {
            StoreLocationBLL_Accessor target = new StoreLocationBLL_Accessor(); // TODO: Initialize to an appropriate value
            List<StoreLocation> expected = null; // TODO: Initialize to an appropriate value
            List<StoreLocation> actual;
            actual = target.GetStoreLocation();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DeleteStoreLocation
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ECountBLL.dll")]
        public void DeleteStoreLocationTest1()
        {
            StoreLocationBLL_Accessor target = new StoreLocationBLL_Accessor(); // TODO: Initialize to an appropriate value
            StoreLocation location = null; // TODO: Initialize to an appropriate value
            target.DeleteStoreLocation(location);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for AddStoreLocation
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ECountBLL.dll")]
        public void AddStoreLocationTest1()
        {
            StoreLocationBLL_Accessor target = new StoreLocationBLL_Accessor(); // TODO: Initialize to an appropriate value
            StoreLocation location = null; // TODO: Initialize to an appropriate value
            target.AddStoreLocation(location);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for StoreLocationBLL Constructor
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ECountBLL.dll")]
        public void StoreLocationBLLConstructorTest1()
        {
            StoreLocationBLL_Accessor target = new StoreLocationBLL_Accessor();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
