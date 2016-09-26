using SGM.Ecount.BLL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SGM.ECount.DataModel;
using System.Collections.Generic;

namespace BLLTest
{
    
    
    /// <summary>
    ///This is a test class for PartCategoryBLLTest and is intended
    ///to contain all PartCategoryBLLTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PartCategoryBLLTest
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
        ///A test for GetPartCategories
        ///</summary>
        [TestMethod()]
        public void GetPartCategoriesTest()
        {
            PartCategoryBLL target = new PartCategoryBLL(); // TODO: Initialize to an appropriate value
            List<PartCategory> expected = null; // TODO: Initialize to an appropriate value
            List<PartCategory> actual;
            actual = target.GetPartCategories();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for PartCategoryBLL Constructor
        ///</summary>
        [TestMethod()]
        public void PartCategoryBLLConstructorTest()
        {
            PartCategoryBLL target = new PartCategoryBLL();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for GetPartCategories
        ///</summary>
        [TestMethod()]
        public void GetPartCategoriesTest1()
        {
            PartCategoryBLL target = new PartCategoryBLL(); // TODO: Initialize to an appropriate value
            List<PartCategory> expected = null; // TODO: Initialize to an appropriate value
            List<PartCategory> actual;
            actual = target.GetPartCategories();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for PartCategoryBLL Constructor
        ///</summary>
        [TestMethod()]
        public void PartCategoryBLLConstructorTest1()
        {
            PartCategoryBLL target = new PartCategoryBLL();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
