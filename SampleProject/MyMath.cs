using System;
using System.Collections.Generic;

namespace SampleProject
{
    public class MyMath
    {
        public static int Sum(List<int> list)
        {
            int total = 0;
            foreach (int num in list)
            {
                total += num;
            }
            return total;
        }

        public static int Sum(string fileName)
        {
            string txt = System.IO.File.ReadAllText(fileName);
            string[] numbers = txt.Split(',');
            List<int> list = new List<int>();
            foreach (string number in numbers)
            {
                list.Add(int.Parse(number));
            }
            return Sum(list);
        }
    }
}