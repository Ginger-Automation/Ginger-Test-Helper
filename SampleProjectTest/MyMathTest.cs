using GingerTestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleProject;
using System.Collections.Generic;

namespace SampleProjectTest
{
    [TestClass]
    public class MyMathTest
    {
        public TestContext TestContext { get; set; }

        [ClassInitialize]
        public static void ClassInitialize(TestContext TestContext)
        {

        }


        [ClassCleanup]
        public static void ClassCleanup()
        {
            
        }

        [TestInitialize]
        public void TestInitialize()
        {
            // write 


            string test = TestContext.TestName;
            TestContext.Properties.Add("koko", 123);

        }

        [TestCleanup]
        public void TestCleanup()
        {
            string test = TestContext.TestName;
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
    }
}
