using AdventOfCode2020.Days;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace UnitTests.Days
{
	public class Day10Test
	{
		private const string TestFilesPath = "../../../Input/Day10/";

		[Fact]
		public void Exercise1Test()
		{
			var day10 = new Day10();

			Assert.Equal(1836, day10.Exercise1());
		}

		[Fact]
		public void Exercise2_SimpleTest()
		{
			var day10 = new Day10(new List<int> { 16, 10, 15, 5, 1, 11, 7, 19, 6, 12, 4 });

			Assert.Equal((long)8, day10.Exercise2());
		}

		[Fact]
		public void Exercise2_MediumTest()
		{
			var day10 = new Day10(File.ReadAllLines($"{TestFilesPath}MediumInput.txt").Select(x => int.Parse(x)).ToList());

			Assert.Equal(19208, day10.Exercise2());
		}

		[Fact]
		public void Exercise2Test()
		{
			var day10 = new Day10();

			Assert.Equal(43406276662336, day10.Exercise2());
		}
	}
}
