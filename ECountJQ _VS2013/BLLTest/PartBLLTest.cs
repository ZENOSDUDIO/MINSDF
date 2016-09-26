using SGM.Ecount.BLL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SGM.ECount.DataModel;
using System.Collections.Generic;
using System.Linq;
using System;

namespace BLLTest
{


    /// <summary>
    ///This is a test class for PartBLLTest and is intended
    ///to contain all PartBLLTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PartBLLTest
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
        ///A test for UpdatePart
        ///</summary>
        [TestMethod()]
        public void UpdatePartTest()
        {
 // TODO: Initialize to an appropriate value
            Part part = CreatePart();// new Part { PartID = new System.Guid("365A5D23-292C-42FF-BCC6-6BAF2F8EBA80") };
            part.PartID = new System.Guid("51B384C7-EEAF-4B06-8607-B817A32C6284");
            //part.PartSegments.Clear();
            part.PartSegments.Add(new PartSegment { Part = part, Segment = new Segment { SegmentID = 5 } });
            //Part part = target.GetPartbyKey(p);// TODO: Initialize to an appropriate value
            part.PartChineseName = "测试零件 01";
            part.Plant.PlantID = 5;
            PartBLL target = new PartBLL();
            //part.EntityKey = target.Context.CreateEntityKey("Part", part);
            target.UpdatePart(part);
           
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for QueryParts
        ///</summary>
        [TestMethod()]
        public void QueryPartsTest()
        {
            PartBLL target = new PartBLL(); // TODO: Initialize to an appropriate value
            Part part = null; // TODO: Initialize to an appropriate value
            List<ViewPart> expected = null; // TODO: Initialize to an appropriate value
            List<ViewPart> actual;
            actual = target.QueryParts(part).ToList();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetRelatedParts
        ///</summary>
        [TestMethod()]
        public void GetRelatedPartsTest()
        {
            PartBLL target = new PartBLL(); // TODO: Initialize to an appropriate value
            Part part = null; // TODO: Initialize to an appropriate value
            List<Part> expected = null; // TODO: Initialize to an appropriate value
            List<Part> actual;
            actual = target.GetRelatedParts(part);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetPartsbyUser
        ///</summary>
        [TestMethod()]
        public void GetPartsbyUserTest()
        {
            PartBLL target = new PartBLL(); // TODO: Initialize to an appropriate value
            User user = null; // TODO: Initialize to an appropriate value
            List<Part> expected = null; // TODO: Initialize to an appropriate value
            List<Part> actual;
            actual = target.GetPartsbyUser(user);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetParts
        ///</summary>
        [TestMethod()]
        public void GetPartsTest()
        {
            PartBLL target = new PartBLL(); // TODO: Initialize to an appropriate value
            List<Part> expected = null; // TODO: Initialize to an appropriate value
            List<Part> actual;
            actual = target.GetParts();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetPartbyKey
        ///</summary>
        [TestMethod()]
        public void GetPartbyKeyTest()
        {
            PartBLL target = new PartBLL(); // TODO: Initialize to an appropriate value
            Part part = new Part { PartID = new Guid("8625D5D6-D1FB-4494-9D90-82284DC5AE55") };
            Part expected = null; // TODO: Initialize to an appropriate value
            Part actual;
            actual = target.GetPartbyKey(part);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DeletePart
        ///</summary>
        [TestMethod()]
        public void DeletePartTest()
        {
            PartBLL target = new PartBLL(); // TODO: Initialize to an appropriate value
            Part part = null; // TODO: Initialize to an appropriate value
            target.DeletePart(part);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for AddPart
        ///</summary>
        [TestMethod()]
        public void AddPartTest()
        {
            PartBLL target = new PartBLL(); // TODO: Initialize to an appropriate value
            Part part = CreatePart();

            //Part expected = null; // TODO: Initialize to an appropriate value
            
            Part actual;
            using (target.Context)
            {
                actual = target.AddPart(part);
            }
            Assert.IsNotNull(actual.PartID);//.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        private static Part CreatePart()
        {
            Part part = new Part
            {
                PartCode = "001",
                Available = true,
                CycleCountLevel = new CycleCountLevel { LevelID = 1 },
                CycleCountTimes = 0,
                PartCategory = new PartCategory { CategoryID = 1 },
                PartChineseName = "测试零件",
                PartEnglishName = "test part",
                PartGroup = new PartGroup { GroupID = 1 },
                PartStatus = new PartStatus { StatusID = 6 },
                Plant = new Plant { PlantID = 6 },
                Supplier = new Supplier { SupplierID = 2 },
                WorkLocation = "test location"
            };
            PartSegment ps = new PartSegment
            {
                Segment = new Segment
                {
                    SegmentID = 2
                    //,
                    //SegmentCode = "Chassis",
                    //SegmentName = "总装底盘工段",
                    //Available = true
                },
                Part = part
            };
            PartSegment ps1 = new PartSegment
            {
                Segment = new Segment
                {
                    SegmentID = 3//,
                    //SegmentCode = "T/C",
                    //SegmentName = "总装内饰/底盘工段",
                    //Available = true
                },
                Part = part
            };
            part.PartSegments.Add(ps);
            part.PartSegments.Add(ps1);
            return part;
        }

        /// <summary>
        ///A test for PartBLL Constructor
        ///</summary>
        [TestMethod()]
        public void PartBLLConstructorTest()
        {
            PartBLL target = new PartBLL();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }


        /// <summary>
        ///A test for GetPartsbyUser
        ///</summary>
        [TestMethod()]
        public void GetPartsbyUserTest1()
        {
            PartBLL target = new PartBLL(); // TODO: Initialize to an appropriate value
            User user = null; // TODO: Initialize to an appropriate value
            List<Part> expected = null; // TODO: Initialize to an appropriate value
            List<Part> actual;
            actual = target.GetPartsbyUser(user);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetParts
        ///</summary>
        [TestMethod()]
        public void GetPartsTest1()
        {
            PartBLL target = new PartBLL(); // TODO: Initialize to an appropriate value
            List<Part> expected = null; // TODO: Initialize to an appropriate value
            List<Part> actual;
            actual = target.GetParts();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetPartbyKey
        ///</summary>
        [TestMethod()]
        public void GetPartbyKeyTest1()
        {
            PartBLL target = new PartBLL(); // TODO: Initialize to an appropriate value
            Part part = null; // TODO: Initialize to an appropriate value
            Part expected = null; // TODO: Initialize to an appropriate value
            Part actual;
            actual = target.GetPartbyKey(part);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DeletePart
        ///</summary>
        [TestMethod()]
        public void DeletePartTest1()
        {
            PartBLL target = new PartBLL(); // TODO: Initialize to an appropriate value
            Part part = null; // TODO: Initialize to an appropriate value
            target.DeletePart(part);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }



        /// <summary>
        ///A test for PartBLL Constructor
        ///</summary>
        [TestMethod()]
        public void PartBLLConstructorTest1()
        {
            PartBLL target = new PartBLL();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
