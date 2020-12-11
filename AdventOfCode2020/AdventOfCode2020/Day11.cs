using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
	class Day11
	{
		public static void Problems()
		{
			var input = FileReader.ReadLines("Inputs\\day11.txt");
			
			char[,] map1 = new char[input[0].Length, input.Count];
			char[,] map2 = new char[input[0].Length, input.Count];
			
			for (int y = 0; y < input.Count; y++)
			{
				for (int x = 0; x < input[y].Length; x++)
				{
					map1[x, y] = input[y][x];
					map2[x, y] = input[y][x];
				}
			}

			bool changed1 = true;
			while (changed1 == true)
			{
				(map1, changed1) = Next(map1, 1); 
			}
			Console.WriteLine($"Part 1: {CountItems(map1,'#')}");
			

			bool changed2 = true;
			while (changed2 == true)
			{
				(map2, changed2) = Next(map2, 2);
			}
			Console.WriteLine($"Part 1: {CountItems(map2, '#')}");

		}

		public static int CountItems(char[,] map, char item)
		{
			var result = 0;
			for (int x = 0; x < map.GetLength(0); x++)
			{
				for (int y = 0; y < map.GetLength(1); y++)
				{
					if (map[x, y] == item) result++;
				}
			}
			return result;
		}
	
		public static int Find(char[,] map, int x, int y,  int dx, int dy, int part)
		{
			while (x >= 0 && y >= 0 && x < map.GetLength(0) && y < map.GetLength(1))
			{
				if (map[x, y] != '.') return map[x, y] == '#' ? 1 : 0;
				x += dx;
				y += dy;
				if (part == 1) break;
			}
			return 0;
		}

		public static (char[,] next, bool changed) Next(char[,] map, int part)
		{
			var next = new char[map.GetLength(0), map.GetLength(1)];
			bool changed = false;
			for (int x = 0; x < next.GetLength(0); x++)
			{
				for (int y = 0; y < next.GetLength(1); y++)
				{
					int count = 0;
					for (int dx = -1; dx <= 1; dx++)
					{
						for (int dy = -1; dy <= 1; dy++)
						{
							if (dx == 0 && dy == 0) continue;
							count += Find(map, x + dx, y + dy, dx, dy, part);
						}
					}

					if (map[x, y] == '#' && count >= (part == 1 ? 4 : 5))
					{
						next[x, y] = 'L';
						changed = true;
					}
					else if (map[x, y] == 'L' && count == 0)
					{
						next[x, y] = '#';
						changed = true;
					}
					else
					{
						next[x, y] = map[x, y];
					}
				}
			}
			return (next, changed);
		}
	}
}
