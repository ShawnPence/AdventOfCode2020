using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
	class Day14
	{
		public static void Problems()
		{
			var input = FileReader.ReadLines("Inputs\\day14.txt");

			var mask = "";
			Dictionary<long, long> memory1 = new Dictionary<long, long>();
			Dictionary<long, long> memory2 = new Dictionary<long, long>();

			foreach (var line in input)
			{
				var parts = line.Split(' ');
				if(parts[0] == "mask")
				{
					mask = parts[2];
				}
				else
				{
					var i = Convert.ToInt64(parts[0][4..^1]);
					memory1[i] = Part1(Convert.ToInt64(parts[2]), mask);
					
					foreach(var i2 in Part2(i,mask))
					{
						memory2[i2] = Convert.ToInt64(parts[2]);
					}
				}
			}

			long result1 = 0;
			foreach (var value in memory1.Values) result1 += value;
			Console.WriteLine($"Part 1: {result1}");
			
			long result2 = 0;
			foreach (var value in memory2.Values) result2 += value;
			Console.WriteLine($"Part 2: {result2}");
		}

		public static long Part1(long value, string mask)
		{
			StringBuilder output = new StringBuilder();
			for(int i = mask.Length - 1; i >= 0; i--)
			{
				switch(mask[i])
				{
					case '0':
						output.Insert(0,'0');
						break;
					case '1':
						output.Insert(0, '1');
						break;
					default:
						output.Insert(0, value % 2 == 1 ? '1' : '0');
						break;
				}
				value /= 2;
			}
			return (Convert.ToInt64(output.ToString(), 2));
		}

		public static List<long> Part2(long value, string mask)
		{
			List<string> results = new List<string>() {""};
			for (int i = mask.Length - 1; i >= 0; i--)
			{
				var temp = new List<string>();
				switch (mask[i])
				{
					case '0':
						foreach(var result in results)
						{
							temp.Add((value % 2 == 0 ? "0" : "1") + result);
						}
						break;
					case '1':
						foreach (var result in results)
						{
							temp.Add('1' + result);
						}
						break;
					default:
						foreach (var result in results)
						{
							temp.Add('0' + result);
							temp.Add('1' + result);
						}
						break;
				}
				value /= 2;
				results = temp;
			}
			var output = new List<long>();
			foreach (var result in results) output.Add(Convert.ToInt64(result.ToString(), 2));

			return (output);
		}
	}
}
