using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Days
{
	public class MemoryValue
	{
		public char[] Bits { get; set; }
		public MemoryValue()
		{
			Bits = Enumerable.Repeat('0', 36).ToArray();
		}

		public MemoryValue(string value)
		{
			Bits = value.Split(" = ")[1].ToCharArray();
		}

		public MemoryValue(long value)
		{
			Bits = Convert.ToString(value, 2).PadLeft(36, '0').ToCharArray();
		}

		public MemoryValue Mask(MemoryValue mask)
		{
			var result = new MemoryValue();

			for (int i = 0; i < 36; i++)
			{
				result.Bits[i] = mask.Bits[i] == 'X' ? Bits[i] : mask.Bits[i];
			}

			return result;
		}

		public MemoryValue MaskV2(MemoryValue mask)
		{
			var result = new MemoryValue();

			for (int i = 0; i < 36; i++)
			{
				if (mask.Bits[i] == '0')
					result.Bits[i] = Bits[i];
				else
					result.Bits[i] = mask.Bits[i];
			}

			return result;
		}

		public IList<long> GetMemoryAddresses(MemoryValue mask)
		{
			var results = new List<string> { "" };

			var masked = MaskV2(mask);

			for (int i = 0; i < 36; i++)
			{
				var bit = masked.Bits[i];

				if (bit != 'X')
					for (int y = 0; y < results.Count; y++)
						results[y] += bit;
				else
				{
					var backupResults = results.Select(x => x += '1').ToList();

					for (int y = 0; y < results.Count; y++)
						results[y] += '0';

					foreach (var result in backupResults)
						results.Add(result);
				}
			}

			return results.Select(x => Convert.ToInt64(x, 2)).OrderBy(x => x).ToList();
		}

		public long ToLong()
		{
			return Convert.ToInt64(string.Join("", Bits), 2);
		}
	}

	public class Day14
	{
		public string[] Lines { get; set; }

		public Day14() : this(FileReader.ReadAllLines("Day14.txt")) { }

		public Day14(string[] lines)
		{
			Lines = lines;
		}

		public long Exercise1()
		{
			var mask = new MemoryValue(Lines[0]);
			var memory = new Dictionary<int, MemoryValue>();

			for (int i = 1; i < Lines.Length; i++)
			{
				var line = Lines[i];

				if (line.StartsWith("mask"))
				{
					mask = new MemoryValue(line);
					continue;
				}

				var address = int.Parse(line.Split('[', ']')[1]);

				memory[address] = new MemoryValue(long.Parse(line.Split(" = ")[1])).Mask(mask);
			}

			return memory.Values.Sum(x => x.ToLong());
		}

		public long Exercise2()
		{
			var mask = new MemoryValue(Lines[0]);
			var memory = new Dictionary<long, long>();

			for (int i = 1; i < Lines.Length; i++)
			{
				var line = Lines[i];

				if (line.StartsWith("mask"))
				{
					mask = new MemoryValue(line);
					continue;
				}

				var address = int.Parse(line.Split('[', ']')[1]);
				var value = long.Parse(line.Split(" = ")[1]);
				var addresses = new MemoryValue(address).GetMemoryAddresses(mask);

				foreach (var item in addresses)
				{
					memory[item] = value;
				}
			}

			return memory.Values.Sum(x => x);
		}
	}
}
