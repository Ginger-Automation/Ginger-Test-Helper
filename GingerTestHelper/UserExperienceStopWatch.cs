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
using System.Diagnostics;
using System.Text;

namespace GingerCoreNETUnitTest.GeneralLib
{
    public class UserExperienceStopWatch
    {
        static bool bDone = false;
        static double mWorkUnitPerSeconds;

        Stopwatch st = new Stopwatch();

        long StartBytes;
        long MemUsedBytes;

        public enum eUserExperinceScore
        {
            VeryFast,
            Fast,
            Slow,
            /// <summary>
            /// More than 5 seconds
            /// </summary>
            VerySlow
        }

        public UserExperienceStopWatch()
        {
            if (!bDone)
            {
                CalcTPS();
            }
            st.Start();
            
            StartBytes = System.GC.GetTotalMemory(true);                        
        }

        //public void Start()
        //{
        //    st.Start();
        //}

        //public void Stop()
        //{
        //    st.Stop();
        //}

        public double ElapsedMillisecondsapsedSec()
        {
            return st.ElapsedMilliseconds;
        }

        public long ElapsedUnifedUnits
        {            
            get
            {
                if (st.IsRunning)  // Prevent from double stop in debug or when getting the param again
                {
                    st.Stop();

                    long StopBytes = 0;
                    StopBytes = System.GC.GetTotalMemory(true);
                    MemUsedBytes = ((long)(StopBytes - StartBytes));
                }
                return (long)(st.ElapsedTicks / mWorkUnitPerSeconds / (double)1000);
            }
        }

        public eUserExperinceScore UserExperienceScore
        {
            get
            {                
                long EUU = ElapsedUnifedUnits;

                if (EUU <= 1000) return eUserExperinceScore.VeryFast;
                if (EUU > 1000 && EUU <3000) return eUserExperinceScore.Fast;
                if (EUU > 3000 && EUU < 5000) return eUserExperinceScore.Slow;
                return eUserExperinceScore.VerySlow;
                
            }
        }


        private void CalcTPS()
        {
            lock (this)
            {
                if (bDone) return;
                Stopwatch st = new Stopwatch();
                st.Start();

                // first time is just warm up;
                DoWorkUnit();

                //TODO: Now we run 10 times and take median not avg
                
                st.Stop();

                mWorkUnitPerSeconds = (double)1000 / (double)st.ElapsedTicks;   // 1000 is 1 sec dividied by the time it took we get how many work unit we can do per seconds
            }
        }

        private void DoWorkUnit()
        {
            //DO some sample work - read files calc etc.
            // strings are heavy so good for testing
            
            string s = "";
            int x = 0;
            double total = 0;
            for (int i = 0; i < 50; i++)
            {
                for (int j = 0; j < 50; j++)
                {
                    s = s + i;
                    string[] a = s.Split('1');
                    total += a.Length;
                    x = i * j;
                }
            }

        }


    }
}
