using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
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
	}
}
