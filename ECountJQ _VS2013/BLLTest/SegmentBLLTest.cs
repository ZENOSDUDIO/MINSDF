using SGM.Ecount.BLL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SGM.ECount.DataModel;
using System.Collections.Generic;

namespace BLLTest
{
    
    
    /// <summary>
    ///This is a test class for SegmentBLLTest and is intended
    ///to contain all SegmentBLLTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SegmentBLLTest
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
        ///A test for GetSegments
        ///</summary>
        [TestMethod()]
        public void GetSegmentsTest()
        {
            SegmentBLL target = new SegmentBLL(); // TODO: Initialize to an appropriate value
            List<Segment> expected = null; // TODO: Initialize to an appropriate value
            List<Segment> actual;
            actual = target.GetSegments();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetSegmentbyWorkshopID
        ///</summary>
        [TestMethod()]
        public void GetSegmentbyWorkshopIDTest()
        {
            SegmentBLL target = new SegmentBLL(); // TODO: Initialize to an appropriate value
            int workshopID = 0; // TODO: Initialize to an appropriate value
            List<Segment> expected = null; // TODO: Initialize to an appropriate value
            List<Segment> actual;
            actual = target.GetSegmentbyWorkshopID(workshopID);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetSegmentbyWorkshop
        ///</summary>
        [TestMethod()]
        public void GetSegmentbyWorkshopTest()
        {
            SegmentBLL target = new SegmentBLL(); // TODO: Initialize to an appropriate value
            Workshop workshop = null; // TODO: Initialize to an appropriate value
            List<Segment> expected = null; // TODO: Initialize to an appropriate value
            List<Segment> actual;
            actual = target.GetSegmentbyWorkshop(workshop);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetSegmentbyPlantID
        ///</summary>
        [TestMethod()]
        public void GetSegmentbyPlantIDTest()
        {
            SegmentBLL target = new SegmentBLL(); // TODO: Initialize to an appropriate value
            int plantID = 0; // TODO: Initialize to an appropriate value
            List<ViewPlantWorkshopSegment> expected = null; // TODO: Initialize to an appropriate value
            List<ViewPlantWorkshopSegment> actual;
            actual = target.GetSegmentbyPlantID(plantID);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetPartSegmentByPartID
        ///</summary>
        [TestMethod()]
        public void GetPartSegmentByPartIDTest()
        {
            SegmentBLL target = new SegmentBLL(); // TODO: Initialize to an appropriate value
            Guid partID = new Guid(); // TODO: Initialize to an appropriate value
            List<PartSegment> expected = null; // TODO: Initialize to an appropriate value
            List<PartSegment> actual;
            actual = target.GetPartSegmentByPartID(partID);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for SegmentBLL Constructor
        ///</summary>
        [TestMethod()]
        public void SegmentBLLConstructorTest()
        {
            SegmentBLL target = new SegmentBLL();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for GetSegments
        ///</summary>
        [TestMethod()]
        public void GetSegmentsTest1()
        {
            SegmentBLL target = new SegmentBLL(); // TODO: Initialize to an appropriate value
            List<Segment> expected = null; // TODO: Initialize to an appropriate value
            List<Segment> actual;
            actual = target.GetSegments();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetSegmentbyWorkshopID
        ///</summary>
        [TestMethod()]
        public void GetSegmentbyWorkshopIDTest1()
        {
            SegmentBLL target = new SegmentBLL(); // TODO: Initialize to an appropriate value
            int workshopID = 0; // TODO: Initialize to an appropriate value
            List<Segment> expected = null; // TODO: Initialize to an appropriate value
            List<Segment> actual;
            actual = target.GetSegmentbyWorkshopID(workshopID);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetSegmentbyWorkshop
        ///</summary>
        [TestMethod()]
        public void GetSegmentbyWorkshopTest1()
        {
            SegmentBLL target = new SegmentBLL(); // TODO: Initialize to an appropriate value
            Workshop workshop = null; // TODO: Initialize to an appropriate value
            List<Segment> expected = null; // TODO: Initialize to an appropriate value
            List<Segment> actual;
            actual = target.GetSegmentbyWorkshop(workshop);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetSegmentbyPlantID
        ///</summary>
        [TestMethod()]
        public void GetSegmentbyPlantIDTest1()
        {
            SegmentBLL target = new SegmentBLL(); // TODO: Initialize to an appropriate value
            int plantID = 0; // TODO: Initialize to an appropriate value
            List<ViewPlantWorkshopSegment> expected = null; // TODO: Initialize to an appropriate value
            List<ViewPlantWorkshopSegment> actual;
            actual = target.GetSegmentbyPlantID(plantID);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetPartSegmentByPartID
        ///</summary>
        [TestMethod()]
        public void GetPartSegmentByPartIDTest1()
        {
            SegmentBLL target = new SegmentBLL(); // TODO: Initialize to an appropriate value
            Guid partID = new Guid(); // TODO: Initialize to an appropriate value
            List<PartSegment> expected = null; // TODO: Initialize to an appropriate value
            List<PartSegment> actual;
            actual = target.GetPartSegmentByPartID(partID);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for SegmentBLL Constructor
        ///</summary>
        [TestMethod()]
        public void SegmentBLLConstructorTest1()
        {
            SegmentBLL target = new SegmentBLL();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
