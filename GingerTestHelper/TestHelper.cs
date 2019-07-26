using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
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

        public TestHelper(TestContext testContext)
        {                    
            this.mTestContext = testContext;            
            WriteLog("ClassInitialize: " + testContext.FullyQualifiedTestClassName);
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
            if (mLog.Length > 0)
            {
                string fileName = Path.Combine(TestArtifactsFolder, mTestContext.FullyQualifiedTestClassName + ".Log");
                System.IO.File.WriteAllText(fileName, mLog.ToString());
            }            
        }

        public void TestInitialize()
        {
            string logMessage = "TestInitialize: " + mTestContext.TestName;
            WriteLog(logMessage);
            mTestStopwatch.Restart();
        }

        public void TestCleanup()
        {
            mTestStopwatch.Stop();
            string logMessage = "TestCleanup: " + mTestContext.TestName + ", Result: " + mTestContext.CurrentTestOutcome + ", Elapsed: " + mTestStopwatch.ElapsedMilliseconds;
            WriteLog(logMessage);
            WriteLog("======================================================================================================================================");
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
                    if (Directory.Exists(TestArtifactsFolder))
                    {
                        Directory.Delete(TestArtifactsFolder, true);
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
            WriteLog("Test artifact file name = " + fileName);
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
            WriteLog("Test artifact file name = " + fileName);
        }

        public string GetTestResourcesFile(string fileName)
        {
            return TestResources.GetTestResourcesFile(fileName);
        }

        public void Log(string message)
        {
            WriteLog(message);
        }
    }
}
