using AdventOfCode2020.Days;
using Xunit;

namespace UnitTests.Days
{
	public class Day14Test
	{
		[Fact]
		public void Exercise1Test()
		{
			var day = new Day14();

			var result = day.Exercise1();

			Assert.Equal(15919415426101, result);
		}

		[Theory]
		[InlineData(1577116)]
		[InlineData(3562548)]
		[InlineData(60801469)]
		[InlineData(974)]
		[InlineData(61211605)]
		[InlineData(662806)]
		[InlineData(120026)]
		public void ToLongTest(long value)
		{
			var memory = new MemoryValue(value);

			Assert.Equal(value, memory.ToLong());
		}

		[Fact]
		public void Exercise2_Short()
		{
			var input = @"mask = 000000000000000000000000000000X1001X
mem[42] = 100
mask = 00000000000000000000000000000000X0XX
mem[26] = 1".Split("\r\n");

			var day = new Day14(input);

			var result = day.Exercise2();

			Assert.Equal(208, result);
		}

		[Theory]
		[InlineData("0000000X0000000000000X00000000X1001X", 16)]
		[InlineData("00000000000000000000000000000000X0XX", 8)]
		[InlineData("000000000000000000000000000000X1001X", 4)]
		[InlineData("000000000000000000000000000000X10010", 2)]
		[InlineData("000000000000000000000000000000010010", 1)]
		public void GetMemoryAddressesTest(string mask, int expected)
		{
			var memoryValue = new MemoryValue(0);
			var maskValue = new MemoryValue(" = " + mask);

			var result = memoryValue.GetMemoryAddresses(maskValue);

			Assert.Equal(expected, result.Count);
		}

		[Fact]
		public void GetMemoryAddressesTest_CorrectLong()
		{
			var memoryValue = new MemoryValue(42);
			var maskValue = new MemoryValue(" = 000000000000000000000000000000X1001X");

			var result = memoryValue.GetMemoryAddresses(maskValue);

			Assert.Equal(26, result[0]);
			Assert.Equal(27, result[1]);
			Assert.Equal(58, result[2]);
			Assert.Equal(59, result[3]);
		}

		[Fact]
		public void Exercise2Test()
		{
			var day = new Day14();

			var result = day.Exercise2();

			Assert.Equal(3443997590975, result);
		}
	}
}
