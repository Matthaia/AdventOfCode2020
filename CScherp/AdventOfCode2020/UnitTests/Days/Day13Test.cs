using AdventOfCode2020.Days;
using Xunit;

namespace UnitTests.Days
{
	public class Day13Test
	{
		[Theory]
		[InlineData(939, "7", 945)]
		[InlineData(939, "13", 949)]
		[InlineData(939, "59", 944)]
		[InlineData(939, "31", 961)]
		[InlineData(939, "19", 950)]
		public void GetNextDepartureTest(int arrival, string id, int expected)
		{
			var bus = new Bus(id);

			var result = bus.GetNextDeprature(arrival);

			Assert.Equal(expected, result);
		}

		[Fact]
		public void Exercise1()
		{
			var day = new Day13();

			var result = day.Exercise1();

			Assert.Equal(6559, result);
		}

		[Fact]
		public void Exercise2()
		{
			var day = new Day13();

			var result = day.Exercise2();

			Assert.Equal(626670513163231, result);
		}
	}
}
