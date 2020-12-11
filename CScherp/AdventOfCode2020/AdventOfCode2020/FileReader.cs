using System.IO;

namespace AdventOfCode2020
{
	public static class FileReader
	{
		private const string baseFolder = "../../../../../../Input/";

		public static string ReadFile(string fileName)
		{
			var text = File.ReadAllText($"{baseFolder}{fileName}");

			return text;
		}

		public static string[] ReadAllLines(string fileName)
		{
			var text = File.ReadAllLines($"{baseFolder}{fileName}");

			return text;
		}
	}
}
