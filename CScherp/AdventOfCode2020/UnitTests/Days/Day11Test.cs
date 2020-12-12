using AdventOfCode2020.Days;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace UnitTests.Days
{
	public class Day11Test
	{
		private const string TestFilesPath = "../../../Input/Day11/";

		[Fact]
		public void SetupCorrectly()
		{
			var day = new Day11();

			Assert.True(day.Plane.Seats.Length > 0);
		}

		[Theory]
		[InlineData(0, 0, 2)]
		[InlineData(0, 1, 3)]
		[InlineData(0, 2, 3)]
		[InlineData(1, 0, 4)]
		[InlineData(1, 1, 6)]
		[InlineData(1, 2, 5)]
		[InlineData(2, 0, 2)]
		[InlineData(2, 1, 4)]
		[InlineData(2, 2, 3)]
		public void NumberOfAdjecentOccupiedSeatsTest(int y, int x, int expected)
		{
			var airplane = new Day11Airplane(new string[] {
				".##",
				"L##",
				"###"
			});

			var result = airplane.NumberOfAdjecentOccupiedSeats(x, y);

			Assert.Equal(expected, result);
		}

		[Fact]
		public void CopyTest()
		{
			var input = @"L.LL.LL.LL
LLLLLLL.LL
L.L.L..L..
LLLL.LL.LL
L.LL.LL.LL
L.LLLLL.LL
..L.L.....
LLLLLLLLLL
L.LLLLLL.L
L.LLLLL.LL";

			var airplane = new Day11Airplane(input.Split("\r\n"));

			var result = new Day11Airplane(airplane.GetCopy());

			Assert.True(airplane.Equals(result));
			Assert.True(result.Equals(airplane));
		}


		[Theory]
		[InlineData("Fase1.txt", "Fase2.txt", 71)]
		[InlineData("Fase2.txt", "Fase3.txt", 51)]
		[InlineData("Fase3.txt", "Fase4.txt", 31)]
		[InlineData("Fase4.txt", "Fase5.txt", 21)]
		[InlineData("Fase5.txt", "Fase6.txt", 7)]
		[InlineData("Fase6.txt", "Fase7.txt", 0)]
		public void Exercise1SmallTest(string start, string end, int expectedChanges)
		{
			var plane = new Day11Airplane(File.ReadAllLines($"{TestFilesPath}{start}"));
			var (changes, seats) = plane.Excercise1();

			var expected = new Day11Airplane(File.ReadAllLines($"{TestFilesPath}{end}"));
			var actual = new Day11Airplane(seats);

			Assert.Equal(expected, actual);
			Assert.Equal(expectedChanges, changes);
		}

		[Fact]
		public void Exercise1Test()
		{
			var plane = new Day11().Plane;
			var (changes, seats) = plane.Excercise1();
			var history = new List<(int changes, char[][] seats)> { (0, plane.Seats) };

			while (changes > 0)
			{
				history.Add((changes, seats));
				plane = new Day11Airplane(seats);
				(changes, seats) = plane.Excercise1();
			}

			var filledSeats = 0;
			for (int y = 0; y < seats.Length; y++)
			{
				for (int x = 0; x < seats[y].Length; x++)
				{
					if (seats[y][x] == '#') filledSeats++;
				}
			}

			Assert.Equal(2468, filledSeats);
		}

		[Fact]
		public void NumberOfAdjecentOccupiedSeatsWithJumpsTest_0()
		{
			var input = @".##.##.
#.#.#.#
##...##
...L...
##...##
#.#.#.#
.##.##.".Split("\r\n");

			var plane = new Day11Airplane(input);

			var result = plane.NumberOfAdjecentOccupiedSeats(3, 3, true);

			Assert.Equal(0, result);
		}


		[Fact]
		public void NumberOfAdjecentOccupiedSeatsWithJumpsTest_8()
		{
			var input = @".......#.
...#.....
.#.......
.........
..#L....#
....#....
.........
#........
...#.....".Split("\r\n");

			var plane = new Day11Airplane(input);

			var result = plane.NumberOfAdjecentOccupiedSeats(3, 4, true);

			Assert.Equal(8, result);
		}

		[Fact]
		public void NumberOfAdjecentOccupiedSeatsWithJumpsTest_1()
		{
			var input = @".............
.L.L.#.#.#.#.
.............".Split("\r\n");

			var plane = new Day11Airplane(input);

			var result = plane.NumberOfAdjecentOccupiedSeats(3, 4, true);

			Assert.Equal(0, result);
		}



		[Fact]
		public void Exercise2Test()
		{
			var plane = new Day11().Plane;
			var (changes, seats) = plane.Excercise2();
			var history = new List<(int changes, char[][] seats)> { (0, plane.Seats) };

			while (changes > 0)
			{
				history.Add((changes, seats));
				plane = new Day11Airplane(seats);
				(changes, seats) = plane.Excercise2();
			}

			var filledSeats = 0;
			for (int y = 0; y < seats.Length; y++)
			{
				for (int x = 0; x < seats[y].Length; x++)
				{
					if (seats[y][x] == '#') filledSeats++;
				}
			}

			Assert.Equal(2214, filledSeats);
		}
	}
}