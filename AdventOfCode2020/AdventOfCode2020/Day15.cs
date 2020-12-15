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

			var numbers = new Dictionary<long, long>();
			long next = start[0];
			for(int i = 1; i <= 30000000; i++)
			{
				if(i == 2020 || i == 30000000) Console.WriteLine($"Part {(i == 2020 ? 1 : 2)}: {next}");
				long temp = 0;
				if(numbers.ContainsKey(next))
				{
					temp = i - numbers[next] - 1;
				}
				numbers[next] = i - 1;
				next = temp;
				if (i < start.Length) next = start[i];
			}
		}
	}
}
