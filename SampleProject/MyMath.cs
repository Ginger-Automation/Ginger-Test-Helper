using System;
using System.Collections.Generic;

namespace SampleProject
{
    public class MyMath
    {
        public static int Sum(List<int> list)
        {
            int total = 0;
            foreach(int num in list)
            {
                total += num;
            }
            return total;
        }
    }
}
