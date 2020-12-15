using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
	class Day15
	{
		public static void Problems()
		{

			Console.WriteLine("Enter the puzzle input values:");
			var start = Console.ReadLine().ToLongArray();

			var numbers = new Dictionary<long, (long i1, long i2)>();
			long last = 0;
			for (int i = 0; i < start.Length; i++)
			{
				last = start[i];
				numbers[last] = (i, -1);
			}

			for (long i = start.Length; i < 30000000; i++)
			{
				if (i == 2020) Console.WriteLine($"Part 1: {last}");
				long current = 0;
				if(numbers[last].i2 != -1)
				{
					current = numbers[last].i1 - numbers[last].i2;
				}

				if(numbers.ContainsKey(current))
				{
					(long i1, long i2) temp = (i, numbers[current].i1);
					numbers[current] = temp;
				}
				else
				{
					numbers[current] = (i, -1);
				}
				
				last = current;
			}
			Console.WriteLine($"Part 2: {last}");


		}
	}
}
