using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
	class Day19
	{
		public static void Problems()
		{
			var input = FileReader.ReadLines("Inputs\\day19.txt");

			long result1 = 0;
			long result2 = 0;

			int i1 = 0;
			Dictionary<int, string> rules = new Dictionary<int, string>();
			for(; i1 < input.Count; i1++ )
			{
				if(input[i1] == "")
				{
					i1++;
					break;
				}
				var parts = input[i1].Split(": ");
				rules[Convert.ToInt32(parts[0])] = parts[1];
			}


			List<string> values = new List<string>();
			for (; i1 < input.Count; i1++)
			{
				values.Add(input[i1]);

			}

			foreach (var value in values) if (possible(0, rules, value).Count > 0) result1++;
			Console.WriteLine($"Part 1 : {result1}");

			var input2 = FileReader.ReadLines("Inputs\\day19_part2.txt");
			for (i1 = 0; i1 < input2.Count; i1++)
			{
				var parts = input2[i1].Split(": ");
				rules[Convert.ToInt32(parts[0])] = parts[1];
			}

			foreach (var value in values) if (possible(0, rules, value).Count > 0) result2++;
			Console.WriteLine($"Part 2 : {result2}");

		}


		public static List<string> possible(int rule, Dictionary<int, string> rules, string input)
		{

			List<string> results = new List<string>();
			if (input.Length == 0) return results;

			var options = rules[rule].Split('|');
			if (options[0][0] == '"')
			{
				if(input[0] == options[0][1]) results.Add(options[0].Substring(1, 1));
				return results;
			}
			for (int i = 0; i < options.Length; i++)
			{
				List<string> temp1 = new List<string>() { "" };
				var parts = options[i].Split(' ');
				for (int j = 0; j < parts.Length; j++)
				{
					if (parts[j] == "") continue;
					var temp2 = new List<string>();
					foreach (var i1 in temp1)
					{
						if (i1.Length == 0 || (input.StartsWith(i1) && i1.Length < input.Length))
						{
							var temp3 = possible(Convert.ToInt32(parts[j]), rules,input.Substring(i1.Length)  );
							foreach (var i2 in temp3)
								temp2.Add(i1 + i2);
						}
					}
					temp1 = temp2;
				}
				results.AddRange(temp1);
			}
			

			if(rule == 0 && !results.Contains(input)) return new List<string>();
			
			return results;
		}
	}
}
