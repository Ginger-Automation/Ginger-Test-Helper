#region License
/*
Copyright © 2014-2018 European Support Limited

Licensed under the Apache License, Version 2.0 (the "License")
you may not use this file except in compliance with the License.
You may obtain a copy of the License at 

http://www.apache.org/licenses/LICENSE-2.0 

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS, 
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
See the License for the specific language governing permissions and 
limitations under the License. 
*/
#endregion

using System;
using System.IO;
using System.Reflection;

namespace GingerTestHelper
{
    public class TestResources
    {
        public static Assembly Assembly { get; set; }

        private static string GetUnitTestBinPath()
        {
            if (Assembly is null)
            {
                throw new Exception("Assembly need to be set - you might have to copy the TestAssemblyInit.cs file in your test project");
            }

            return System.IO.Path.GetDirectoryName(Assembly.Location);
        }

        private static string GetTestResourcesFolder()
        {
            return Path.Combine(GetUnitTestBinPath(), @"TestResources");
        }

        public static string GetTestTempFolder(string path1, string path2 = null, string path3 = null)
        {
            // TODO: when test start clear this folder

            string tempFolder = Path.Combine(GetUnitTestBinPath(), "TestTemp", path1);
            if (!System.IO.Directory.Exists(tempFolder))
            {
                System.IO.Directory.CreateDirectory(tempFolder);
            }
            if (path2 != null)
            {
                tempFolder = Path.Combine(tempFolder, path2);
                if (!System.IO.Directory.Exists(tempFolder))
                {
                    System.IO.Directory.CreateDirectory(tempFolder);
                }
            }
            if (path3 != null)
            {
                tempFolder = Path.Combine(tempFolder, path3);
                if (!System.IO.Directory.Exists(tempFolder))
                {
                    System.IO.Directory.CreateDirectory(tempFolder);
                }
            }

            return tempFolder;
        }

        public static string GetTestResourcesFile(string fileName)
        {
            string fullPath = Path.Combine(GetTestResourcesFolder(), fileName);
            if (!System.IO.File.Exists(fullPath))
            {
                throw new Exception("Test resource file not found: " + fullPath + " >>> Verify File->Properties->CopyToOutPutDirectory");
            }
            return fullPath;
        }

        public static string GetTestResourcesFolder(string folder)
        {
            string fullPath = Path.Combine(GetTestResourcesFolder(), folder);
            if (!System.IO.Directory.Exists(fullPath))
            {
                throw new Exception("Test resource folder not found: " + fullPath + " >>> Verify File->Properties->CopyToOutPutDirectory");
            }
            return fullPath;
        }

        public static string GetTempFile(string fileName)
        {
            return Path.Combine(GetTestTempFolder(""), fileName);
        }

        public static string GetTempFolder(string folderName)
        {
            return GetTestTempFolder(folderName);
        }

        public static string GetTestArtifactsFolder()
        {
            // TODO: when test start clear this folder

            string testArtifactsFolder = Path.Combine(GetUnitTestBinPath(), "TestArtifacts");
            if (!System.IO.Directory.Exists(testArtifactsFolder))
            {
                System.IO.Directory.CreateDirectory(testArtifactsFolder);
            }
            return testArtifactsFolder;

        }
    }
}
