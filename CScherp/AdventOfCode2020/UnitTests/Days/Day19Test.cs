using AdventOfCode2020.Days;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace UnitTests.Days
{
	public class RuleParseTestDataGenerator : IEnumerable<object[]>
	{
		private readonly List<object[]> data = new List<object[]>
		{
			// Create string without escapes: "1: \"a\"" --> "1: "b""
			new object[] { new StringBuilder("1: ").Append('"').Append("a").Append('"').ToString(), 1, "a", null, null },
			new object[] { "87: 32", 87, null, new List<int> { 32 }, null },
			new object[] { "102: 43 321", 102, null, new List<int> { 43, 321 }, null },
			new object[] { "32: 43 | 32 24", 32, null, new List<int> { 43 }, new List<int> { 32, 24 } },
			new object[] { "2: 43 76 | 32", 2, null, new List<int> { 43, 76 }, new List<int> { 32 } },
			new object[] { "50: 15 65 | 60 74", 50, null, new List<int> { 15, 65 }, new List<int> { 60, 74 } }
		};

		public IEnumerator<object[]> GetEnumerator() => data.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}

	public class Day19Test
	{
		private const string TestFilesPath = "../../../Input/Day19/";

		[Fact]
		public void ParseTest()
		{
			var day = new Day19();

			Assert.Equal(129, day.Rules.Count);
			Assert.Equal(454, day.Messages.Count);
		}

		[Theory]
		[ClassData(typeof(RuleParseTestDataGenerator))]
		public void RuleParseTest(string input, int expectedIndex, string expectedValue = null, IList<int> expectedLeft = null, IList<int> expectedRight = null)
		{
			var (index, rule) = Rule.Parse(input);

			Assert.Equal(expectedIndex, index);
			Assert.Equal(expectedValue, rule.Value);
			Assert.Equal(expectedLeft, rule.Left);
			Assert.Equal(expectedRight, rule.Right);
		}

		[Fact]
		public void Day1SimpleTest()
		{
			var input = File.ReadAllLines($"{TestFilesPath}/Exercise1Simple.txt");

			var day = new Day19(input);

			var result = day.Exercise1();

			Assert.Equal(2, result);
		}
	}
}
