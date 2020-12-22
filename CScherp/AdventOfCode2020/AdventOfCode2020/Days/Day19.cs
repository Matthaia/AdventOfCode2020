using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Days
{
	public class Rule
	{
		public int Index { get; set; }
		public string Value { get; set; }
		public List<int> Left { get; set; }
		public List<int> Right { get; set; }

		public static (int, Rule) Parse(string input)
		{
			// 50: 15 65 | 60 74
			// 1: "a"
			// 102: 43 321
			var regex = new Regex(@"(?<index>\d+): ((?<letter>.a.|.b.)|((?<input1>\w+.*) \| (?<input2>\w+.*))|(?<input1>\w+.*))");

			var rule = new Rule();

			var matches = regex.Match(input).Groups;
			var index = int.Parse(matches["index"].Value);

			rule.Index = index;

			if (matches["letter"].Success)
				rule.Value = matches["letter"].Value.Replace("\"", ""); // It's already a string so no need to have "a" --> a.

			if (matches["input1"].Success)
				rule.Left = matches["input1"].Value.Split(" ").Select(int.Parse).ToList();

			if (matches["input2"].Success)
				rule.Right = matches["input2"].Value.Split(" ").Select(int.Parse).ToList();

			return (index, rule);
		}

		public bool IsValid(IDictionary<int, Rule> rules, string input)
		{
			if (Value != null)
				return input == Value;

			var leftValid = false;
			var rightValid = false;

			var leftChunkSize = input.Length / Left.Count;
			var leftParts = Enumerable.Range(0, input.Length / leftChunkSize).Select(i => input.Substring(i * leftChunkSize, leftChunkSize));

			leftValid = leftParts.Select((part, index) => rules[Left[index]].IsValid(rules, part)).All(x => x);

			//if (Left.Count == 1)
			//	leftValid = rules[Left[0]].IsValid(rules, input);
			//else
			//	leftValid = rules[Left[0]].IsValid(rules, input.Substring(0, input.Length / 2)) && rules[Left[1]].IsValid(rules, input.Substring(input.Length / 2, input.Length));
		

			if (Right == null)
				return leftValid;
			else
			{
				var rightChunkSize = input.Length / Left.Count;
				var rightParts = Enumerable.Range(0, input.Length / rightChunkSize).Select(i => input.Substring(i * rightChunkSize, rightChunkSize));

				rightValid = rightParts.Select((part, index) => rules[Left[index]].IsValid(rules, part)).All(x => x);
				//if (Right.Count == 1)
				//	rightValid = rules[Right[0]].IsValid(rules, input);
				//else
				//	rightValid = rules[Right[0]].IsValid(rules, input.Substring(0, input.Length / 2)) && rules[Right[1]].IsValid(rules, input.Substring(input.Length / 2, input.Length));
			}

			return leftValid || rightValid;
		}

		public override string ToString()
		{
			var result = $"{Index}: ";

			if (!string.IsNullOrEmpty(Value))
				return result += Value;

			result += string.Join(" ", Left);

			if (Right == null)
				return result;

			return result += $" | {string.Join(" ", Right)}";
		}
	}

	public class Day19
	{
		public Dictionary<int, Rule> Rules { get; set; }
		public List<string> Messages { get; set; }

		public Day19() : this(FileReader.ReadAllLines("Day19.txt")) { }

		public Day19(string[] lines)
		{
			Rules = new Dictionary<int, Rule>();
			Messages = new List<string>();

			var index = 0;

			for (var i = 0; i < lines.Length; i++)
			{
				if (string.IsNullOrWhiteSpace(lines[i]))
				{
					index = ++i;
					break;
				}
				var parsed = Rule.Parse(lines[i]);
				Rules[parsed.Item1] = parsed.Item2;
			}

			for (var i = index; i < lines.Length; i++)
			{
				Messages.Add(lines[i]);
			}
		}

		public int Exercise1()
		{
			var result = 0;

			foreach(var message in Messages)
			{
				var valid = Rules[0].IsValid(Rules, message);

				if (valid)
					result++;
			}

			return result;
		}
	}
}
