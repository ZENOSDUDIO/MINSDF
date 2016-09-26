using SGM.Ecount.BLL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SGM.ECount.DataModel;
using System.Collections.Generic;
using System;

namespace BLLTest
{
    
    
    /// <summary>
    ///This is a test class for PartStatusBLLTest and is intended
    ///to contain all PartStatusBLLTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PartStatusBLLTest
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
        ///A test for GetPartStatus
        ///</summary>
        [TestMethod()]
        public void GetPartStatusTest()
        {
            PartStatusBLL target = new PartStatusBLL(); // TODO: Initialize to an appropriate value
            List<PartStatus> expected = null; // TODO: Initialize to an appropriate value
            int actual=2;
            actual = target.GetPartStatus().Count;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DeletePartStatus
        ///</summary>
        [TestMethod()]
        public void DeletePartStatusTest()
        {
            PartStatusBLL target = new PartStatusBLL(); // TODO: Initialize to an appropriate value
            PartStatus status = new PartStatus(); // TODO: Initialize to an appropriate value
            status.StatusID = 1;
            target.DeletePartStatus(status);
            //Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        [TestMethod()]
        public void UpdatePartStatusTest()
        {
            PartStatusBLL target = new PartStatusBLL(); // TODO: Initialize to an appropriate value
            List<PartStatus> list= target.GetPartStatus();

            PartStatus status = new PartStatus(); // TODO: Initialize to an appropriate value
            status.StatusID = list[0].StatusID;
            status.StatusName = list[0].StatusName;
            status.Available = !(list[0].Available);
            target.UpdatePartStatus(status);
            PartStatus actual = target.GetPartStatusByKey(status);
            Assert.AreEqual(status.Available, actual.Available);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for AddPartStatus
        ///</summary>
        [TestMethod()]
        public void AddPartStatusTest()
        {
            PartStatusBLL target = new PartStatusBLL(); // TODO: Initialize to an appropriate value
            PartStatus status = new PartStatus(); // TODO: Initialize to an appropriate value
            status.CycleCount = true;

            Random r=new Random(1);
            status.StatusName = "teststastus" + r.Next();            
            target.AddPartStatus(status);
            //Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for UpdatePartStatus
        ///</summary>
        [TestMethod()]
        public void UpdatePartStatusTest1()
        {
            PartStatusBLL target = new PartStatusBLL(); // TODO: Initialize to an appropriate value
            PartStatus status = null; // TODO: Initialize to an appropriate value
            target.UpdatePartStatus(status);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for GetPartStatusByKey
        ///</summary>
        [TestMethod()]
        public void GetPartStatusByKeyTest()
        {
            PartStatusBLL target = new PartStatusBLL(); // TODO: Initialize to an appropriate value
            PartStatus status = null; // TODO: Initialize to an appropriate value
            PartStatus expected = null; // TODO: Initialize to an appropriate value
            PartStatus actual;
            actual = target.GetPartStatusByKey(status);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetPartStatus
        ///</summary>
        [TestMethod()]
        public void GetPartStatusTest1()
        {
            PartStatusBLL target = new PartStatusBLL(); // TODO: Initialize to an appropriate value
            List<PartStatus> expected = null; // TODO: Initialize to an appropriate value
            List<PartStatus> actual;
            actual = target.GetPartStatus();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DeletePartStatus
        ///</summary>
        [TestMethod()]
        public void DeletePartStatusTest1()
        {
            PartStatusBLL target = new PartStatusBLL(); // TODO: Initialize to an appropriate value
            PartStatus status = null; // TODO: Initialize to an appropriate value
            target.DeletePartStatus(status);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for AddPartStatus
        ///</summary>
        [TestMethod()]
        public void AddPartStatusTest1()
        {
            PartStatusBLL target = new PartStatusBLL(); // TODO: Initialize to an appropriate value
            PartStatus status = null; // TODO: Initialize to an appropriate value
            target.AddPartStatus(status);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for PartStatusBLL Constructor
        ///</summary>
        [TestMethod()]
        public void PartStatusBLLConstructorTest()
        {
            PartStatusBLL target = new PartStatusBLL();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for UpdatePartStatus
        ///</summary>
        [TestMethod()]
        public void UpdatePartStatusTest2()
        {
            PartStatusBLL target = new PartStatusBLL(); // TODO: Initialize to an appropriate value
            PartStatus status = null; // TODO: Initialize to an appropriate value
            target.UpdatePartStatus(status);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for GetPartStatusByKey
        ///</summary>
        [TestMethod()]
        public void GetPartStatusByKeyTest1()
        {
            PartStatusBLL target = new PartStatusBLL(); // TODO: Initialize to an appropriate value
            PartStatus status = null; // TODO: Initialize to an appropriate value
            PartStatus expected = null; // TODO: Initialize to an appropriate value
            PartStatus actual;
            actual = target.GetPartStatusByKey(status);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetPartStatus
        ///</summary>
        [TestMethod()]
        public void GetPartStatusTest2()
        {
            PartStatusBLL target = new PartStatusBLL(); // TODO: Initialize to an appropriate value
            List<PartStatus> expected = null; // TODO: Initialize to an appropriate value
            List<PartStatus> actual;
            actual = target.GetPartStatus();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DeletePartStatus
        ///</summary>
        [TestMethod()]
        public void DeletePartStatusTest2()
        {
            PartStatusBLL target = new PartStatusBLL(); // TODO: Initialize to an appropriate value
            PartStatus status = null; // TODO: Initialize to an appropriate value
            target.DeletePartStatus(status);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for AddPartStatus
        ///</summary>
        [TestMethod()]
        public void AddPartStatusTest2()
        {
            PartStatusBLL target = new PartStatusBLL(); // TODO: Initialize to an appropriate value
            PartStatus status = null; // TODO: Initialize to an appropriate value
            target.AddPartStatus(status);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for PartStatusBLL Constructor
        ///</summary>
        [TestMethod()]
        public void PartStatusBLLConstructorTest1()
        {
            PartStatusBLL target = new PartStatusBLL();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
