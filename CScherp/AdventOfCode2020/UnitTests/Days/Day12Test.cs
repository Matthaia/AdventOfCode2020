using AdventOfCode2020.Days;
using System.Linq;
using Xunit;

namespace UnitTests.Days
{
	public class Day12Test
	{
		[Theory]
		[InlineData("N23", MoveType.North, 23)]
		[InlineData("E2", MoveType.East, 2)]
		[InlineData("S6", MoveType.South, 6)]
		[InlineData("W656", MoveType.West, 656)]
		[InlineData("L180", MoveType.Left, 180)]
		[InlineData("R90", MoveType.Right, 90)]
		[InlineData("F1232", MoveType.Forward, 1232)]
		public void MoveTest(string input, MoveType expectedType, int expectedValue)
		{
			var move = new Move(input);

			Assert.Equal(expectedType, move.MoveType);
			Assert.Equal(expectedValue, move.Value);
		}

		[Fact]
		public void Exercise1_SimpleTest()
		{
			var input = new string[] { "F10", "N3", "F7", "R90", "F11" };
			var day12 = new Day12();
			day12.Moves = input.Select(x => new Move(x)).ToList();

			var result = day12.Exercise1();

			Assert.Equal(25, result);
		}

		[Fact]
		public void Exercise1Test()
		{
			var day12 = new Day12();

			var result = day12.Exercise1();

			Assert.Equal(820, result);
		}

		[Fact]
		public void Exercise2_SimpleTest()
		{
			var input = new string[] { "F10", "N3", "F7", "R90", "F11" };
			var day12 = new Day12();
			day12.Moves = input.Select(x => new Move(x)).ToList();

			var result = day12.Exercise2();

			Assert.Equal(286, result);
		}

		[Fact]
		public void Exercise2Test()
		{
			var day12 = new Day12();

			var result = day12.Exercise2();

			Assert.Equal(66614, result);
		}
	}
}
