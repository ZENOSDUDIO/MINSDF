using SGM.Ecount.BLL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SGM.ECount.DataModel;
using System.Collections.Generic;

namespace BLLTest
{
    
    
    /// <summary>
    ///This is a test class for UserBLLTest and is intended
    ///to contain all UserBLLTest Unit Tests
    ///</summary>
    [TestClass()]
    public class UserBLLTest
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
        ///A test for Validate
        ///</summary>
        [TestMethod()]
        public void ValidateTest()
        {
            UserBLL target = new UserBLL(); // TODO: Initialize to an appropriate value
            string userName = string.Empty; // TODO: Initialize to an appropriate value
            string password = string.Empty; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.Validate(userName, password);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for UpdateUser
        ///</summary>
        [TestMethod()]
        public void UpdateUserTest()
        {
            UserBLL target = new UserBLL(); // TODO: Initialize to an appropriate value
            User user = null; // TODO: Initialize to an appropriate value
            target.UpdateUser(user);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for GetUsers
        ///</summary>
        [TestMethod()]
        public void GetUsersTest()
        {
            UserBLL target = new UserBLL(); // TODO: Initialize to an appropriate value
            List<User> expected = null; // TODO: Initialize to an appropriate value
            List<User> actual;
            actual = target.GetUsers();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetUserInfo
        ///</summary>
        [TestMethod()]
        public void GetUserInfoTest()
        {
            UserBLL target = new UserBLL(); // TODO: Initialize to an appropriate value
            User user = null; // TODO: Initialize to an appropriate value
            User expected = null; // TODO: Initialize to an appropriate value
            User actual;
            actual = target.GetUserInfo(user);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DeleteUer
        ///</summary>
        [TestMethod()]
        public void DeleteUerTest()
        {
            UserBLL target = new UserBLL(); // TODO: Initialize to an appropriate value
            User user = null; // TODO: Initialize to an appropriate value
            target.DeleteUer(user);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for AddUser
        ///</summary>
        [TestMethod()]
        public void AddUserTest()
        {
            UserBLL target = new UserBLL(); // TODO: Initialize to an appropriate value
            User user = null; // TODO: Initialize to an appropriate value
            target.AddUser(user);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for AddToGroup
        ///</summary>
        [TestMethod()]
        public void AddToGroupTest()
        {
            UserBLL target = new UserBLL(); // TODO: Initialize to an appropriate value
            UserGroup userGroup = null; // TODO: Initialize to an appropriate value
            User user = null; // TODO: Initialize to an appropriate value
            target.AddToGroup(userGroup, user);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for UserBLL Constructor
        ///</summary>
        [TestMethod()]
        public void UserBLLConstructorTest()
        {
            UserBLL target = new UserBLL();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for Validate
        ///</summary>
        [TestMethod()]
        public void ValidateTest1()
        {
            UserBLL target = new UserBLL(); // TODO: Initialize to an appropriate value
            string userName = string.Empty; // TODO: Initialize to an appropriate value
            string password = string.Empty; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.Validate(userName, password);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for UpdateUser
        ///</summary>
        [TestMethod()]
        public void UpdateUserTest1()
        {
            UserBLL target = new UserBLL(); // TODO: Initialize to an appropriate value
            User user = null; // TODO: Initialize to an appropriate value
            target.UpdateUser(user);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for GetUsers
        ///</summary>
        [TestMethod()]
        public void GetUsersTest1()
        {
            UserBLL target = new UserBLL(); // TODO: Initialize to an appropriate value
            List<User> expected = null; // TODO: Initialize to an appropriate value
            List<User> actual;
            actual = target.GetUsers();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetUserInfo
        ///</summary>
        [TestMethod()]
        public void GetUserInfoTest1()
        {
            UserBLL target = new UserBLL(); // TODO: Initialize to an appropriate value
            User user = null; // TODO: Initialize to an appropriate value
            User expected = null; // TODO: Initialize to an appropriate value
            User actual;
            actual = target.GetUserInfo(user);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DeleteUer
        ///</summary>
        [TestMethod()]
        public void DeleteUerTest1()
        {
            UserBLL target = new UserBLL(); // TODO: Initialize to an appropriate value
            User user = null; // TODO: Initialize to an appropriate value
            target.DeleteUer(user);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for AddUser
        ///</summary>
        [TestMethod()]
        public void AddUserTest1()
        {
            UserBLL target = new UserBLL(); // TODO: Initialize to an appropriate value
            User user = null; // TODO: Initialize to an appropriate value
            target.AddUser(user);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for AddToGroup
        ///</summary>
        [TestMethod()]
        public void AddToGroupTest1()
        {
            UserBLL target = new UserBLL(); // TODO: Initialize to an appropriate value
            UserGroup userGroup = null; // TODO: Initialize to an appropriate value
            User user = null; // TODO: Initialize to an appropriate value
            target.AddToGroup(userGroup, user);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for UserBLL Constructor
        ///</summary>
        [TestMethod()]
        public void UserBLLConstructorTest1()
        {
            UserBLL target = new UserBLL();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
