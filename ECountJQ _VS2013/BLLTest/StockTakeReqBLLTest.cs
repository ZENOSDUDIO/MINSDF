using SGM.Ecount.BLL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SGM.ECount.DataModel;
using System.Linq;
using System;
using System.Collections.Generic;

namespace BLLTest
{
    
    
    /// <summary>
    ///This is a test class for StockTakeReqBLLTest and is intended
    ///to contain all StockTakeReqBLLTest Unit Tests
    ///</summary>
    [TestClass()]
    public class StockTakeReqBLLTest
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
        ///A test for UpdateStocktakeRequest
        ///</summary>
        [TestMethod()]
        public void UpdateStocktakeRequestTest()
        {
            StockTakeReqBLL target = new StockTakeReqBLL(); // TODO: Initialize to an appropriate value
            StocktakeRequest request = null; // TODO: Initialize to an appropriate value
            target.UpdateStocktakeRequest(request);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for QueryRequestDetailsByPage
        ///</summary>
        [TestMethod()]
        public void QueryRequestDetailsByPageTest()
        {
            StockTakeReqBLL target = new StockTakeReqBLL(); // TODO: Initialize to an appropriate value
            ViewStockTakeRequest condition = null; // TODO: Initialize to an appropriate value
            int pageSize = 0; // TODO: Initialize to an appropriate value
            int pageNumber = 0; // TODO: Initialize to an appropriate value
            int pageCount = 0; // TODO: Initialize to an appropriate value
            int pageCountExpected = 0; // TODO: Initialize to an appropriate value
            IQueryable<ViewStockTakeRequest> expected = null; // TODO: Initialize to an appropriate value
            IQueryable<ViewStockTakeRequest> actual;
            actual = target.QueryRequestDetailsByPage(condition, pageSize, pageNumber, out pageCount);
            Assert.AreEqual(pageCountExpected, pageCount);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for QueryRequestDetails
        ///</summary>
        [TestMethod()]
        public void QueryRequestDetailsTest()
        {
            StockTakeReqBLL target = new StockTakeReqBLL(); // TODO: Initialize to an appropriate value
            ViewStockTakeRequest condition = null; // TODO: Initialize to an appropriate value
            IQueryable<ViewStockTakeRequest> expected = null; // TODO: Initialize to an appropriate value
            IQueryable<ViewStockTakeRequest> actual;
            actual = target.QueryRequestDetails(condition);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for QueryRequest
        ///</summary>
        [TestMethod()]
        public void QueryRequestTest()
        {
            StockTakeReqBLL target = new StockTakeReqBLL(); // TODO: Initialize to an appropriate value
            ViewStockTakeRequest condition = null; // TODO: Initialize to an appropriate value
            List<StocktakeRequest> expected = null; // TODO: Initialize to an appropriate value
            List<StocktakeRequest> actual;
            actual = target.QueryRequest(condition);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetRequestByUser
        ///</summary>
        [TestMethod()]
        public void GetRequestByUserTest()
        {
            StockTakeReqBLL target = new StockTakeReqBLL(); // TODO: Initialize to an appropriate value
            User user = null; // TODO: Initialize to an appropriate value
            List<StocktakeRequest> expected = null; // TODO: Initialize to an appropriate value
            List<StocktakeRequest> actual;
            actual = target.GetRequestByUser(user);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetRequest
        ///</summary>
        [TestMethod()]
        public void GetRequestTest()
        {
            StockTakeReqBLL target = new StockTakeReqBLL(); // TODO: Initialize to an appropriate value
            StocktakeRequest request = null; // TODO: Initialize to an appropriate value
            StocktakeRequest expected = null; // TODO: Initialize to an appropriate value
            StocktakeRequest actual;
            actual = target.GetRequest(request);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CreateStocktakeRequest
        ///</summary>
        [TestMethod()]
        public void CreateStocktakeRequestTest1()
        {
            StockTakeReqBLL target = new StockTakeReqBLL(); // TODO: Initialize to an appropriate value
            StocktakeRequest request = null; // TODO: Initialize to an appropriate value
            bool retrieveNew = false; // TODO: Initialize to an appropriate value
            StocktakeRequest expected = null; // TODO: Initialize to an appropriate value
            StocktakeRequest actual;
            actual = target.CreateStocktakeRequest(request, retrieveNew);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CreateStocktakeRequest
        ///</summary>
        [TestMethod()]
        public void CreateStocktakeRequestTest()
        {
            StockTakeReqBLL target = new StockTakeReqBLL(); // TODO: Initialize to an appropriate value
            StocktakeRequest request = null; // TODO: Initialize to an appropriate value
            StocktakeRequest expected = null; // TODO: Initialize to an appropriate value
            StocktakeRequest actual;
            actual = target.CreateStocktakeRequest(request);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for BuildQueryCondition
        ///</summary>
        [TestMethod()]
        public void BuildQueryConditionTest()
        {
            StockTakeReqBLL target = new StockTakeReqBLL(); // TODO: Initialize to an appropriate value
            Nullable<long> requestID = new Nullable<long>(); // TODO: Initialize to an appropriate value
            string requestNumber = string.Empty; // TODO: Initialize to an appropriate value
            string userName = string.Empty; // TODO: Initialize to an appropriate value
            Nullable<int> plantID = new Nullable<int>(); // TODO: Initialize to an appropriate value
            string partCode = string.Empty; // TODO: Initialize to an appropriate value
            Nullable<int> stocktakeType = new Nullable<int>(); // TODO: Initialize to an appropriate value
            string partChineseName = string.Empty; // TODO: Initialize to an appropriate value
            Nullable<DateTime> dateStart = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            Nullable<DateTime> dateEnd = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            ViewStockTakeRequest expected = null; // TODO: Initialize to an appropriate value
            ViewStockTakeRequest actual;
            actual = target.BuildQueryCondition(requestID, requestNumber, userName, plantID, partCode, stocktakeType, partChineseName, dateStart, dateEnd);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for BuildQuery
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ECountBLL.dll")]
        public void BuildQueryTest()
        {
            StockTakeReqBLL_Accessor target = new StockTakeReqBLL_Accessor(); // TODO: Initialize to an appropriate value
            ViewStockTakeRequest condition = null; // TODO: Initialize to an appropriate value
            IQueryable<ViewStockTakeRequest> requestQuery = null; // TODO: Initialize to an appropriate value
            IQueryable<ViewStockTakeRequest> expected = null; // TODO: Initialize to an appropriate value
            IQueryable<ViewStockTakeRequest> actual;
            actual = target.BuildQuery(condition, requestQuery);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for StockTakeReqBLL Constructor
        ///</summary>
        [TestMethod()]
        public void StockTakeReqBLLConstructorTest()
        {
            StockTakeReqBLL target = new StockTakeReqBLL();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for UpdateStocktakeRequest
        ///</summary>
        [TestMethod()]
        public void UpdateStocktakeRequestTest1()
        {
            StockTakeReqBLL target = new StockTakeReqBLL(); // TODO: Initialize to an appropriate value
            StocktakeRequest request = null; // TODO: Initialize to an appropriate value
            target.UpdateStocktakeRequest(request);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for QueryRequestDetailsByPage
        ///</summary>
        [TestMethod()]
        public void QueryRequestDetailsByPageTest1()
        {
            StockTakeReqBLL target = new StockTakeReqBLL(); // TODO: Initialize to an appropriate value
            ViewStockTakeRequest condition = null; // TODO: Initialize to an appropriate value
            int pageSize = 0; // TODO: Initialize to an appropriate value
            int pageNumber = 0; // TODO: Initialize to an appropriate value
            int pageCount = 0; // TODO: Initialize to an appropriate value
            int pageCountExpected = 0; // TODO: Initialize to an appropriate value
            IQueryable<ViewStockTakeRequest> expected = null; // TODO: Initialize to an appropriate value
            IQueryable<ViewStockTakeRequest> actual;
            actual = target.QueryRequestDetailsByPage(condition, pageSize, pageNumber, out pageCount);
            Assert.AreEqual(pageCountExpected, pageCount);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for QueryRequestDetails
        ///</summary>
        [TestMethod()]
        public void QueryRequestDetailsTest1()
        {
            StockTakeReqBLL target = new StockTakeReqBLL(); // TODO: Initialize to an appropriate value
            ViewStockTakeRequest condition = null; // TODO: Initialize to an appropriate value
            IQueryable<ViewStockTakeRequest> expected = null; // TODO: Initialize to an appropriate value
            IQueryable<ViewStockTakeRequest> actual;
            actual = target.QueryRequestDetails(condition);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for QueryRequest
        ///</summary>
        [TestMethod()]
        public void QueryRequestTest1()
        {
            StockTakeReqBLL target = new StockTakeReqBLL(); // TODO: Initialize to an appropriate value
            ViewStockTakeRequest condition = null; // TODO: Initialize to an appropriate value
            List<StocktakeRequest> expected = null; // TODO: Initialize to an appropriate value
            List<StocktakeRequest> actual;
            actual = target.QueryRequest(condition);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetRequestByUser
        ///</summary>
        [TestMethod()]
        public void GetRequestByUserTest1()
        {
            StockTakeReqBLL target = new StockTakeReqBLL(); // TODO: Initialize to an appropriate value
            User user = null; // TODO: Initialize to an appropriate value
            List<StocktakeRequest> expected = null; // TODO: Initialize to an appropriate value
            List<StocktakeRequest> actual;
            actual = target.GetRequestByUser(user);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetRequest
        ///</summary>
        [TestMethod()]
        public void GetRequestTest1()
        {
            StockTakeReqBLL target = new StockTakeReqBLL(); // TODO: Initialize to an appropriate value
            StocktakeRequest request = null; // TODO: Initialize to an appropriate value
            StocktakeRequest expected = null; // TODO: Initialize to an appropriate value
            StocktakeRequest actual;
            actual = target.GetRequest(request);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CreateStocktakeRequest
        ///</summary>
        [TestMethod()]
        public void CreateStocktakeRequestTest3()
        {
            StockTakeReqBLL target = new StockTakeReqBLL(); // TODO: Initialize to an appropriate value
            StocktakeRequest request = null; // TODO: Initialize to an appropriate value
            bool retrieveNew = false; // TODO: Initialize to an appropriate value
            StocktakeRequest expected = null; // TODO: Initialize to an appropriate value
            StocktakeRequest actual;
            actual = target.CreateStocktakeRequest(request, retrieveNew);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CreateStocktakeRequest
        ///</summary>
        [TestMethod()]
        public void CreateStocktakeRequestTest2()
        {
            StockTakeReqBLL target = new StockTakeReqBLL(); // TODO: Initialize to an appropriate value
            StocktakeRequest request = null; // TODO: Initialize to an appropriate value
            StocktakeRequest expected = null; // TODO: Initialize to an appropriate value
            StocktakeRequest actual;
            actual = target.CreateStocktakeRequest(request);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for BuildQueryCondition
        ///</summary>
        [TestMethod()]
        public void BuildQueryConditionTest1()
        {
            StockTakeReqBLL target = new StockTakeReqBLL(); // TODO: Initialize to an appropriate value
            Nullable<long> requestID = new Nullable<long>(); // TODO: Initialize to an appropriate value
            string requestNumber = string.Empty; // TODO: Initialize to an appropriate value
            string userName = string.Empty; // TODO: Initialize to an appropriate value
            Nullable<int> plantID = new Nullable<int>(); // TODO: Initialize to an appropriate value
            string partCode = string.Empty; // TODO: Initialize to an appropriate value
            Nullable<int> stocktakeType = new Nullable<int>(); // TODO: Initialize to an appropriate value
            string partChineseName = string.Empty; // TODO: Initialize to an appropriate value
            Nullable<DateTime> dateStart = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            Nullable<DateTime> dateEnd = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            ViewStockTakeRequest expected = null; // TODO: Initialize to an appropriate value
            ViewStockTakeRequest actual;
            actual = target.BuildQueryCondition(requestID, requestNumber, userName, plantID, partCode, stocktakeType, partChineseName, dateStart, dateEnd);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for BuildQuery
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ECountBLL.dll")]
        public void BuildQueryTest1()
        {
            StockTakeReqBLL_Accessor target = new StockTakeReqBLL_Accessor(); // TODO: Initialize to an appropriate value
            ViewStockTakeRequest condition = null; // TODO: Initialize to an appropriate value
            IQueryable<ViewStockTakeRequest> requestQuery = null; // TODO: Initialize to an appropriate value
            IQueryable<ViewStockTakeRequest> expected = null; // TODO: Initialize to an appropriate value
            IQueryable<ViewStockTakeRequest> actual;
            actual = target.BuildQuery(condition, requestQuery);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for StockTakeReqBLL Constructor
        ///</summary>
        [TestMethod()]
        public void StockTakeReqBLLConstructorTest1()
        {
            StockTakeReqBLL target = new StockTakeReqBLL();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
