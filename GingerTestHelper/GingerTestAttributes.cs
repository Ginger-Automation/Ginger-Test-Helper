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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace GingerTestHelper
{
    /// <summary>
    /// Basic core tests runs fast usually in ms (must be less than 500 ms), can run in parallel – no dependency – no Mutex
    /// </summary>

    public class Level1Attribute : TestCategoryBaseAttribute
    {
        public override IList<string> TestCategories
        {
            get { return new List<string> { "Level1" }; }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Level2Attribute : TestCategoryBaseAttribute
    {
        public override IList<string> TestCategories
        {
            get { return new List<string> { "Level2" }; }
        }
    }

    /// <summary>
    /// include UI testing launch browsers, test app etc, can have Mutex – sequencial run slower 
    /// </summary>
    public class Level3Attribute : TestCategoryBaseAttribute
    {
        public override IList<string> TestCategories
        {
            get { return new List<string> { "Level3" }; }
        }
    }
}