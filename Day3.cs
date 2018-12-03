using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace AoC_2018
{
	class Day3 : IDay
	{
		struct FabricElement {
			public List<int> rectangleList;
		}

		public void Do()
		{
			StreamReader sr = new StreamReader("inputs/day3.txt");
			FabricElement[,] map = new FabricElement[1000,1000];
			List<int> part2candidates = new List<int>();
			while (!sr.EndOfStream)
			{
				string line = sr.ReadLine();
				MatchCollection matches = Regex.Matches(line, @"\d+");
				int id = Convert.ToInt32(matches[0].Value);
				int x = Convert.ToInt32(matches[1].Value);
				int y = Convert.ToInt32(matches[2].Value);
				int sizex = Convert.ToInt32(matches[3].Value);
				int sizey = Convert.ToInt32(matches[4].Value);
				part2candidates.Add(id);
				for (int i = x; i < x+sizex; i++)
				{
					for (int j = y; j < y+sizey; j++)
					{
						if (map[i, j].rectangleList == null)
							map[i, j].rectangleList = new List<int>();

						map[i, j].rectangleList.Add(id);
					}
				}
			}
			int part1 = 0;

			for (int i = 0; i < map.GetLength(0); i++)
			{
				for (int j = 0; j < map.GetLength(1); j++)
				{
					if (map[i, j].rectangleList != null)
					{
						if (map[i, j].rectangleList.Count > 1)
						{
							part1++;
							for (int k = 0; k < map[i,j].rectangleList.Count; k++)
							{
								part2candidates.Remove(map[i, j].rectangleList[k]);
							}
						}
					}
				}
			}
			Console.WriteLine(part1);
			Console.WriteLine(part2candidates[0]);
		}
	}
}
