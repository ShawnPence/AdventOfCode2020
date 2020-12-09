using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
	class Day09
	{
		public static void Problems()
		{
			var input = FileReader.ReadLinesLong("Inputs\\day9.txt");
			
			for(int i = 25; i < input.Count; i++)
			{
				if(!AddTwo(input,i,input[i]))
				{
					Console.WriteLine($"Part 1: {input[i]}");
					var result2 = FindSum(input, input[i]);
					Console.WriteLine($"Part 2: {result2}");
					break;
				}
			}
		}

		public static bool AddTwo(List<long> x, int end, long value)
		{
			HashSet<long> values = new HashSet<long>();
			for(int i = end - 25; i < end; i++)
			{
				if (values.Contains(value - x[i])) return true;
				values.Add(x[i]);
			}
			return false;
		}

		public static long FindSum(List<long> input, long value)
		{
			int start = 0;
			int end = -1;
			long sum = input[0];
			for(int i = 1; i < input.Count; i++)
			{
				sum += input[i];
				if(sum == value)
				{
					end = i;
					i = input.Count + 1;
				}
				else if(sum > value)
				{
					while(sum > value && start < i)
					{
						sum -= input[start];
						start++;
					}
				}
			}

			long min = Int64.MaxValue;
			long max = Int64.MinValue;
			for(int i = start; i <= end; i++)
			{
				min = Math.Min(min, input[i]);
				max = Math.Max(max, input[i]);
			}
			return min + max;
		}
	}
}
