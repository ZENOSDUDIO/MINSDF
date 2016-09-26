using SGM.Ecount.BLL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SGM.ECount.DataModel;
using System.Collections.Generic;

namespace BLLTest
{
    
    
    /// <summary>
    ///This is a test class for DifferenceAnalyzeBLLTest and is intended
    ///to contain all DifferenceAnalyzeBLLTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DifferenceAnalyzeBLLTest
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
        ///A test for GetDiferenceAnalyzeItems
        ///</summary>
        [TestMethod()]
        public void GetDiferenceAnalyzeItemsTest()
        {
            DifferenceAnalyzeBLL target = new DifferenceAnalyzeBLL(); // TODO: Initialize to an appropriate value
            List<DifferenceAnalyzeItem> expected = null; // TODO: Initialize to an appropriate value
            List<DifferenceAnalyzeItem> actual;
            actual = target.GetDiferenceAnalyzeItems();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetDiferenceAnalyzeDetails
        ///</summary>
        [TestMethod()]
        public void GetDiferenceAnalyzeDetailsTest()
        {
            DifferenceAnalyzeBLL target = new DifferenceAnalyzeBLL(); // TODO: Initialize to an appropriate value
            DifferenceAnalyzeItem item = null; // TODO: Initialize to an appropriate value
            List<DifferenceAnalyzeDetails> expected = null; // TODO: Initialize to an appropriate value
            List<DifferenceAnalyzeDetails> actual;
            actual = target.GetDiferenceAnalyzeDetails(item);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DeleteDifferenceAnalyzeItem
        ///</summary>
        [TestMethod()]
        public void DeleteDifferenceAnalyzeItemTest()
        {
            DifferenceAnalyzeBLL target = new DifferenceAnalyzeBLL(); // TODO: Initialize to an appropriate value
            DifferenceAnalyzeItem item = null; // TODO: Initialize to an appropriate value
            target.DeleteDifferenceAnalyzeItem(item);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for AddDifferenceAnalyzeItem
        ///</summary>
        [TestMethod()]
        public void AddDifferenceAnalyzeItemTest()
        {
            DifferenceAnalyzeBLL target = new DifferenceAnalyzeBLL(); // TODO: Initialize to an appropriate value
            DifferenceAnalyzeItem item = null; // TODO: Initialize to an appropriate value
            target.AddDifferenceAnalyzeItem(item);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for DifferenceAnalyzeBLL Constructor
        ///</summary>
        [TestMethod()]
        public void DifferenceAnalyzeBLLConstructorTest()
        {
            DifferenceAnalyzeBLL target = new DifferenceAnalyzeBLL();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for GetDiferenceAnalyzeItems
        ///</summary>
        [TestMethod()]
        public void GetDiferenceAnalyzeItemsTest1()
        {
            DifferenceAnalyzeBLL target = new DifferenceAnalyzeBLL(); // TODO: Initialize to an appropriate value
            List<DifferenceAnalyzeItem> expected = null; // TODO: Initialize to an appropriate value
            List<DifferenceAnalyzeItem> actual;
            actual = target.GetDiferenceAnalyzeItems();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetDiferenceAnalyzeDetails
        ///</summary>
        [TestMethod()]
        public void GetDiferenceAnalyzeDetailsTest1()
        {
            DifferenceAnalyzeBLL target = new DifferenceAnalyzeBLL(); // TODO: Initialize to an appropriate value
            DifferenceAnalyzeItem item = null; // TODO: Initialize to an appropriate value
            List<DifferenceAnalyzeDetails> expected = null; // TODO: Initialize to an appropriate value
            List<DifferenceAnalyzeDetails> actual;
            actual = target.GetDiferenceAnalyzeDetails(item);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DeleteDifferenceAnalyzeItem
        ///</summary>
        [TestMethod()]
        public void DeleteDifferenceAnalyzeItemTest1()
        {
            DifferenceAnalyzeBLL target = new DifferenceAnalyzeBLL(); // TODO: Initialize to an appropriate value
            DifferenceAnalyzeItem item = null; // TODO: Initialize to an appropriate value
            target.DeleteDifferenceAnalyzeItem(item);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for AddDifferenceAnalyzeItem
        ///</summary>
        [TestMethod()]
        public void AddDifferenceAnalyzeItemTest1()
        {
            DifferenceAnalyzeBLL target = new DifferenceAnalyzeBLL(); // TODO: Initialize to an appropriate value
            DifferenceAnalyzeItem item = null; // TODO: Initialize to an appropriate value
            target.AddDifferenceAnalyzeItem(item);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for DifferenceAnalyzeBLL Constructor
        ///</summary>
        [TestMethod()]
        public void DifferenceAnalyzeBLLConstructorTest1()
        {
            DifferenceAnalyzeBLL target = new DifferenceAnalyzeBLL();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
