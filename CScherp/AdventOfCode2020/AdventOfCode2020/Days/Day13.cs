using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020.Days
{
	public class Bus
	{
		public long ID { get; set; }

		public Bus(string id)
		{
			if (long.TryParse(id, out var result))
				ID = result;
			else
				ID = -1;
		}

		public long GetNextDeprature(long arrival)
		{
			var loops = (long)Math.Ceiling((decimal)arrival / ID);

			return loops * ID;
		}

		public bool IsValid => ID != -1;
	}

	public class Day13
	{
		public int EarliestDeparture { get; set; }
		public IList<Bus> Busses { get; set; }

		public Day13() : this(FileReader.ReadAllLines("Day13.txt")) { }

		public Day13(string[] rawInput)
		{
			EarliestDeparture = int.Parse(rawInput[0]);
			Busses = rawInput[1].Split(",").Select(x => new Bus(x)).ToList();
		}

		public long Exercise1()
		{
			Bus earliestBus = null;
			var shortestWait = long.MaxValue;

			foreach(var bus in Busses.Where(x => x.IsValid))
			{
				var wait = bus.GetNextDeprature(EarliestDeparture) - EarliestDeparture;
				if (shortestWait > wait)
				{
					earliestBus = bus;
					shortestWait = wait;
				}
			}

			return earliestBus.ID * shortestWait;
		}

		public long Exercise2()
		{
			var time = 0L;
			var increment = Busses[0].ID;
			for (var i = 1; i < Busses.Count; i++)
			{
				if (Busses[i].IsValid)
				{
					var newTime = Busses[i].ID;
					while (true)
					{
						time += increment;
						if ((time + i) % newTime == 0)
						{
							increment *= newTime;
							break;
						}
					}
				}
			}
			return time;
		}
	}
}
