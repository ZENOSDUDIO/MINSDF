using SGM.ECount.BLL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SGM.ECount.DataModel;
using System.Collections.Generic;

namespace BLLTest
{
    
    
    /// <summary>
    ///This is a test class for SupplierBLLTest and is intended
    ///to contain all SupplierBLLTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SupplierBLLTest
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
        ///A test for GetSupplier
        ///</summary>
        [TestMethod()]
        public void GetSupplierTest()
        {
            SupplierBLL target = new SupplierBLL(); // TODO: Initialize to an appropriate value
            List<Supplier> expected = null; // TODO: Initialize to an appropriate value
            List<Supplier> actual;
            int count;
            Supplier s = new Supplier { DUNS = "66" };
            actual = target.QuerySupplierByPage(s, 6, 2, out count);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for SupplierBLL Constructor
        ///</summary>
        [TestMethod()]
        public void SupplierBLLConstructorTest()
        {
            SupplierBLL target = new SupplierBLL();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }


  
    }
}
