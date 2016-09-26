using SGM.ECount.BLL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SGM.ECount.DataModel;

namespace BLLTest
{
    
    
    /// <summary>
    ///This is a test class for PartSegmentBLLTest and is intended
    ///to contain all PartSegmentBLLTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PartSegmentBLLTest
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
        ///A test for DeleteRelationByPart
        ///</summary>
        [TestMethod()]
        public void DeleteRelationByPartTest()
        {
            PartSegmentBLL target = new PartSegmentBLL(); // TODO: Initialize to an appropriate value
            Part part = null; // TODO: Initialize to an appropriate value
            ECountContext outerContext = null; // TODO: Initialize to an appropriate value
            bool saveChanges = false; // TODO: Initialize to an appropriate value
            target.DeleteRelationByPart(part, saveChanges);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for AddRelation
        ///</summary>
        [TestMethod()]
        public void AddRelationTest()
        {
            PartSegmentBLL target = new PartSegmentBLL(); // TODO: Initialize to an appropriate value
            PartSegment relation = null; // TODO: Initialize to an appropriate value
            ECountContext outerContext = null; // TODO: Initialize to an appropriate value
            bool saveChanges = false; // TODO: Initialize to an appropriate value
            target.AddRelation(relation, saveChanges);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for PartSegmentBLL Constructor
        ///</summary>
        [TestMethod()]
        public void PartSegmentBLLConstructorTest()
        {
            PartSegmentBLL target = new PartSegmentBLL();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for DeleteRelationByPart
        ///</summary>
        [TestMethod()]
        public void DeleteRelationByPartTest1()
        {
            PartSegmentBLL target = new PartSegmentBLL(); // TODO: Initialize to an appropriate value
            Part part = null; // TODO: Initialize to an appropriate value
            ECountContext outerContext = null; // TODO: Initialize to an appropriate value
            bool saveChanges = false; // TODO: Initialize to an appropriate value
            target.DeleteRelationByPart(part, saveChanges);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for AddRelation
        ///</summary>
        [TestMethod()]
        public void AddRelationTest1()
        {
            PartSegmentBLL target = new PartSegmentBLL(); // TODO: Initialize to an appropriate value
            PartSegment relation = null; // TODO: Initialize to an appropriate value
            ECountContext outerContext = null; // TODO: Initialize to an appropriate value
            bool saveChanges = false; // TODO: Initialize to an appropriate value
            target.AddRelation(relation,  saveChanges);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for PartSegmentBLL Constructor
        ///</summary>
        [TestMethod()]
        public void PartSegmentBLLConstructorTest1()
        {
            PartSegmentBLL target = new PartSegmentBLL();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
