using AdventOfCode2020.Days;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace UnitTests.Days
{
	public class Day11Test
	{
		[Fact]
		public void SetupCorrectly()
		{
			var day = new Day11();

			Assert.True(day.Seats.Length > 0);
		}
	}
}
