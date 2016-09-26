using SGM.Ecount.BLL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Objects.DataClasses;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;

namespace BLLTest
{
    
    
    /// <summary>
    ///This is a test class for BaseGenericBLLTest and is intended
    ///to contain all BaseGenericBLLTest Unit Tests
    ///</summary>
    [TestClass()]
    public class BaseGenericBLLTest
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
        ///A test for UpdateObject
        ///</summary>
        public void UpdateObjectTestHelper<T>()
            where T : EntityObject
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            BaseGenericBLL_Accessor<T> target = new BaseGenericBLL_Accessor<T>(param0); // TODO: Initialize to an appropriate value
            T entity = default(T); // TODO: Initialize to an appropriate value
            bool saveChanges = false; // TODO: Initialize to an appropriate value
            target.UpdateObject(entity, saveChanges);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        [TestMethod()]
        [DeploymentItem("ECountBLL.dll")]
        public void UpdateObjectTest()
        {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call UpdateObjectTestHelper<T>() with appropriate type parameters.");
        }

        /// <summary>
        ///A test for GetQueryByPage
        ///</summary>
        public void GetQueryByPageTestHelper<T, E>()
            where T : EntityObject
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            BaseGenericBLL_Accessor<T> target = new BaseGenericBLL_Accessor<T>(param0); // TODO: Initialize to an appropriate value
            IQueryable<E> query = null; // TODO: Initialize to an appropriate value
            int pageSize = 0; // TODO: Initialize to an appropriate value
            int pageNumber = 0; // TODO: Initialize to an appropriate value
            int pageCount = 0; // TODO: Initialize to an appropriate value
            int pageCountExpected = 0; // TODO: Initialize to an appropriate value
            IQueryable<E> expected = null; // TODO: Initialize to an appropriate value
            IQueryable<E> actual;
            actual = target.GetQueryByPage<E>(query, pageSize, pageNumber, out pageCount);
            Assert.AreEqual(pageCountExpected, pageCount);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        [DeploymentItem("ECountBLL.dll")]
        public void GetQueryByPageTest()
        {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call GetQueryByPageTestHelper<T, E>() with appropriate type parameters.");
        }

        /// <summary>
        ///A test for GetObjects
        ///</summary>
        public void GetObjectsTestHelper<T>()
            where T : EntityObject
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            BaseGenericBLL_Accessor<T> target = new BaseGenericBLL_Accessor<T>(param0); // TODO: Initialize to an appropriate value
            ObjectQuery<T> expected = null; // TODO: Initialize to an appropriate value
            ObjectQuery<T> actual;
            actual = target.GetObjects();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        [DeploymentItem("ECountBLL.dll")]
        public void GetObjectsTest()
        {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call GetObjectsTestHelper<T>() with appropriate type parameters.");
        }

        /// <summary>
        ///A test for GetObjectByKey
        ///</summary>
        public void GetObjectByKeyTest1Helper<T>()
            where T : EntityObject
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            BaseGenericBLL_Accessor<T> target = new BaseGenericBLL_Accessor<T>(param0); // TODO: Initialize to an appropriate value
            T entity = default(T); // TODO: Initialize to an appropriate value
            T expected = default(T); // TODO: Initialize to an appropriate value
            T actual;
            actual = target.GetObjectByKey(entity);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        [DeploymentItem("ECountBLL.dll")]
        public void GetObjectByKeyTest1()
        {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call GetObjectByKeyTest1Helper<T>() with appropriate type parameters.");
        }

        /// <summary>
        ///A test for GetObjectByKey
        ///</summary>
        public void GetObjectByKeyTestHelper<T>()
            where T : EntityObject
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            BaseGenericBLL_Accessor<T> target = new BaseGenericBLL_Accessor<T>(param0); // TODO: Initialize to an appropriate value
            T entity = default(T); // TODO: Initialize to an appropriate value
            bool lazyLoad = false; // TODO: Initialize to an appropriate value
            T expected = default(T); // TODO: Initialize to an appropriate value
            T actual;
            actual = target.GetObjectByKey(entity, lazyLoad);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        [DeploymentItem("ECountBLL.dll")]
        public void GetObjectByKeyTest()
        {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call GetObjectByKeyTestHelper<T>() with appropriate type parameters.");
        }

        /// <summary>
        ///A test for GetList
        ///</summary>
        public void GetListTestHelper<T>()
            where T : EntityObject
        {
            string entitySetName = string.Empty; // TODO: Initialize to an appropriate value
            BaseGenericBLL<T> target = new BaseGenericBLL<T>(entitySetName); // TODO: Initialize to an appropriate value
            List<T> expected = null; // TODO: Initialize to an appropriate value
            List<T> actual;
            actual = target.GetList();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void GetListTest()
        {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call GetListTestHelper<T>() with appropriate type parameters.");
        }

        /// <summary>
        ///A test for DeleteObject
        ///</summary>
        public void DeleteObjectTestHelper<T>()
            where T : EntityObject
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            BaseGenericBLL_Accessor<T> target = new BaseGenericBLL_Accessor<T>(param0); // TODO: Initialize to an appropriate value
            T entity = default(T); // TODO: Initialize to an appropriate value
            bool saveChanges = false; // TODO: Initialize to an appropriate value
            target.DeleteObject(entity, saveChanges);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        [TestMethod()]
        [DeploymentItem("ECountBLL.dll")]
        public void DeleteObjectTest()
        {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call DeleteObjectTestHelper<T>() with appropriate type parameters.");
        }

        /// <summary>
        ///A test for AddObject
        ///</summary>
        public void AddObjectTestHelper<T>()
            where T : EntityObject
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            BaseGenericBLL_Accessor<T> target = new BaseGenericBLL_Accessor<T>(param0); // TODO: Initialize to an appropriate value
            T entity = default(T); // TODO: Initialize to an appropriate value
            bool saveChanges = false; // TODO: Initialize to an appropriate value
            target.AddObject(entity, saveChanges);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        [TestMethod()]
        [DeploymentItem("ECountBLL.dll")]
        public void AddObjectTest()
        {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call AddObjectTestHelper<T>() with appropriate type parameters.");
        }

        /// <summary>
        ///A test for BaseGenericBLL`1 Constructor
        ///</summary>
        public void BaseGenericBLLConstructorTestHelper<T>()
            where T : EntityObject
        {
            string entitySetName = string.Empty; // TODO: Initialize to an appropriate value
            BaseGenericBLL<T> target = new BaseGenericBLL<T>(entitySetName);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        [TestMethod()]
        public void BaseGenericBLLConstructorTest()
        {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call BaseGenericBLLConstructorTestHelper<T>() with appropriate type param" +
                    "eters.");
        }

        /// <summary>
        ///A test for UpdateObject
        ///</summary>
        public void UpdateObjectTest1Helper<T>()
            where T : EntityObject
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            BaseGenericBLL_Accessor<T> target = new BaseGenericBLL_Accessor<T>(param0); // TODO: Initialize to an appropriate value
            T entity = default(T); // TODO: Initialize to an appropriate value
            bool saveChanges = false; // TODO: Initialize to an appropriate value
            target.UpdateObject(entity, saveChanges);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        [TestMethod()]
        [DeploymentItem("ECountBLL.dll")]
        public void UpdateObjectTest1()
        {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call UpdateObjectTest1Helper<T>() with appropriate type parameters.");
        }

        /// <summary>
        ///A test for GetQueryByPage
        ///</summary>
        public void GetQueryByPageTest1Helper<T, E>()
            where T : EntityObject
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            BaseGenericBLL_Accessor<T> target = new BaseGenericBLL_Accessor<T>(param0); // TODO: Initialize to an appropriate value
            IQueryable<E> query = null; // TODO: Initialize to an appropriate value
            int pageSize = 0; // TODO: Initialize to an appropriate value
            int pageNumber = 0; // TODO: Initialize to an appropriate value
            int pageCount = 0; // TODO: Initialize to an appropriate value
            int pageCountExpected = 0; // TODO: Initialize to an appropriate value
            IQueryable<E> expected = null; // TODO: Initialize to an appropriate value
            IQueryable<E> actual;
            actual = target.GetQueryByPage<E>(query, pageSize, pageNumber, out pageCount);
            Assert.AreEqual(pageCountExpected, pageCount);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        [DeploymentItem("ECountBLL.dll")]
        public void GetQueryByPageTest1()
        {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call GetQueryByPageTest1Helper<T, E>() with appropriate type parameters.");
        }

        /// <summary>
        ///A test for GetObjects
        ///</summary>
        public void GetObjectsTest1Helper<T>()
            where T : EntityObject
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            BaseGenericBLL_Accessor<T> target = new BaseGenericBLL_Accessor<T>(param0); // TODO: Initialize to an appropriate value
            ObjectQuery<T> expected = null; // TODO: Initialize to an appropriate value
            ObjectQuery<T> actual;
            actual = target.GetObjects();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        [DeploymentItem("ECountBLL.dll")]
        public void GetObjectsTest1()
        {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call GetObjectsTest1Helper<T>() with appropriate type parameters.");
        }

        /// <summary>
        ///A test for GetObjectByKey
        ///</summary>
        public void GetObjectByKeyTest3Helper<T>()
            where T : EntityObject
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            BaseGenericBLL_Accessor<T> target = new BaseGenericBLL_Accessor<T>(param0); // TODO: Initialize to an appropriate value
            T entity = default(T); // TODO: Initialize to an appropriate value
            T expected = default(T); // TODO: Initialize to an appropriate value
            T actual;
            actual = target.GetObjectByKey(entity);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        [DeploymentItem("ECountBLL.dll")]
        public void GetObjectByKeyTest3()
        {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call GetObjectByKeyTest3Helper<T>() with appropriate type parameters.");
        }

        /// <summary>
        ///A test for GetObjectByKey
        ///</summary>
        public void GetObjectByKeyTest2Helper<T>()
            where T : EntityObject
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            BaseGenericBLL_Accessor<T> target = new BaseGenericBLL_Accessor<T>(param0); // TODO: Initialize to an appropriate value
            T entity = default(T); // TODO: Initialize to an appropriate value
            bool lazyLoad = false; // TODO: Initialize to an appropriate value
            T expected = default(T); // TODO: Initialize to an appropriate value
            T actual;
            actual = target.GetObjectByKey(entity, lazyLoad);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        [DeploymentItem("ECountBLL.dll")]
        public void GetObjectByKeyTest2()
        {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call GetObjectByKeyTest2Helper<T>() with appropriate type parameters.");
        }

        /// <summary>
        ///A test for GetList
        ///</summary>
        public void GetListTest1Helper<T>()
            where T : EntityObject
        {
            string entitySetName = string.Empty; // TODO: Initialize to an appropriate value
            BaseGenericBLL<T> target = new BaseGenericBLL<T>(entitySetName); // TODO: Initialize to an appropriate value
            List<T> expected = null; // TODO: Initialize to an appropriate value
            List<T> actual;
            actual = target.GetList();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void GetListTest1()
        {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call GetListTest1Helper<T>() with appropriate type parameters.");
        }

        /// <summary>
        ///A test for DeleteObject
        ///</summary>
        public void DeleteObjectTest1Helper<T>()
            where T : EntityObject
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            BaseGenericBLL_Accessor<T> target = new BaseGenericBLL_Accessor<T>(param0); // TODO: Initialize to an appropriate value
            T entity = default(T); // TODO: Initialize to an appropriate value
            bool saveChanges = false; // TODO: Initialize to an appropriate value
            target.DeleteObject(entity, saveChanges);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        [TestMethod()]
        [DeploymentItem("ECountBLL.dll")]
        public void DeleteObjectTest1()
        {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call DeleteObjectTest1Helper<T>() with appropriate type parameters.");
        }

        /// <summary>
        ///A test for AddObject
        ///</summary>
        public void AddObjectTest1Helper<T>()
            where T : EntityObject
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            BaseGenericBLL_Accessor<T> target = new BaseGenericBLL_Accessor<T>(param0); // TODO: Initialize to an appropriate value
            T entity = default(T); // TODO: Initialize to an appropriate value
            bool saveChanges = false; // TODO: Initialize to an appropriate value
            target.AddObject(entity, saveChanges);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        [TestMethod()]
        [DeploymentItem("ECountBLL.dll")]
        public void AddObjectTest1()
        {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call AddObjectTest1Helper<T>() with appropriate type parameters.");
        }

        /// <summary>
        ///A test for BaseGenericBLL`1 Constructor
        ///</summary>
        public void BaseGenericBLLConstructorTest1Helper<T>()
            where T : EntityObject
        {
            string entitySetName = string.Empty; // TODO: Initialize to an appropriate value
            BaseGenericBLL<T> target = new BaseGenericBLL<T>(entitySetName);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        [TestMethod()]
        public void BaseGenericBLLConstructorTest1()
        {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call BaseGenericBLLConstructorTest1Helper<T>() with appropriate type para" +
                    "meters.");
        }
    }
}
