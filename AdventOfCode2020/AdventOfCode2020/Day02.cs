using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
	class Day02
	{
		public static void Problem1()
		{
			var inputs = FileReader.ReadLines("Inputs\\day2.txt");
			int valid = 0;
			foreach(var input in inputs)
			{
				var parts = input.Split(" ");
				var minmax = parts[0].Split("-");
				int min = Convert.ToInt32(minmax[0]);
				int max = Convert.ToInt32(minmax[1]);
				char letterNeeded = parts[1][0];
				int count = 0;
				foreach (var c in parts[2])
				{
					if(c == letterNeeded) count++;
				}
				if (count >= min && count <= max) valid++;

			}
			Console.WriteLine(valid);
		}

		public static void Problem2()
		{
			var inputs = FileReader.ReadLines("Inputs\\day2.txt");
			int valid = 0;
			foreach (var input in inputs)
			{
				var parts = input.Split(" ");
				var indexes = parts[0].Split("-");


				int indexA = Convert.ToInt32(indexes[0]);
				int indexB = Convert.ToInt32(indexes[1]);

				char letterNeeded = parts[1][0];

				//per puzzle instructions, indexes are 1-based
				if (parts[2][indexA - 1] == letterNeeded ^ parts[2][indexB - 1] == letterNeeded) valid++;
			}
			Console.WriteLine(valid);
		}
	}
}
