using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
	class Day16
	{
		public static void Problems()
		{
			var input = FileReader.ReadLines("Inputs\\day16.txt");

			var fieldList = new Dictionary<string, (long min1, long max1, long min2, long max2)>();

			int rowNumber = 0;
			while(rowNumber < input.Count && !(input[rowNumber] == ""))
			{
				var parts = input[rowNumber].Split(':');
				var temp = parts[1].Replace("or", "-");
				var r = temp.ToLongArray('-');
				fieldList.Add(parts[0], (r[0], r[1],r[2],r[3]));
				rowNumber++;
			}
			rowNumber += 2;

			var t = input[rowNumber].ToLongArray();

			rowNumber += 3;
			List<long[]> rows = new List<long[]>();
			while(rowNumber < input.Count)
			{
				rows.Add(input[rowNumber].ToLongArray());
				rowNumber++;
			}

			long result1 = 0;
			HashSet<int> invalid = new HashSet<int>();
			for (int i = 0; i < rows.Count; i++)
			{
				var values = rows[i];
				foreach (var value in values)
				{
					bool valid = false;
					foreach (var r in fieldList.Values)
					{
						if ((value <= r.max1 && value >= r.min1) || (value <= r.max2 && value >= r.min2)) valid = true;
					}

					if (!valid)
					{
						result1 += value;
						invalid.Add(i);
					}

				}
			}

			Console.WriteLine($"Part 1: {result1}");



			Dictionary<int, List<string>> fields = new Dictionary<int, List<string>>();
			for(int field = 0; field < rows[0].Length; field++)
			{
				fields[field] = new List<string>();
				foreach(var n in fieldList.Keys)
				{
					fields[field].Add(n);

				}
				for(int i = 0; i < rows.Count; i++)
				{
					if (invalid.Contains(i)) continue;
					var temp = new List<string>();
					var value = rows[i][field];
					foreach(var n in fields[field])
					{
						var r = fieldList[n];
						if ((value <= r.max1 && value >= r.min1) || (value <= r.max2 && value >= r.min2)) temp.Add(n);
					}
					fields[field] = temp;
				}
			}

			ReduceList(fields);

			long result2 = 1;
			foreach(var field in fields.Keys)
			{
				if (fields[field][0].StartsWith("departure")) result2 *= t[field];
			}



			Console.WriteLine($"Part 1: {result2}");
		}

		public static void ReduceList(Dictionary<int, List<string>> fields)
		{
			var f = new HashSet<string>();
			var count = 1;
			while(count > 0)
			{
				count = 0;
				foreach(var field in fields.Keys)
				{
					if (fields[field].Count > 1)
					{
						count++;
						var temp = new List<string>();
						foreach (var n in fields[field])
						{
							if (!f.Contains(n)) temp.Add(n);

						}
						fields[field] = temp;
					}
					else
					{
						f.Add(fields[field][0]);
					}

				}
			}
		}
	}
}
