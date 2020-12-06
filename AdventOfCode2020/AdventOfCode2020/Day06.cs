using System;
using System.Collections.Generic;


namespace AdventOfCode2020
{
	class Day06
	{
		public static void Problems()
		{
			var input = FileReader.ReadLines("Inputs\\day6.txt");
			input.Add("");

			int result1 = 0;
			HashSet<char> values = new HashSet<char>();

			int result2 = 0;
			var values2 = new int[26];
			int expected = 0;

			foreach(var line in input)
			{
				if(line != "")
				{
					expected++;
					foreach (var c in line)
					{
						values.Add(c);
						values2[c - 'a']++;
					}
				}
				else
				{
					result1 += values.Count;
					foreach(var value in values2)
					{
						if (value == expected) result2++;
					}

					values = new HashSet<char>();

					expected = 0;
					values2 = new int[26];
				}

			}

			Console.WriteLine($"Part 1: {result1}");
			Console.WriteLine($"Part 2: {result2}");

		}
	}
}
