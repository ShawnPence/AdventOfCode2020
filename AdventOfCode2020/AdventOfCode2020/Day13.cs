using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
	class Day13
	{
		public static void Problems()
		{
			var input = FileReader.ReadLines("Inputs\\day13.txt");

			var start1 = Convert.ToInt32(input[0]);
			var values = input[1].Split(',');

			var minWait = Int32.MaxValue;
			var minID = 0;
			foreach(var number in values)
			{
				if(number != "x")
				{
					var id = Convert.ToInt32(number);
					var wait = (start1 / id + 1) * id - start1;
					if(wait < minWait)
					{
						minWait = wait;
						minID = id;
					}
				}
			}

			Console.WriteLine($"Part 1: {minWait * minID}");


			List<(long id, long offset)> values2 = new List<(long id, long offset)>(); 
			for(int i = 1; i < values.Length; i++)
			{
				if (values[i] != "x")
				{
					values2.Add((Convert.ToInt64(values[i]),i));
				}
			}

			long start2 = Convert.ToInt64(values[0]);
			var repeat = start2;
			for (int i = 0; i < values2.Count; i++)
			{
				start2 = FindPoint(start2, values2[i].id, repeat, values2[i].offset);
				repeat = Misc.LCM(repeat, values2[i].id);
			}

			Console.WriteLine($"Part 2: {start2}");
		}

		public static long FindPoint(long start, long id, long repeat, long offset)
		{
			var current = id;
			while (current != start + offset)
			{
				if (current < start + offset)
				{
					if((start + offset - current) % id == 0)
					{
						current = start + offset;
					}
					else
					{
						current += ((start + offset - current) / id + 1) * id;
					}
				}

				if (current > start + offset)
				{
					if((current - (start + offset)) % repeat == 0)
					{
						start = current - offset;
					}
					else
					{ 
						start += ((current - (start + offset)) / repeat + 1) * repeat;
					}
				}
			}
			return start;
		}
	}
}
