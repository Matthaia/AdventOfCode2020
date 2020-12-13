using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Days
{
	public enum MoveType
	{
		North,
		East,
		South,
		West,
		Left,
		Right,
		Forward
	}

	public class Move
	{
		public MoveType MoveType { get; set; }
		public int Value { get; set; }

		public Move(string input)
		{
			switch (input[0])
			{
				case 'N':
					MoveType = MoveType.North;
					break;

				case 'E':
					MoveType = MoveType.East;
					break;

				case 'S':
					MoveType = MoveType.South;
					break;

				case 'W':
					MoveType = MoveType.West;
					break;

				case 'L':
					MoveType = MoveType.Left;
					break;

				case 'R':
					MoveType = MoveType.Right;
					break;

				case 'F':
					MoveType = MoveType.Forward;
					break;
			}

			Value = int.Parse(input[1..]);
		}

		public Position ApplyMove(Position position)
		{
			switch (MoveType)
			{
				case MoveType.North:
					return position.North(Value);
				case MoveType.South:
					return position.South(Value);
				case MoveType.East:
					return position.East(Value);
				case MoveType.West:
					return position.West(Value);
				case MoveType.Left:
					{
						var rotations = Value / 90;
						var leftDirection = position.Direction;
						for (var i = 0; i < rotations; i++)
						{
							leftDirection = leftDirection.Left();
						}

						return new Position(leftDirection, position.X, position.Y);
					}
				case MoveType.Right:
					{
						var rotations = Value / 90;
						var rightDirection = position.Direction;
						for (var i = 0; i < rotations; i++)
						{
							rightDirection = rightDirection.Right();
						}

						return new Position(rightDirection, position.X, position.Y);
					}
				case MoveType.Forward:
					return position.Direction switch
					{
						Direction.North => position.North(Value),
						Direction.East => position.East(Value),
						Direction.South => position.South(Value),
						Direction.West => position.West(Value),
						_ => throw new Exception()
					};
				default:
					throw new Exception();
			}
		}

		public (Position, Position) ApplyWayPoint(Position position, Position waypoint)
		{
			switch (MoveType)
			{
				case MoveType.North:
					return (position, waypoint.North(Value));
				case MoveType.South:
					return (position, waypoint.South(Value));
				case MoveType.East:
					return (position, waypoint.East(Value));
				case MoveType.West:
					return (position, waypoint.West(Value));
				case MoveType.Left:
					{
						var rotations = Value / 90;
						var leftWaypoint = waypoint;
						for (var i = 0; i < rotations; i++)
						{
							leftWaypoint = leftWaypoint.RotAntiClockwise();
						}

						return (position, leftWaypoint);
					}
				case MoveType.Right:
					{
						var rotations = Value / 90;
						var rightWaypoint = waypoint;
						for (var i = 0; i < rotations; i++)
						{
							rightWaypoint = rightWaypoint.RotClockwise();
						}

						return (position, rightWaypoint);
					}
				case MoveType.Forward:
					return (position.Add(waypoint, Value), waypoint);
				default:
					throw new Exception();
			}
		}

		public override string ToString()
		{
			return $"{MoveType} - {Value}";
		}
	}

	public enum Direction
	{
		North,
		East,
		South,
		West,
	}

	public class Position
	{
		public Position(Direction direction, int x, int y)
		{
			Direction = direction;
			X = x;
			Y = y;
		}

		public Direction Direction { get; }
		public int X { get; }
		public int Y { get; }

		public Position North(int value) => new Position(Direction, X, Y - value);
		public Position East(int value) => new Position(Direction, X + value, Y);
		public Position South(int value) => new Position(Direction, X, Y + value);
		public Position West(int value) => new Position(Direction, X - value, Y);

		public Position Add(Position pos, int multiplier = 1) => new Position(Direction, X + pos.X * multiplier, Y + pos.Y * multiplier);

		public Position RotClockwise() => new Position(Direction, -Y, X);
		public Position RotAntiClockwise() => new Position(Direction, Y, -X);
	}

	public class Day12
	{
		public IList<Move> Moves { get; set; }

		public Day12()
		{
			var rawInput = FileReader.ReadAllLines("Day12.txt");
			Moves = rawInput.Select(x => new Move(x)).ToList();
		}

		public int Exercise1()
		{
			var position = new Position(Direction.East, 0, 0);

			foreach (var move in Moves)
			{
				position = move.ApplyMove(position);
			}

			return Math.Abs(position.X) + Math.Abs(position.Y);
		}

		public int Exercise2()
		{
			var position = new Position(Direction.East, 0, 0);
			var waypoint = new Position(Direction.North, 10, -1);

			foreach (var move in Moves)
			{
				(position, waypoint) = move.ApplyWayPoint(position, waypoint);
			}

			return Math.Abs(position.X) + Math.Abs(position.Y);
		}
	}

	public static class Extensions
	{
		public static Direction Left(this Direction direction) =>
			direction switch
			{
				Direction.North => Direction.West,
				Direction.East => Direction.North,
				Direction.South => Direction.East,
				Direction.West => Direction.South,
				_ => throw new Exception()
			};

		public static Direction Right(this Direction direction) =>
			direction switch
			{
				Direction.North => Direction.East,
				Direction.East => Direction.South,
				Direction.South => Direction.West,
				Direction.West => Direction.North,
				_ => throw new Exception()
			};
	}
}
