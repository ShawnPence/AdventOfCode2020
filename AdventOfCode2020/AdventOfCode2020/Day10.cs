using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
	class Day10
	{
		public static void Problems()
		{
			var input = FileReader.ReadLinesLong("Inputs\\day10.txt");
			input.Sort();
			
			int distance3 = 1;
			int distance1 = 0;
			if (input[0] == 1) distance1++;
			if (input[0] == 3) distance3++;
			for (int i = 1; i < input.Count; i++)
			{
				if (input[i] - input[i - 1] == 3) distance3++;
				if (input[i] - input[i - 1] == 1) distance1++;
			}
			Console.WriteLine(distance1 * distance3);

			Console.WriteLine(options(input, 0, 0));

		}

		public static Dictionary<(int, long), long> results = new Dictionary<(int, long), long>();

		public static long options(List<long> input, int startAtIndex, long inputValue)
		{
			
			if(results.ContainsKey((startAtIndex, inputValue))) return results[(startAtIndex, inputValue)];
						
			if(input[startAtIndex] - inputValue > 3) return 0;
		
			if(startAtIndex == input.Count - 1)	return 1;

			long result = 0;

			result += options(input, startAtIndex + 1, input[startAtIndex]); 

			result += options(input, startAtIndex + 1, inputValue);

			results[(startAtIndex, inputValue)] = result;

			return result;
		}

	}
}
