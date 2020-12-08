using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
	class Day08
	{
		public static void Problems()
		{
			var input = FileReader.ReadLines("Inputs\\day8.txt");
			var (_ , value1) = Test(input);
			Console.WriteLine($"Part 1: {value1}");

			for (int i = 0; i < input.Count; i++)
			{
				var instruction = input[i].Split(' ')[0];
				if (instruction == "jmp" || instruction == "nop")
				{
					var input2 = new List<string>(input);
					input2[i] = instruction == "jmp" ? input[i].Replace("jmp", "nop") : input[i].Replace("nop", "jmp");
					var (completed2 ,value2) = Test(input2);
					if (completed2)
					{
						Console.WriteLine($"Part 2: {value2}");
						break;
					}
				}
			}
		}

		public static (bool completed, int value) Test(List<string> input)
		{
			int a = 0;
			HashSet<int> e = new HashSet<int>();
			int p = 0;
			while (!e.Contains(p) && p < input.Count)
			{
				e.Add(p);
				var instruction = input[p].Split(' ')[0];
				var value = Convert.ToInt32(input[p].Split(' ')[1]);
				switch (instruction)
				{
					case "nop":
						p++;
						break;
					case "acc":
						a += value;
						p++;
						break;
					case "jmp":
						p += value;
						break;
				}
				if (e.Contains(p)) return (false,a);
			}
			return (true, a);
		}
	}
}
