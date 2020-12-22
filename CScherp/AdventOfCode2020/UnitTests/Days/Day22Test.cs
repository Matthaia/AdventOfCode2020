using AdventOfCode2020.Days;
using System.IO;
using Xunit;

namespace UnitTests.Days
{
	public class Day22Test
	{
		private const string TestFilesPath = "../../../Input/Day22/";

		[Fact]
		public void ParseTest()
		{
			var day = new Day22();

			Assert.Equal(25, day.Player1.Cards.Count);
			Assert.Equal(25, day.Player2.Cards.Count);
		}

		[Fact]
		public void AddCardsTest()
		{
			var player = new Player();
			player.AddCards(123, 1, 532, 234, 634, 23);

			Assert.Equal(634, player.Cards.Dequeue());
			Assert.Equal(532, player.Cards.Dequeue());
			Assert.Equal(234, player.Cards.Dequeue());
			Assert.Equal(123, player.Cards.Dequeue());
			Assert.Equal(23, player.Cards.Dequeue());
			Assert.Equal(1, player.Cards.Dequeue());
		}

		[Fact]
		public void Exercise1_SimpleTest()
		{
			var input = File.ReadAllLines($"{TestFilesPath}/Day22SimpleTest.txt");
			var day = new Day22(input);

			var result = day.Exercise1();

			Assert.Equal(306, result);
		}

		[Fact]
		public void Exercise1Test()
		{
			var day = new Day22();

			var result = day.Exercise1();

			Assert.Equal(32598, result);
		}

		[Fact]
		public void Exercise2_SimpleTest()
		{
			var input = File.ReadAllLines($"{TestFilesPath}/Day22SimpleTest.txt");
			var day = new Day22(input);

			var result = day.Exercise2();

			Assert.Equal(291, result);
		}

		[Fact]
		public void Exercise2Test()
		{
			var day = new Day22();

			var result = day.Exercise2();

			Assert.Equal(35836, result);
		}
	}
}
