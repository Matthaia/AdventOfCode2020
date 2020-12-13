using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020.Days
{
	public enum Direction
	{
		Top,
		TopRight,
		Right,
		BottomRight,
		Bottom,
		BottomLeft,
		Left,
		TopLeft
	}

	public class Day11Airplane
	{
		public char[][] Seats { get; set; }

		public Day11Airplane(string[] rows)
		{
			Seats = new char[rows.Length][];

			for (int y = 0; y < rows.Length; y++)
			{
				var seats = rows[y].ToCharArray();
				Seats[y] = new char[seats.Length];

				for (int x = 0; x < seats.Length; x++)
				{
					Seats[y][x] = seats[x];
				}
			}
		}

		public Day11Airplane(char[][] seats)
		{
			Seats = seats;
		}

		public int NumberOfAdjecentOccupiedSeats(int x, int y, bool withJumps = false)
		{
			var occupiedPlaces = 0;

			foreach(var direction in (Direction[])Enum.GetValues(typeof(Direction)))
			{
				var seat = GetNextSeat(x, y, direction, withJumps);

				if (seat.HasValue && seat.Value == '#')
					occupiedPlaces++;
			}

			return occupiedPlaces;
		}

		public char? GetNextSeat(int x, int y, Direction direction, bool withJumps = false)
		{
			var directionData = new Dictionary<Direction, (int yDiff, int xDiff)>
			{
				{ Direction.Top, (-1, 0) },
				{ Direction.TopRight, (-1, 1) },
				{ Direction.Right, (0, 1) },
				{ Direction.BottomRight, (1, 1) },
				{ Direction.Bottom, (1, 0) },
				{ Direction.BottomLeft, (1, -1) },
				{ Direction.Left, (0, -1) },
				{ Direction.TopLeft, (-1, -1) }
			};

			var directionDiff = directionData[direction];

			var (X, Y) = (directionDiff.xDiff + x, directionDiff.yDiff + y);

			while(true)
			{
				if (Y < 0 || Y >= Seats.Length)
					return null;

				if (X < 0 || X >= Seats[Y].Length)
					return null;

				if (!withJumps)
					return Seats[Y][X];

				if (Seats[Y][X] == '#')
					return '#';

				if (Seats[Y][X] == 'L')
					return null;

				X += directionDiff.xDiff;
				Y += directionDiff.yDiff;
			}
		}

		public (int changes, char[][] seats) Excercise1()
		{
			var newMap = GetCopy();
			var changes = 0;
			for (int y = 0; y < Seats.Length; y++)
			{
				for (int x = 0; x < Seats[y].Length; x++)
				{
					var seat = Seats[y][x];
					switch (seat)
					{
						case 'L':
							if (NumberOfAdjecentOccupiedSeats(x, y) == 0)
							{
								newMap[y][x] = '#';
								changes++;
							}
							break;

						case '#':
							if (NumberOfAdjecentOccupiedSeats(x, y) >= 4)
							{
								newMap[y][x] = 'L';
								changes++;
							}
							break;
					}
				}
			}

			return (
				changes,
				newMap
			);
		}

		public (int changes, char[][] seats) Excercise2()
		{
			var newMap = GetCopy();
			var changes = 0;
			for (int y = 0; y < Seats.Length; y++)
			{
				for (int x = 0; x < Seats[y].Length; x++)
				{
					var seat = Seats[y][x];
					switch (seat)
					{
						case 'L':
							if (NumberOfAdjecentOccupiedSeats(x, y, true) == 0)
							{
								newMap[y][x] = '#';
								changes++;
							}
							break;

						case '#':
							if (NumberOfAdjecentOccupiedSeats(x, y, true) >= 5)
							{
								newMap[y][x] = 'L';
								changes++;
							}
							break;
					}
				}
			}

			return (
				changes,
				newMap
			);
		}

		public char[][] GetCopy() => new Day11Airplane(Seats.Select(x => string.Join("", x)).ToArray()).Seats;

		public override string ToString()
		{
			var builder = new StringBuilder();
			foreach (var row in Seats)
			{
				builder.Append($"[{string.Join("", row)}]");
			}

			return builder.ToString();
		}

		public override bool Equals(object obj)
		{
			if (obj as Day11Airplane == null)
				return false;

			var other = obj as Day11Airplane;

			if (other.Seats.Length != Seats.Length)
				return false;

			for (int y = 0; y < Seats.Length; y++)
			{
				if (other.Seats[y].Length != Seats[y].Length)
					return false;

				for (int x = 0; x < Seats[y].Length; x++)
				{
					if (other.Seats[y][x] != Seats[y][x])
						return false;
				}
			}

			return true;
		}
	}

	public class Day11
	{
		public Day11Airplane Plane { get; set; }

		public Day11()
		{
			var rawInput = FileReader.ReadAllLines("Day11.txt");

			Plane = new Day11Airplane(rawInput);
		}
	}
}
