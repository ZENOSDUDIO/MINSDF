using SGM.Ecount.BLL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SGM.ECount.DataModel;
using System.Collections.Generic;

namespace BLLTest
{
    
    
    /// <summary>
    ///This is a test class for UserGroupBLLTest and is intended
    ///to contain all UserGroupBLLTest Unit Tests
    ///</summary>
    [TestClass()]
    public class UserGroupBLLTest
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
        ///A test for UpdateUserGroup
        ///</summary>
        [TestMethod()]
        public void UpdateUserGroupTest()
        {
            UserGroupBLL target = new UserGroupBLL(); // TODO: Initialize to an appropriate value
            UserGroup userGroup = null; // TODO: Initialize to an appropriate value
            target.UpdateUserGroup(userGroup);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for IsUserInRole
        ///</summary>
        [TestMethod()]
        public void IsUserInRoleTest()
        {
            string userName = string.Empty; // TODO: Initialize to an appropriate value
            string roleName = string.Empty; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = UserGroupBLL.IsUserInRole(userName, roleName);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetUserGroupsbyUser
        ///</summary>
        [TestMethod()]
        public void GetUserGroupsbyUserTest()
        {
            UserGroupBLL target = new UserGroupBLL(); // TODO: Initialize to an appropriate value
            string userName = string.Empty; // TODO: Initialize to an appropriate value
            string[] expected = null; // TODO: Initialize to an appropriate value
            string[] actual;
            actual = target.GetUserGroupsbyUser(userName);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetUserGroups
        ///</summary>
        [TestMethod()]
        public void GetUserGroupsTest()
        {
            UserGroupBLL target = new UserGroupBLL(); // TODO: Initialize to an appropriate value
            List<UserGroup> expected = null; // TODO: Initialize to an appropriate value
            List<UserGroup> actual;
            actual = target.GetUserGroups();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DeleteUserGroup
        ///</summary>
        [TestMethod()]
        public void DeleteUserGroupTest()
        {
            UserGroupBLL target = new UserGroupBLL(); // TODO: Initialize to an appropriate value
            UserGroup userGroup = null; // TODO: Initialize to an appropriate value
            target.DeleteUserGroup(userGroup);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for AddUserGroup
        ///</summary>
        [TestMethod()]
        public void AddUserGroupTest()
        {
            UserGroupBLL target = new UserGroupBLL(); // TODO: Initialize to an appropriate value
            UserGroup userGroup = null; // TODO: Initialize to an appropriate value
            target.AddUserGroup(userGroup);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for UserGroupBLL Constructor
        ///</summary>
        [TestMethod()]
        public void UserGroupBLLConstructorTest()
        {
            UserGroupBLL target = new UserGroupBLL();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for UpdateUserGroup
        ///</summary>
        [TestMethod()]
        public void UpdateUserGroupTest1()
        {
            UserGroupBLL target = new UserGroupBLL(); // TODO: Initialize to an appropriate value
            UserGroup userGroup = null; // TODO: Initialize to an appropriate value
            target.UpdateUserGroup(userGroup);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for IsUserInRole
        ///</summary>
        [TestMethod()]
        public void IsUserInRoleTest1()
        {
            string userName = string.Empty; // TODO: Initialize to an appropriate value
            string roleName = string.Empty; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = UserGroupBLL.IsUserInRole(userName, roleName);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetUserGroupsbyUser
        ///</summary>
        [TestMethod()]
        public void GetUserGroupsbyUserTest1()
        {
            UserGroupBLL target = new UserGroupBLL(); // TODO: Initialize to an appropriate value
            string userName = string.Empty; // TODO: Initialize to an appropriate value
            string[] expected = null; // TODO: Initialize to an appropriate value
            string[] actual;
            actual = target.GetUserGroupsbyUser(userName);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetUserGroups
        ///</summary>
        [TestMethod()]
        public void GetUserGroupsTest1()
        {
            UserGroupBLL target = new UserGroupBLL(); // TODO: Initialize to an appropriate value
            List<UserGroup> expected = null; // TODO: Initialize to an appropriate value
            List<UserGroup> actual;
            actual = target.GetUserGroups();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DeleteUserGroup
        ///</summary>
        [TestMethod()]
        public void DeleteUserGroupTest1()
        {
            UserGroupBLL target = new UserGroupBLL(); // TODO: Initialize to an appropriate value
            UserGroup userGroup = null; // TODO: Initialize to an appropriate value
            target.DeleteUserGroup(userGroup);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for AddUserGroup
        ///</summary>
        [TestMethod()]
        public void AddUserGroupTest1()
        {
            UserGroupBLL target = new UserGroupBLL(); // TODO: Initialize to an appropriate value
            UserGroup userGroup = null; // TODO: Initialize to an appropriate value
            target.AddUserGroup(userGroup);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for UserGroupBLL Constructor
        ///</summary>
        [TestMethod()]
        public void UserGroupBLLConstructorTest1()
        {
            UserGroupBLL target = new UserGroupBLL();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
