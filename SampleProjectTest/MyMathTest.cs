using GingerTestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleProject;
using System.Collections.Generic;

namespace SampleProjectTest
{
    [TestClass]
    public class MyMathTest 
    {        
        static TestHelper mTestHelper;
        
        [ClassInitialize]
        public static void ClassInitialize(TestContext TestContext)
        {
            mTestHelper = new TestHelper(TestContext);
        }


        [ClassCleanup]
        public static void ClassCleanup()
        {
            mTestHelper.ClassCleanup();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            mTestHelper.TestInitialize();
            
        }

        [TestCleanup]
        public void TestCleanup()
        {
            mTestHelper.TestCleanup();
        }

        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            string fileName = TestResources.GetTestResourcesFile("numbers.txt");
            string[] list = System.IO.File.ReadAllLines(fileName);
            List<int> numbers = new List<int>();
            foreach(string line in list)
            {
                numbers.Add(int.Parse(line));
            }

            //Act
            int total = MyMath.Sum(numbers);

            //Assert           
            Assert.AreEqual(4, numbers.Count, "numbers count is 4");
            Assert.AreEqual(20, total, "total=20");
            
        }
        [TestMethod]
        public void TestMethod2()
        {
            // Arrange
            string fileName = mTestHelper.GetTestResourcesFile("numbers.txt");
            string[] list = System.IO.File.ReadAllLines(fileName);
            List<int> numbers = new List<int>();
            foreach (string line in list)
            {
                mTestHelper.Log("line = " + line);
                numbers.Add(int.Parse(line));
            }

            //Act
            int total = MyMath.Sum(numbers);

            //Assert           
            Assert.AreEqual(4, numbers.Count, "numbers count is 4");
            Assert.AreEqual(20, total, "total=20");
            
            mTestHelper.CreateTestArtifact("file1.txt", "fffffffffffffffffffff");
            mTestHelper.AddTestArtifact(fileName);
        }


    }
}
