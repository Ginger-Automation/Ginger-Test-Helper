using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace GingerTestHelper
{
    public class TestHelper
    {
        private TestContext mTestContext;
        StringBuilder mLog = new StringBuilder();
        Stopwatch mTestStopwatch = new Stopwatch();
        Stopwatch mclassStopwatch = new Stopwatch();
        int PassedCounter;
        int FailedCounter;

        public void ClassInitialize(TestContext testContext)
        {
            this.mTestContext = testContext;
            WriteLog("ClassInitialize: " + testContext.FullyQualifiedTestClassName);
            WriteLog("_____________________________________________________________________________________________________________________________________");
            mclassStopwatch.Start();
        }
      
       
        private void WriteLog(string logMessage)
        {
            mLog.Append(DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss ")).Append(logMessage).Append(Environment.NewLine);
        }

        public void ClassCleanup()
        {
            mclassStopwatch.Stop();
            string logMessage = "ClassCleanup: " + mTestContext.FullyQualifiedTestClassName + ", Elapsed: " + mclassStopwatch.ElapsedMilliseconds;
            WriteLog(logMessage);
            WriteLog("Passed: " + PassedCounter);
            WriteLog("Failed: " + FailedCounter);
            
            if (mLog.Length > 0)
            {
                string fileName = Path.Combine(TestArtifactsFolder, mTestContext.FullyQualifiedTestClassName + ".log");
                System.IO.File.WriteAllText(fileName, mLog.ToString());
            }            
        }

        public void TestInitialize(TestContext testContext)
        {
            mTestContext = testContext;
            string logMessage = "TestInitialize: " + mTestContext.TestName;
            WriteLog(logMessage);
            mTestStopwatch.Restart();
        }

        public void TestCleanup()
        {
            mTestStopwatch.Stop();

            if (mTestContext.CurrentTestOutcome == UnitTestOutcome.Passed)
            {
                PassedCounter++;
            }
            else
            {
                FailedCounter++;
                WriteLog(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>   !!! Test Failed !!!   <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
            }
            string logMessage = "TestCleanup: " + mTestContext.TestName + ", Result: " + mTestContext.CurrentTestOutcome + ", Elapsed: " + mTestStopwatch.ElapsedMilliseconds;
            WriteLog(logMessage);
            WriteLog("_____________________________________________________________________________________________________________________________________");
        }

        string mTestArtifactsFolder; 
        string TestArtifactsFolder
        {
            get
            {
                if (mTestArtifactsFolder == null)
                {                                        
                    mTestArtifactsFolder = Path.Combine(TestResources.GetTestArtifactsFolder(), mTestContext.FullyQualifiedTestClassName);

                    // clean artifacts folder
                    if (Directory.Exists(mTestArtifactsFolder))
                    {
                        Directory.Delete(mTestArtifactsFolder, true);
                    };
                    
                    // Create new empty folder for test artifacts for the test class
                    Directory.CreateDirectory(mTestArtifactsFolder);                    
                }
                
                return mTestArtifactsFolder;
            }
        }


        /// <summary>
        /// Create test artifact file with content
        /// </summary>
        /// <param name="fileName">artifacts file name to create</param>
        /// <param name="content">text to be written to the file</param>
        public void CreateTestArtifact(string fileName, string content)
        {
            string fullPAth = Path.Combine(TestArtifactsFolder, fileName);
            System.IO.File.WriteAllText(fullPAth, content);
            WriteLog("Created Test artifact file with content = " + fileName);
        }

        /// <summary>
        /// Copy file to test artifacts folder
        /// </summary>
        /// <param name="fileName">Full path of existing file name</param>
        /// /// <param name="newFileName">Optional, provide new file name else will use original file name</param>
        public void AddTestArtifact(string fileName, string newFileName = null)
        {
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException("File not found", fileName);
            }
            string targetFile;
            if (newFileName == null)
            {
                targetFile = Path.Combine(TestArtifactsFolder, Path.GetFileName(fileName));
            }
            else
            {
                targetFile = newFileName;
            }
            File.Copy(fileName, targetFile);
            WriteLog("Added test artifact file = " + fileName);
        }

        public string GetTestResourcesFile(string fileName)
        {
            return TestResources.GetTestResourcesFile(fileName);
        }

        public string GetTestResourcesFile(string folder, string fileName)
        {
            return TestResources.GetTestResourcesFile(folder + Path.DirectorySeparatorChar + fileName);
        }

        public string GetTestResourcesFile(string folder1, string folder2, string fileName)
        {
            return TestResources.GetTestResourcesFile(folder1 + Path.DirectorySeparatorChar + folder2 + Path.DirectorySeparatorChar + fileName);
        }

        public string GetTestResourcesFile(string folder1, string folder2, string folder3, string fileName)
        {
            return TestResources.GetTestResourcesFile(folder1 + Path.DirectorySeparatorChar + folder2 + Path.DirectorySeparatorChar + folder3 + Path.DirectorySeparatorChar + fileName);
        }


        /// <summary>
        /// Return a new temp file name which include Test Resources temp folder + ClassName + "." + fileName
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string GetTempFileName(string fileName)
        {
            return TestResources.GetTempFile(mTestContext.FullyQualifiedTestClassName + "." + fileName);
        }

        public string GetTempFolder(string path1, string path2 = null, string path3 = null)
        {
            return TestResources.GetTestTempFolder(path1, path2, path3);
        }



        public void Log(string message)
        {
            WriteLog(message);
        }
    }
}
