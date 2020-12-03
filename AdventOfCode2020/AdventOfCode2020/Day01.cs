using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
	class Day01
	{
		public static void Problem1()
		{
			var input = FileReader.ReadLines("Inputs\\day1.txt");

			HashSet<long> nums = new HashSet<long>();

			long result = -1;

			for (int i = 0; i < input.Count - 1 && result == -1; i++)
			{
				var x = Convert.ToInt64(input[i]);
				if(nums.Contains(2020 - x))
				{
					result = (2020 - x) * x;
				}
				else
				{
					nums.Add(x);
				}
			}
			Console.WriteLine(result);
		}

		public static void Problem2()
		{
			var input = FileReader.ReadLines("Inputs\\day1.txt");

			Dictionary<long, int> nums = new Dictionary<long, int>();
			long[] vals = new long[input.Count];

			long result = -1;

			for (int i = 0; i < input.Count - 1; i++)
			{
				var x = Convert.ToInt64(input[i]);
				vals[i] = x;
				nums[x] = i;
			}

			for (int i = 0; i < vals.Length - 2 && result == -1; i++)
			{
				for (int j = 0; j < vals.Length - 1 && result == -1; j++)
				{
					long x = 2020 - vals[i] - vals[j];
					if (nums.ContainsKey(x) && nums[x] != i && nums[x] != j)
					{
						result = vals[i] * vals[j] * x;
					}
				}
			}

			Console.WriteLine(result);
		}
	}
}
