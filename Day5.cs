using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace AoC_2018
{
	class Day5 : IDay
	{
		public void Do()
		{
			StreamReader sr = new StreamReader("inputs/day5.txt");
			string originalPolymer = sr.ReadToEnd();
			
			Console.WriteLine("Part 1: " + React(originalPolymer).Length);
			Dictionary<char, int> removalMap = new Dictionary<char, int>();
			for (char i = 'A'; i <= 'Z'; i++)
			{
				removalMap.Add(i, 0);
				removalMap.Add(char.ToLower(i), 0);
			}
			foreach (char key in removalMap.Keys.ToArray())
			{
				StringBuilder sb = new StringBuilder(originalPolymer);
				sb.Replace(char.ToLower(key).ToString(), "");
				sb.Replace(char.ToUpper(key).ToString(), "");
				removalMap[key] = React(sb.ToString()).Length;
			}
			Console.WriteLine("Part 2: " + removalMap.Min(x => x.Value));
		}

		string React(string originalPolymer)
		{
			StringBuilder polymer = new StringBuilder(originalPolymer);
			bool reaction = true;
			StringBuilder sb = new StringBuilder();
			while (reaction)
			{
				sb.Clear();
				reaction = false;
				for (int i = 0; i < polymer.Length; i++)
				{
					if (i + 1 < polymer.Length)
					{
						if ((polymer[i] == char.ToUpper(polymer[i + 1]) || polymer[i] == char.ToLower(polymer[i + 1])) && polymer[i] != polymer[i + 1])
						{
							//destroyed
							i++; //skip next char
							reaction = true;
						}
						else
						{
							sb.Append(polymer[i]);
						}
					}
					else
						sb.Append(polymer[i]);
				}
				polymer = new StringBuilder(sb.ToString());
			}
			return polymer.ToString();
		}
	}
}
