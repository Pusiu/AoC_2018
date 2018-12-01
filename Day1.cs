using System;
using System.Collections.Generic;
using System.IO;

namespace AoC_2018
{
    public class Day1 : IDay
    {
        public void Do()
        {
            string line;
            int curfreq=0;
            bool found = false;
            List<int> freqList = new List<int>();
            while (!found)
            {
                StreamReader sr = new StreamReader("inputs/day1.txt");
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    int n = Convert.ToInt32(line);
                    curfreq+=n;
                    if (!freqList.Contains(curfreq))
                        freqList.Add(curfreq);
                    else
                    {
                        found = true;
                        break;
                    }
                }
            }
            Console.WriteLine(curfreq);
        }
    }
}