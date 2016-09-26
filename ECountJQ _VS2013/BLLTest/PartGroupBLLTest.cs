using SGM.ECount.BLL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SGM.ECount.DataModel;
using System.Collections.Generic;

namespace BLLTest
{
    
    
    /// <summary>
    ///This is a test class for PartGroupBLLTest and is intended
    ///to contain all PartGroupBLLTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PartGroupBLLTest
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
        ///A test for PartGroupBLL Constructor
        ///</summary>
        [TestMethod()]
        public void PartGroupBLLConstructorTest()
        {
            PartGroupBLL target = new PartGroupBLL();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        [TestMethod()]
        public void GetPartGroupByKeyTest()
        {
            PartGroupBLL target = new PartGroupBLL();
            PartGroup tmpGroup = target.GetPartGroupByKey(new PartGroup { GroupID = 1 });
            Assert.IsNotNull(tmpGroup);
        }

        [TestMethod()]
        public void GetPartGroupsTest()
        {
            PartGroupBLL target = new PartGroupBLL();
            List<PartGroup> groups = target.GetPartGroups();
            Assert.IsNotNull(groups);
            Assert.AreNotEqual(0,groups.Count);
        }

        [TestMethod()]
        public void GetPartGroupsByPageTest()
        {
            PartGroupBLL target = new PartGroupBLL();
            int pageCount;
            List<PartGroup> groups = target.GetPartGroupsByPage(2,2,out pageCount);
            Assert.IsNotNull(groups);
            Assert.AreNotEqual(0, groups.Count);
            Assert.AreNotEqual(0, pageCount);
        }


        [TestMethod()]
        public void AddPartGroupTest()
        {
            PartGroupBLL target = new PartGroupBLL();
            PartGroup group = new PartGroup { GroupName = "test group2" };
            group.Part.Add(new Part { PartID = new System.Guid("BAF2361E-CB23-4515-96A3-000BFA2D7D56") });
            group.Part.Add(new Part { PartID = new System.Guid("6D95D410-994C-4DB2-803D-0012CBB90BD9") });
            group.Part.Add(new Part { PartID = new System.Guid("564D2794-0E93-47C1-9682-0018CAA85F2F") });
            group.Part.Add(new Part { PartID = new System.Guid("7B1BE85D-9B95-4BC1-8BC5-0020AE58D2EE") });
            group.Part.Add(new Part { PartID = new System.Guid("44997C30-0C2C-46C4-A172-00251018EC85") });
            group.Part.Add(new Part { PartID = new System.Guid("6E29BACB-858B-46C5-8596-003786F21FBB") });
            target.AddPartGroup(group);
            Assert.AreNotEqual(0,group.GroupID);
        }


        [TestMethod()]
        public void UpdatePartGroup()
        {
            PartGroupBLL target = new PartGroupBLL();
            PartGroup group = new PartGroup { GroupID=48, GroupName = "test group048", Description="desc" };
            //group.Part.Add(new Part { PartID = new System.Guid("BAF2361E-CB23-4515-96A3-000BFA2D7D56") });
            group.Part.Add(new Part { PartID = new System.Guid("6D95D410-994C-4DB2-803D-0012CBB90BD9") });
            group.Part.Add(new Part { PartID = new System.Guid("564D2794-0E93-47C1-9682-0018CAA85F2F") });
            //group.Part.Add(new Part { PartID = new System.Guid("7B1BE85D-9B95-4BC1-8BC5-0020AE58D2EE") });
            //group.Part.Add(new Part { PartID = new System.Guid("44997C30-0C2C-46C4-A172-00251018EC85") });
            //group.Part.Add(new Part { PartID = new System.Guid("6E29BACB-858B-46C5-8596-003786F21FBB") });
            group.Part.Add(new Part { PartID = new System.Guid("D232CEC7-7133-4905-9E5C-004128F6AC40") });
            target.UpdatePartGroup(group);


            PartGroupBLL target1 = new PartGroupBLL();
            PartGroup group1 = new PartGroup { GroupID = 48, GroupName = "test group 0048", Description = "desc" };
            group.Part.Add(new Part { PartID = new System.Guid("BAF2361E-CB23-4515-96A3-000BFA2D7D56") });
            //group.Part.Add(new Part { PartID = new System.Guid("6D95D410-994C-4DB2-803D-0012CBB90BD9") });
            group.Part.Add(new Part { PartID = new System.Guid("564D2794-0E93-47C1-9682-0018CAA85F2F") });
            //group.Part.Add(new Part { PartID = new System.Guid("7B1BE85D-9B95-4BC1-8BC5-0020AE58D2EE") });
            //group.Part.Add(new Part { PartID = new System.Guid("44997C30-0C2C-46C4-A172-00251018EC85") });
            //group.Part.Add(new Part { PartID = new System.Guid("6E29BACB-858B-46C5-8596-003786F21FBB") });
            group1.Part.Add(new Part { PartID = new System.Guid("D232CEC7-7133-4905-9E5C-004128F6AC40") });
            target1.UpdatePartGroup(group1);

            group = target.GetPartGroupByKey(group);
            Assert.AreEqual("test group 048", group.GroupName);
        }

        [TestMethod()]
        public void DeletePartGroup()
        {
            PartGroupBLL target = new PartGroupBLL();
            PartGroup group = new PartGroup { GroupID = 30 };
            target.DeletePartGroup(group);
            group = target.GetPartGroupByKey(group);
            Assert.AreEqual(null, group);
        }
    }
}
