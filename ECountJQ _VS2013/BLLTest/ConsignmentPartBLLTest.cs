using SGM.ECount.BLL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SGM.ECount.DataModel;
using System.Collections.Generic;
using System.Linq;
using System;

namespace BLLTest
{
    
    
    /// <summary>
    ///This is a test class for ConsignmentPartBLLTest and is intended
    ///to contain all ConsignmentPartBLLTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ConsignmentPartBLLTest
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
        ///A test for UpdateConsignmentPartRecord
        ///</summary>
        [TestMethod()]
        public void UpdateConsignmentPartRecordTest()
        {
            
            ConsignmentPartBLL target = new ConsignmentPartBLL(); // TODO: Initialize to an appropriate value
            ConsignmentPartRecord record = new ConsignmentPartRecord { RecordID = new Guid("7791DD67-9388-4A14-A64C-B7D48BAB4AB7"), Part = new Part { PartID = new Guid("6D95D410-994C-4DB2-803D-0012CBB90BD9") }, Supplier = new Supplier { SupplierID = 2 },Description="test" };
            
            target.UpdateConsignmentPartRecord(record);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for GetConsignmentPartRecords
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ECountBLL.dll")]
        public void GetConsignmentPartRecordsTest()
        {
            ConsignmentPartBLL_Accessor target = new ConsignmentPartBLL_Accessor(); // TODO: Initialize to an appropriate value
            List<ConsignmentPartRecord> expected = null; // TODO: Initialize to an appropriate value
            List<ConsignmentPartRecord> actual;
            ConsignmentPartRecord filter = new ConsignmentPartRecord();
            actual = target.QueryRecords(filter).ToList();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetConsignmentPartRecordbykey
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ECountBLL.dll")]
        public void GetConsignmentPartRecordbykeyTest()
        {
            ConsignmentPartBLL_Accessor target = new ConsignmentPartBLL_Accessor(); // TODO: Initialize to an appropriate value
            ConsignmentPartRecord record = new ConsignmentPartRecord { RecordID = new Guid("A19BBA41-41E4-479D-BAAB-01F1482CEA36") };
            ConsignmentPartRecord expected = null; // TODO: Initialize to an appropriate value
            ConsignmentPartRecord actual;
            actual = target.GetConsignmentPartRecordbykey(record);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DeleteConsignmentPartRecord
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ECountBLL.dll")]
        public void DeleteConsignmentPartRecordTest()
        {
            ConsignmentPartBLL_Accessor target = new ConsignmentPartBLL_Accessor(); // TODO: Initialize to an appropriate value
            ConsignmentPartRecord record = null; // TODO: Initialize to an appropriate value
            target.DeleteConsignmentPartRecord(record);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for ConsignmentPartBLL Constructor
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ECountBLL.dll")]
        public void ConsignmentPartBLLConstructorTest()
        {
            ConsignmentPartBLL_Accessor target = new ConsignmentPartBLL_Accessor();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for UpdateConsignmentPartRecord
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ECountBLL.dll")]
        public void UpdateConsignmentPartRecordTest1()
        {
            ConsignmentPartBLL_Accessor target = new ConsignmentPartBLL_Accessor(); // TODO: Initialize to an appropriate value
            ConsignmentPartRecord record = null; // TODO: Initialize to an appropriate value
            target.UpdateConsignmentPartRecord(record);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

    }
}
