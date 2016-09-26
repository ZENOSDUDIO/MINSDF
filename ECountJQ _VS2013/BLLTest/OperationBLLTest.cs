using SGM.Ecount.BLL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SGM.ECount.DataModel;
using System.Collections.Generic;

namespace BLLTest
{
    
    
    /// <summary>
    ///This is a test class for OperationBLLTest and is intended
    ///to contain all OperationBLLTest Unit Tests
    ///</summary>
    [TestClass()]
    public class OperationBLLTest
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
        ///A test for GetUserGroupbyOperation
        ///</summary>
        [TestMethod()]
        public void GetUserGroupbyOperationTest()
        {
            OperationBLL target = new OperationBLL(); // TODO: Initialize to an appropriate value
            Operation operation = null; // TODO: Initialize to an appropriate value
            List<UserGroup> expected = null; // TODO: Initialize to an appropriate value
            List<UserGroup> actual;
            actual = target.GetUserGroupbyOperation(operation);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetOperations
        ///</summary>
        [TestMethod()]
        public void GetOperationsTest()
        {
            OperationBLL target = new OperationBLL(); // TODO: Initialize to an appropriate value
            List<Operation> expected = null; // TODO: Initialize to an appropriate value
            List<Operation> actual;
            actual = target.GetOperations();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetOperationbyKey
        ///</summary>
        [TestMethod()]
        public void GetOperationbyKeyTest()
        {
            OperationBLL target = new OperationBLL(); // TODO: Initialize to an appropriate value
            Operation operation = null; // TODO: Initialize to an appropriate value
            Operation expected = null; // TODO: Initialize to an appropriate value
            Operation actual;
            actual = target.GetOperationbyKey(operation);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for OperationBLL Constructor
        ///</summary>
        [TestMethod()]
        public void OperationBLLConstructorTest()
        {
            OperationBLL target = new OperationBLL();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for GetUserGroupbyOperation
        ///</summary>
        [TestMethod()]
        public void GetUserGroupbyOperationTest1()
        {
            OperationBLL target = new OperationBLL(); // TODO: Initialize to an appropriate value
            Operation operation = null; // TODO: Initialize to an appropriate value
            List<UserGroup> expected = null; // TODO: Initialize to an appropriate value
            List<UserGroup> actual;
            actual = target.GetUserGroupbyOperation(operation);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetOperations
        ///</summary>
        [TestMethod()]
        public void GetOperationsTest1()
        {
            OperationBLL target = new OperationBLL(); // TODO: Initialize to an appropriate value
            List<Operation> expected = null; // TODO: Initialize to an appropriate value
            List<Operation> actual;
            actual = target.GetOperations();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetOperationbyKey
        ///</summary>
        [TestMethod()]
        public void GetOperationbyKeyTest1()
        {
            OperationBLL target = new OperationBLL(); // TODO: Initialize to an appropriate value
            Operation operation = null; // TODO: Initialize to an appropriate value
            Operation expected = null; // TODO: Initialize to an appropriate value
            Operation actual;
            actual = target.GetOperationbyKey(operation);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for OperationBLL Constructor
        ///</summary>
        [TestMethod()]
        public void OperationBLLConstructorTest1()
        {
            OperationBLL target = new OperationBLL();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
