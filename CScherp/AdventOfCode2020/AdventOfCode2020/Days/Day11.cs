using System.Text;
namespace AdventOfCode2020.Days
{
	public class Day11
	{
		public char[][] Seats { get; set; }

		public Day11()
		{
			var rawInput = FileReader.ReadAllLines("Day11.txt");

			Seats = new char[rawInput.Length][];

			for(int y = 0; y < rawInput.Length; y++)
			{
				var seats = rawInput[y].ToCharArray();
				Seats[y] = new char[seats.Length];

				for (int x = 0; x < seats.Length; x++)
				{
					Seats[y][x] = seats[x];
				}
			}
		}

		public int NumberOfAdjecentOccupiedSeats

		public override string ToString()
		{
			var builder = new StringBuilder();
			foreach (var row in Seats)
			{
				builder.Append($"[{string.Join(",", row)}]");
			}

			return builder.ToString();
		}
	}
}
