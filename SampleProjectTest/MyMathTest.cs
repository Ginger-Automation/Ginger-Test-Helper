using GingerTestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleProject;
using System;
using System.Collections.Generic;
using System.IO;

namespace SampleProjectTest
{
    [TestClass]
    public class MyMathTest 
    {
        static TestHelper mTestHelper = new TestHelper();
        public TestContext TestContext { get; set; }

        [ClassInitialize]
        public static void ClassInitialize(TestContext TestContext)
        {            
            mTestHelper.ClassInitialize(TestContext);
        }


        [ClassCleanup]
        public static void ClassCleanup()
        {
            mTestHelper.ClassCleanup();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            mTestHelper.TestInitialize(TestContext);
            
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
        
        [TestMethod]
        public void CreateTempTestFile()
        {
            // Arrange
            string fileName = mTestHelper.GetTempFileName("1.txt");
            string txt = "10,4,6,7,2,3";
            System.IO.File.WriteAllText(fileName, txt);

            //Act
            int total = MyMath.Sum(fileName);
            long fileSize = new FileInfo(fileName).Length;

            //Assert                       
            Assert.AreEqual(32, total, "total");
            Assert.AreEqual(12, fileSize, "fileSize");

            mTestHelper.AddTestArtifact(fileName);
        }


        [TestMethod]
        public void AppenTempTestFile()
        {
            // Arrange
            string fileName = mTestHelper.GetTempFileName("2.txt");


            //Act
            for (int i=0;i<10;i++)
            {
                System.IO.File.AppendAllText(fileName, "line " + i + Environment.NewLine);
            }
                        
            long fileSize = new FileInfo(fileName).Length;

            //Assert                       
            Assert.AreEqual(80, fileSize, "fileSize");            

            mTestHelper.AddTestArtifact(fileName);
        }


    }
}
