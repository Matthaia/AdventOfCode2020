using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Days
{
	public class Day10
	{
		public IList<int> Jolts { get; set; }
		public IList<int> SortedJolts => Jolts.OrderBy(x => x).ToList();

		public Day10()
		{
			var rawInput = FileReader.ReadAllLines("Day10.txt");

			Jolts = new List<int> { 0 };
			foreach (var input in rawInput)
				Jolts.Add(int.Parse(input));

			Jolts.Add(SortedJolts[Jolts.Count - 1] + 3);
		}

		public Day10(IList<int> input)
		{
			Jolts = new List<int> { 0 };
			foreach (var number in input)
				Jolts.Add(number);

			Jolts.Add(SortedJolts[Jolts.Count - 1] + 3);
		}

		public int Exercise1()
		{
			var one = 0;
			var three = 0;

			for (int i = 0; i < SortedJolts.Count - 1; i++)
			{
				if (SortedJolts[i + 1] - SortedJolts[i] == 1)
					one++;
				else
					three++;
			}

			return one * three;
		}

		public long Exercise2()
		{
			var jolts = SortedJolts.Reverse();
			var subTreeCounts = new Dictionary<int, long>();

			foreach (var jolt in jolts)
			{
				var possibleNext = jolts.Where(j => j > jolt && j <= jolt + 3);
				subTreeCounts[jolt] = possibleNext.Select(n => subTreeCounts[n]).Sum();

				if (subTreeCounts[jolt] == 0)
					subTreeCounts[jolt] = 1;
			}

			return subTreeCounts[0];
		}
	}
}
