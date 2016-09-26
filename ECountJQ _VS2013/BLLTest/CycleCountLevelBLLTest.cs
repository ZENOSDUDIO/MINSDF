using SGM.Ecount.BLL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SGM.ECount.DataModel;
using System.Collections.Generic;

namespace BLLTest
{
    
    
    /// <summary>
    ///This is a test class for CycleCountLevelBLLTest and is intended
    ///to contain all CycleCountLevelBLLTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CycleCountLevelBLLTest
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
        ///A test for UpdateCycleCountLevel
        ///</summary>
        [TestMethod()]
        public void UpdateCycleCountLevelTest()
        {
            CycleCountLevelBLL target = new CycleCountLevelBLL(); // TODO: Initialize to an appropriate value
            CycleCountLevel level = null; // TODO: Initialize to an appropriate value
            target.UpdateCycleCountLevel(level);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for GetCycleCountLevel
        ///</summary>
        [TestMethod()]
        public void GetCycleCountLevelTest()
        {
            CycleCountLevelBLL target = new CycleCountLevelBLL(); // TODO: Initialize to an appropriate value
            List<CycleCountLevel> expected = null; // TODO: Initialize to an appropriate value
            List<CycleCountLevel> actual;
            actual = target.GetCycleCountLevel();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DeleteCycleCountLevel
        ///</summary>
        [TestMethod()]
        public void DeleteCycleCountLevelTest()
        {
            CycleCountLevelBLL target = new CycleCountLevelBLL(); // TODO: Initialize to an appropriate value
            CycleCountLevel level = null; // TODO: Initialize to an appropriate value
            target.DeleteCycleCountLevel(level);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for AddCycleCountLevel
        ///</summary>
        [TestMethod()]
        public void AddCycleCountLevelTest()
        {
            CycleCountLevelBLL target = new CycleCountLevelBLL(); // TODO: Initialize to an appropriate value
            CycleCountLevel level = null; // TODO: Initialize to an appropriate value
            target.AddCycleCountLevel(level);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for CycleCountLevelBLL Constructor
        ///</summary>
        [TestMethod()]
        public void CycleCountLevelBLLConstructorTest()
        {
            CycleCountLevelBLL target = new CycleCountLevelBLL();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for UpdateCycleCountLevel
        ///</summary>
        [TestMethod()]
        public void UpdateCycleCountLevelTest1()
        {
            CycleCountLevelBLL target = new CycleCountLevelBLL(); // TODO: Initialize to an appropriate value
            CycleCountLevel level = null; // TODO: Initialize to an appropriate value
            target.UpdateCycleCountLevel(level);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for GetCycleCountLevel
        ///</summary>
        [TestMethod()]
        public void GetCycleCountLevelTest1()
        {
            CycleCountLevelBLL target = new CycleCountLevelBLL(); // TODO: Initialize to an appropriate value
            List<CycleCountLevel> expected = null; // TODO: Initialize to an appropriate value
            List<CycleCountLevel> actual;
            actual = target.GetCycleCountLevel();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DeleteCycleCountLevel
        ///</summary>
        [TestMethod()]
        public void DeleteCycleCountLevelTest1()
        {
            CycleCountLevelBLL target = new CycleCountLevelBLL(); // TODO: Initialize to an appropriate value
            CycleCountLevel level = null; // TODO: Initialize to an appropriate value
            target.DeleteCycleCountLevel(level);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for AddCycleCountLevel
        ///</summary>
        [TestMethod()]
        public void AddCycleCountLevelTest1()
        {
            CycleCountLevelBLL target = new CycleCountLevelBLL(); // TODO: Initialize to an appropriate value
            CycleCountLevel level = null; // TODO: Initialize to an appropriate value
            target.AddCycleCountLevel(level);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for CycleCountLevelBLL Constructor
        ///</summary>
        [TestMethod()]
        public void CycleCountLevelBLLConstructorTest1()
        {
            CycleCountLevelBLL target = new CycleCountLevelBLL();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
