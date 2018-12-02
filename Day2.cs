using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace AoC_2018
{
    public class Day2 : IDay
    {
        public void Do()
        {
            //part1
            int twos = 0;
            int threes = 0;
            StreamReader sr = new StreamReader("inputs/day2.txt");
            while (!sr.EndOfStream)
            {
                Dictionary<char, int> dict = new Dictionary<char, int>();
                string line = sr.ReadLine();
                for (int i=0; i < line.Length;i++)
                {
                    if (dict.ContainsKey(line[i]))
                        dict[line[i]]++;
                    else
                        dict.Add(line[i],1);
                }
                bool tw=false;
                bool th=false;
                foreach (char k in dict.Keys)
                {
                    if (dict[k]==2)
                        tw=true;
                    if (dict[k]==3)
                        th=true;
                }
                if (tw)
                    twos++;
                if (th)
                    threes++;
            }

            Console.WriteLine("Part 1:" + twos*threes);

            sr = new StreamReader("inputs/day2.txt");
            List<string> lines = new List<string>();
            while (!sr.EndOfStream)
            {
                string line=sr.ReadLine();

                foreach (string l in lines)
                {
                    int diff =0;
                    int index=0;
                    for (int i=0; i<l.Length;i++)
                    {
                        if (l[i]!=line[i])
                        {
                            if (diff==0)
                                index=i;
                            diff++;
                        }
                    }
                   if (diff==1)
                    {
                        Console.WriteLine(line + " differs from " + l + 
                                            " by one letter - " + line[index]
                                            + " solution: " + line.Remove(index,1));
                        return;
                    }
                }

                lines.Add(line);
            }
        }
    }
}