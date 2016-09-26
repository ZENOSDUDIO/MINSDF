using SGM.Ecount.BLL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SGM.ECount.DataModel;
using System;
using System.Linq;
using System.Collections.Generic;

namespace BLLTest
{
    
    
    /// <summary>
    ///This is a test class for StocktakeDetailBLLTest and is intended
    ///to contain all StocktakeDetailBLLTest Unit Tests
    ///</summary>
    [TestClass()]
    public class StocktakeDetailBLLTest
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
        ///A test for QueryDetails
        ///</summary>
        [TestMethod()]
        public void QueryDetailsTest()
        {
            StocktakeDetailBLL target = new StocktakeDetailBLL(); // TODO: Initialize to an appropriate value
            StocktakeDetails details = null; // TODO: Initialize to an appropriate value
            Nullable<DateTime> dateStart = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            Nullable<DateTime> dateEnd = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            List<StocktakeDetails> expected = null; // TODO: Initialize to an appropriate value
            List<StocktakeDetails> actual;
            actual = target.QueryDetails(details, dateStart, dateEnd);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetDetailsByUser
        ///</summary>
        [TestMethod()]
        public void GetDetailsByUserTest()
        {
            StocktakeDetailBLL target = new StocktakeDetailBLL(); // TODO: Initialize to an appropriate value
            StocktakeNotification notification = null; // TODO: Initialize to an appropriate value
            User user = null; // TODO: Initialize to an appropriate value
            List<StocktakeDetails> expected = null; // TODO: Initialize to an appropriate value
            List<StocktakeDetails> actual;
            actual = target.GetDetailsByUser(notification, user);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DeleteDetails
        ///</summary>
        [TestMethod()]
        public void DeleteDetailsTest()
        {
            StocktakeDetailBLL target = new StocktakeDetailBLL(); // TODO: Initialize to an appropriate value
            StocktakeDetails details = null; // TODO: Initialize to an appropriate value
            target.DeleteDetails(details);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for BuildQuery
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ECountBLL.dll")]
        public void BuildQueryTest()
        {
            StocktakeDetailBLL_Accessor target = new StocktakeDetailBLL_Accessor(); // TODO: Initialize to an appropriate value
            StocktakeDetails details = null; // TODO: Initialize to an appropriate value
            Nullable<DateTime> dateStart = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            Nullable<DateTime> dateEnd = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            IQueryable<StocktakeDetails> expected = null; // TODO: Initialize to an appropriate value
            IQueryable<StocktakeDetails> actual;
            actual = target.BuildQuery(details, dateStart, dateEnd);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for AddDetails
        ///</summary>
        [TestMethod()]
        public void AddDetailsTest()
        {
            StocktakeDetailBLL target = new StocktakeDetailBLL(); // TODO: Initialize to an appropriate value
            StocktakeDetails details = null; // TODO: Initialize to an appropriate value
            target.AddDetails(details);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for StocktakeDetailBLL Constructor
        ///</summary>
        [TestMethod()]
        public void StocktakeDetailBLLConstructorTest()
        {
            StocktakeDetailBLL target = new StocktakeDetailBLL();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for QueryDetails
        ///</summary>
        [TestMethod()]
        public void QueryDetailsTest1()
        {
            StocktakeDetailBLL target = new StocktakeDetailBLL(); // TODO: Initialize to an appropriate value
            StocktakeDetails details = null; // TODO: Initialize to an appropriate value
            Nullable<DateTime> dateStart = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            Nullable<DateTime> dateEnd = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            List<StocktakeDetails> expected = null; // TODO: Initialize to an appropriate value
            List<StocktakeDetails> actual;
            actual = target.QueryDetails(details, dateStart, dateEnd);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetDetailsByUser
        ///</summary>
        [TestMethod()]
        public void GetDetailsByUserTest1()
        {
            StocktakeDetailBLL target = new StocktakeDetailBLL(); // TODO: Initialize to an appropriate value
            StocktakeNotification notification = null; // TODO: Initialize to an appropriate value
            User user = null; // TODO: Initialize to an appropriate value
            List<StocktakeDetails> expected = null; // TODO: Initialize to an appropriate value
            List<StocktakeDetails> actual;
            actual = target.GetDetailsByUser(notification, user);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DeleteDetails
        ///</summary>
        [TestMethod()]
        public void DeleteDetailsTest1()
        {
            StocktakeDetailBLL target = new StocktakeDetailBLL(); // TODO: Initialize to an appropriate value
            StocktakeDetails details = null; // TODO: Initialize to an appropriate value
            target.DeleteDetails(details);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for BuildQuery
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ECountBLL.dll")]
        public void BuildQueryTest1()
        {
            StocktakeDetailBLL_Accessor target = new StocktakeDetailBLL_Accessor(); // TODO: Initialize to an appropriate value
            StocktakeDetails details = null; // TODO: Initialize to an appropriate value
            Nullable<DateTime> dateStart = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            Nullable<DateTime> dateEnd = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            IQueryable<StocktakeDetails> expected = null; // TODO: Initialize to an appropriate value
            IQueryable<StocktakeDetails> actual;
            actual = target.BuildQuery(details, dateStart, dateEnd);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for AddDetails
        ///</summary>
        [TestMethod()]
        public void AddDetailsTest1()
        {
            StocktakeDetailBLL target = new StocktakeDetailBLL(); // TODO: Initialize to an appropriate value
            StocktakeDetails details = null; // TODO: Initialize to an appropriate value
            target.AddDetails(details);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for StocktakeDetailBLL Constructor
        ///</summary>
        [TestMethod()]
        public void StocktakeDetailBLLConstructorTest1()
        {
            StocktakeDetailBLL target = new StocktakeDetailBLL();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
