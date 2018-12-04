using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AoC_2018
{
	class Day4Guard
	{
		public int id = -1;
		public Dictionary<int, int> shiftPattern = new Dictionary<int, int>(); //value is how many times at that minute guard was sleeping
		public int lastOrderIndex = 0;
		public Day4Guard(int id)
		{
			for (int i = 0; i < 60; i++)
			{
				shiftPattern.Add(i, 0);
			}
			this.id = id;
		}

		public void FallAsleep(int minute)
		{
			shiftPattern[minute]++;
			lastOrderIndex = minute;
		}
		public void WakeUp(int minute)
		{
			for (int i = lastOrderIndex+1; i < minute; i++)
			{
				shiftPattern[i]++;
			}
		}

		public override string ToString()
		{
			return id.ToString();
		}
	}

	class Day4 : IDay
	{

		public void Do()
		{
			StreamReader sr = new StreamReader("inputs/day4.txt");
			SortedList<DateTime,string> orders = new SortedList<DateTime, string>();
			while (!sr.EndOfStream)
			{
				string line = sr.ReadLine();
				MatchCollection m = Regex.Matches(line, @"\d+");
				DateTime t = new DateTime(Convert.ToInt32(m[0].Value),
											Convert.ToInt32(m[1].Value),
											Convert.ToInt32(m[2].Value),
											Convert.ToInt32(m[3].Value),
											Convert.ToInt32(m[4].Value),
											0);
				orders.Add(t, line);
			}
			List<Day4Guard> guards = new List<Day4Guard>();
			Day4Guard lastGuard = null;
			for (int i = 0; i < orders.Count; i++)
			{
				Day4Guard g = lastGuard;
				string line;
				orders.TryGetValue(orders.Keys[i], out line);
				DateTime time = orders.Keys[i];
				MatchCollection m = Regex.Matches(line, @"\d+");
				if (m.Count == 6)
				{
					int id = Convert.ToInt32(m[5].Value);
					lastGuard = guards.Where(x => x.id == id).FirstOrDefault();
					if (lastGuard == null)
					{ 
						lastGuard = new Day4Guard(id);
						guards.Add(lastGuard);
					}

				}
				else
				{
					if (line.Contains("fall"))
						lastGuard.FallAsleep(time.Minute);
					else
						lastGuard.WakeUp(time.Minute);
				}
			}
			Day4Guard mostSleepingGuard = guards[0];
			foreach (Day4Guard g in guards)
			{
				if (g.shiftPattern.Sum(x => x.Value) > mostSleepingGuard.shiftPattern.Sum(x => x.Value))
					mostSleepingGuard = g;
			}
			int maxMinute = mostSleepingGuard.shiftPattern.Aggregate((l,r) => l.Value > r.Value ? l : r).Key; //https://stackoverflow.com/questions/2805703/good-way-to-get-the-key-of-the-highest-value-of-a-dictionary-in-c-sharp
			Console.WriteLine("Part 1: " + mostSleepingGuard.id * maxMinute);

			int mostSleptMinute = 0;
			/*for (int i = 0; i < 60; i++)
			{
				foreach (Day4Guard g in guards)
				{
					if (g.shiftPattern[i] > mostSleepingGuard.shiftPattern[i])
					{
						mostSleepingGuard = g;
					}
					if (mostSleptMinute == -1)
						mostSleptMinute = mostSleepingGuard.shiftPattern[i];
					else
					{
						if (g == mostSleepingGuard)
						{
							if (g.shiftPattern[i] > g.shiftPattern[mostSleptMinute])
								mostSleptMinute = i;
						}
					}
				}
			}*/
			mostSleepingGuard = guards[0];
			mostSleptMinute = 0;
			for (int i = 0; i < 60; i++)
			{
				foreach (Day4Guard g in guards)
				{
					if (g.shiftPattern[i] > mostSleepingGuard.shiftPattern[mostSleptMinute])
					{
						mostSleepingGuard = g;
						mostSleptMinute = i;
					}
				}
			}
			Console.WriteLine("Part 2: " + mostSleepingGuard.id * mostSleptMinute);
		}
	}
}
