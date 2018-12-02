using System;
using System.Collections.Generic;

namespace AoC_2018
{
    class Program
    {
        static void Main(string[] args)
        {
			List<IDay> days = new List<IDay>();
			days.Add(new Day1());
            days.Add(new Day2());

			days[days.Count-1].Do();
			Console.ReadLine();        
            }
    }
}
