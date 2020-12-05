using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
	static class Day05
	{
		public static void Problems()
		{
			var inputs = FileReader.ReadLines("Inputs\\day5.txt");

			long result1 = 0;

			List<int> values = new List<int>();

			foreach (var line in inputs)
			{
				(int min, int max) rows = (0, 127);
				(int min, int max) columns = (0, 7);
				foreach(char c in line)
				{
					switch(c)
					{
						case 'B':
							rows = upperHalf(rows);
							break;
						case 'F':
							rows = lowerHalf(rows);
							break;
						case 'L':
							columns = lowerHalf(columns);
							break;
						case 'R':
							columns = upperHalf(columns);
							break;
					}
					
				}
				result1 = Math.Max(result1, rows.min * 8 + columns.min);
				values.Add(rows.min * 8 + columns.min);
			}

			//Part 1 answer:
			Console.WriteLine(result1);

			//Part 2 answer:
			values.Sort();
			for(int i = 1; i < values.Count; i++)
			{
				if (values[i] > values[i - 1] + 1)
				{
					Console.WriteLine(values[i] - 1);
					break;
				}
			}

		}

		public static (int min,int max) upperHalf((int min,int max) x)
		{
			int remove = (x.max - x.min + 1) / 2;
			return (x.min + remove, x.max);
		}
		public static (int min, int max) lowerHalf((int min, int max) x)
		{
			int remove = (x.max - x.min + 1) / 2;
			return (x.min, x.max - remove);
		}
	}
}
