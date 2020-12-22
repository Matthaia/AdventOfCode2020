using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Days
{
	public class Player
	{
		public string Name { get; set; }
		public Queue<int> Cards { get; set; }

		public Player()
		{
			Cards = new Queue<int>();
		}

		public void AddCards(params int[] cards)
		{
			foreach (var card in cards.OrderByDescending(x => x))
				Cards.Enqueue(card);
		}
	}

	public class Day22
	{
		public Player Player1 { get; set; }
		public Player Player2 { get; set; }

		public Day22() : this(FileReader.ReadAllLines("Day22.txt")) { }

		public Day22(string[] lines)
		{
			Player1 = new Player();
			Player2 = new Player();
			var index = 0;
			var line = lines[index];

			while (!string.IsNullOrWhiteSpace(line))
			{
				if (line.Contains(":"))
					Player1.Name = line.Replace(":", "");
				else
					Player1.Cards.Enqueue(int.Parse(line));

				index++;
				line = lines[index];
			}

			index++;
			line = lines[index];

			while (!string.IsNullOrWhiteSpace(line))
			{
				if (line.Contains(":"))
					Player2.Name = line.Replace(":", "");
				else
					Player2.Cards.Enqueue(int.Parse(line));

				index++;
				if (index >= lines.Length)
					break;

				line = lines[index];
			}
		}

		public int Exercise1()
		{
			var result = Solve(new Queue<int>(Player1.Cards), new Queue<int>(Player2.Cards));

			return result.Item2;
		}

		public int Exercise2()
		{
			var result = Solve(new Queue<int>(Player1.Cards), new Queue<int>(Player2.Cards), true);

			return result.Item2;
		}

		/// <summary>
		/// Solve the battle
		/// </summary>
		/// <returns>True if player 1 won, false if player 2 won. Also returns the score.</returns>
		public (bool, int) Solve(Queue<int> player1Cards, Queue<int> player2Cards, bool withSubBattles = false)
		{
			var roundsPlayed = new Dictionary<(int, int), bool>();

			while (true)
			{
				// Prevent loops.
				if (roundsPlayed.ContainsKey((player1Cards.GetSequenceHashCode(), player2Cards.GetSequenceHashCode())))
					return (true, 0);

				roundsPlayed[(player1Cards.GetSequenceHashCode(), player2Cards.GetSequenceHashCode())] = true;

				var cardPlayer1 = player1Cards.Dequeue();
				var cardPlayer2 = player2Cards.Dequeue();

				if (withSubBattles && cardPlayer1 <= player1Cards.Count && cardPlayer2 <= player2Cards.Count)
				{
					var cardsPlayer1 = new Queue<int>(new List<int>(player1Cards).Take(cardPlayer1));
					var cardsPlayer2 = new Queue<int>(new List<int>(player2Cards).Take(cardPlayer2));

					var result = Solve(cardsPlayer1, cardsPlayer2, true);

					// Player 1 won.
					if (result.Item1)
					{
						player1Cards.Enqueue(cardPlayer1);
						player1Cards.Enqueue(cardPlayer2);
					}
					// Player 2 won.
					else
					{
						player2Cards.Enqueue(cardPlayer2);
						player2Cards.Enqueue(cardPlayer1);
					}
				}
				else if (cardPlayer1 > cardPlayer2)
				{
					player1Cards.Enqueue(cardPlayer1);
					player1Cards.Enqueue(cardPlayer2);
				}
				else if (cardPlayer1 < cardPlayer2)
				{
					player2Cards.Enqueue(cardPlayer2);
					player2Cards.Enqueue(cardPlayer1);
				}
				else
				{
					// Equal cards
					throw new Exception($"Found some equal cards, player1: {cardPlayer1}, player2: {cardPlayer2}");
				}

				// Player 2 won.
				if (player1Cards.Count == 0)
				{
					var score = 0;
					var factor = player2Cards.Count;

					while (player2Cards.Count > 0)
					{
						var card = player2Cards.Dequeue();
						score += card * factor;

						factor--;
					}

					return (false, score);
				}
				// Player 1 won.
				else if (player2Cards.Count == 0)
				{
					var score = 0;
					var factor = player1Cards.Count;

					while (player1Cards.Count > 0)
					{
						var card = player1Cards.Dequeue();
						score += card * factor;

						factor--;
					}

					return (true, score);
				}
			}
		}
	}

	public static class ListExtensions
	{
		public static int GetSequenceHashCode<T>(this IEnumerable<T> sequence)
		{
			const int seed = 487;
			const int modifier = 31;

			unchecked
			{
				return sequence.Aggregate(seed, (current, item) =>
					(current * modifier) + item.GetHashCode());
			}
		}
	}
}
