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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GingerTestHelper
{
    public class TestResources
    {
        public static Assembly Assembly {get; set;}        

        private static string GetUnitTestBinPath()
        {
            if (Assembly is null)
            {
                throw new Exception("Assembly need to be set");
            }

            string s = System.IO.Path.GetDirectoryName(Assembly.Location);            
            return s;
        }

        private static string GetTestResourcesFolder()
        {            
            string DocumentsFolder = Path.Combine(GetUnitTestBinPath(), @"TestResources");
            return DocumentsFolder;
        }

        public static string getGingerUnitTesterTempFolder(string path1, string path2 =null, string path3 =null)
        {
            // TODO: when test start clear this folder
            
            string TempFolder = Path.Combine(GetUnitTestBinPath(), "TempFolder" , path1);
            if (!System.IO.Directory.Exists(TempFolder))
            {
                System.IO.Directory.CreateDirectory(TempFolder);
            }
            if (path2 != null)
            {
                TempFolder = Path.Combine(TempFolder, path2);
                if (!System.IO.Directory.Exists(TempFolder))
                {
                    System.IO.Directory.CreateDirectory(TempFolder);
                }
            }
            if (path3 != null)
            {
                TempFolder = Path.Combine(TempFolder, path3);
                if (!System.IO.Directory.Exists(TempFolder))
                {
                    System.IO.Directory.CreateDirectory(TempFolder);
                }
            }

            return TempFolder;
        }

        public static string GetTestResourcesFile(string fileName)
        {
            string FullPath = Path.Combine(GetTestResourcesFolder(), fileName);
            if (!System.IO.File.Exists(FullPath))
            {
                throw new Exception("Test resource file not found: " + FullPath + " >>> Verify File->Properties->CopyToOutPutDirectory");
            }
            return FullPath;
            
        }

        public static string GetTestResourcesFolder(string folder)
        {
            string FullPath = Path.Combine(GetTestResourcesFolder(), folder);
            if (!System.IO.Directory.Exists(FullPath))
            {
                throw new Exception("Test resource folder not found: " + FullPath + " >>> Verify File->Properties->CopyToOutPutDirectory");
            }
            return FullPath;
        }

        public static string GetTempFile(string fileName)
        {
            return Path.Combine(getGingerUnitTesterTempFolder(""), fileName);
        }
    }
}
