using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
	/// <summary>
	/// methods to quickly read a text file into a list of string, int, or long as needed for Advent of Code puzzles
	/// 
	/// note: these do not have input validation and are not suitable for production use - intended only for my personal use in Advent of Code puzzles
	/// </summary>
	class FileReader
	{
		public static List<string> ReadLines(string fileName = "")
		{
			var inputtext = new List<string>();
			if (fileName == "")
			{
				Console.WriteLine("Input file:");
				fileName = Console.ReadLine();
			}
			while (!File.Exists(fileName))
			{
				Console.WriteLine("file not found - try again:");
				fileName = Console.ReadLine();
			}

			using (StreamReader sr = new StreamReader(fileName))
			{
				while (sr.Peek() != -1)
				{
					inputtext.Add(sr.ReadLine());
				}

			}
			return inputtext;
		}

		public static List<int> ReadLinesInt(string fileName = "")
		{
			var inputtext = new List<int>();
			if (fileName == "")
			{
				Console.WriteLine("Input file:");
				fileName = Console.ReadLine();
			}
			while (!File.Exists(fileName))
			{
				Console.WriteLine("file not found - try again:");
				fileName = Console.ReadLine();
			}

			using (StreamReader sr = new StreamReader(fileName))
			{
				while (sr.Peek() != -1)
				{
					inputtext.Add(Convert.ToInt32(sr.ReadLine()));
				}

			}
			return inputtext;
		}

		public static List<long> ReadLinesLong(string fileName = "")
		{
			var inputtext = new List<long>();
			if (fileName == "")
			{
				Console.WriteLine("Input file:");
				fileName = Console.ReadLine();
			}
			while (!File.Exists(fileName))
			{
				Console.WriteLine("file not found - try again:");
				fileName = Console.ReadLine();
			}

			using (StreamReader sr = new StreamReader(fileName))
			{
				while (sr.Peek() != -1)
				{
					inputtext.Add(Convert.ToInt64(sr.ReadLine()));
				}

			}
			return inputtext;
		}

		public static List<double> ReadLinesDouble(string fileName = "")
		{
			var inputtext = new List<double>();
			if (fileName == "")
			{
				Console.WriteLine("Input file:");
				fileName = Console.ReadLine();
			}
			while (!File.Exists(fileName))
			{
				Console.WriteLine("file not found - try again:");
				fileName = Console.ReadLine();
			}

			using (StreamReader sr = new StreamReader(fileName))
			{
				while (sr.Peek() != -1)
				{
					inputtext.Add(Convert.ToDouble(sr.ReadLine()));
				}

			}
			return inputtext;
		}
	}
}
