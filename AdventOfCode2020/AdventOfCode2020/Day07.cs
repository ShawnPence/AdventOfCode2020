using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
	class Day07
	{
		public static void Problem1()
		{
			var input = FileReader.ReadLines("Inputs\\day7.txt");

			var inside = new Dictionary<string, List<string>>();
			
			foreach(var line in input)
			{
				var parts = line.Split(' ');
				var outside = parts[0] + " " + parts[1];
				if(parts[4] != "no")
				{
					for(int i = 1; 4 * i < parts.Length; i++ )
					{
						var name = parts[i * 4 + 1] + " " + parts[i * 4 + 2];
						if (!inside.ContainsKey(name)) inside[name] = new List<string>();
						inside[name].Add(outside);
					}
				}
			}

			HashSet<string> items = new HashSet<string>();
			Queue<string> q = new Queue<string>();
			q.Enqueue("shiny gold");
			while(q.Count > 0)
			{
				var item = q.Dequeue();
				if (inside.ContainsKey(item))
				{
					foreach (var i in inside[item])
					{
						if (!items.Contains(i))
						{
							items.Add(i);
							q.Enqueue(i);
						}
					}
				}
			}
			Console.WriteLine($"Part 1: {items.Count}");
		}

		public static void Problem2()
		{
			var input = FileReader.ReadLines("Inputs\\day7.txt");

			var contains = new Dictionary<string, List<(long count, string name)>>();

			foreach (var line in input)
			{

				var parts = line.Split(' ');
				var outside= parts[0] + " " + parts[1];
				if (parts[4] != "no")
				{
					for (int i = 1; 4 * i < parts.Length; i++)
					{
						var name = parts[i * 4 + 1] + " " + parts[i * 4 + 2];
						var count = Convert.ToInt64(parts[i * 4]);
						if (count > 0)
						{
							if (!contains.ContainsKey(outside)) contains[outside] = new List<(long count, string name)>();
							contains[outside].Add((count, name));
						}
					}
				}
			}

			Console.Write($"Part 2: {CountContents("shiny gold", contains) - 1}" );
			
		}

		public static long CountContents(string name, Dictionary<string, List<(long count, string name)>> contains)
		{
			if (!contains.ContainsKey(name)) return 1;
			long result = 1;
			foreach(var inside in contains[name])
			{
				result += inside.count * CountContents(inside.name, contains);
			}
			return result;
		}
	}
}
