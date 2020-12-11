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
			
			char[,] map = new char[input[0].Length + 2, input.Count + 2];
			char[,] map2 = new char[input[0].Length + 2, input.Count + 2];
			
			for (int y = 0; y < input.Count; y++)
			{
				for (int x = 0; x < input[y].Length; x++)
				{
					map[x + 1, y + 1] = input[y][x];
					map2[x + 1, y + 1] = input[y][x];
				}
			}

			bool changed = true;

			while (changed == true)
			{
				(map, changed) = Next1(map);
			}
			var result1 = 0;
			for (int x = 1; x < map.GetLength(0) - 1; x++)
			{
				for (int y = 1; y < map.GetLength(1) - 1; y++)
				{
					if (map[x, y] == '#') result1++;
				}
			}
			Console.WriteLine(result1);

			changed = true;
			while (changed == true)
			{
				(map2, changed) = Next2(map2);
			}
			var result2 = 0;
			for (int x = 1; x < map2.GetLength(0) - 1; x++)
			{
				for (int y = 1; y < map2.GetLength(1) - 1; y++)
				{
					if (map2[x, y] == '#') result2++;
				}
			}
			Console.WriteLine(result2);

		}

		public static (char[,] next,bool changed) Next1(char[,] map)
		{
			var next = new char[map.GetLength(0), map.GetLength(1)];
			bool changed = false;
			for (int x = 1; x < next.GetLength(0) - 1; x++)
			{
				for (int y = 1; y < next.GetLength(1) - 1; y++)
				{
					int count = 0;
					for (int dx = -1; dx <= 1; dx++)
					{
						for (int dy = -1; dy <= 1; dy++)
						{
							if (dx == 0 && dy == 0) continue;
							count += map[x + dx, y + dy] == '#' ? 1 : 0;
						}
					}

					if (map[x, y] == '#' && count >= 4)
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

		
		public static int find(char[,] map, int x, int y,  int dx, int dy)
		{
			while (x > 0 && y > 0 && x < map.GetLength(0) - 1 && y < map.GetLength(1))
			{
				if (map[x, y] != '.') return map[x, y] == '#' ? 1 : 0;
				x += dx;
				y += dy;
			}
			return 0;
		}

		public static (char[,] next, bool changed) Next2(char[,] map)
		{
			var next = new char[map.GetLength(0), map.GetLength(1)];
			bool changed = false;
			for (int x = 1; x < next.GetLength(0) - 1; x++)
			{
				for (int y = 1; y < next.GetLength(1) - 1; y++)
				{
					int count = 0;
					for (int dx = -1; dx <= 1; dx++)
					{
						for (int dy = -1; dy <= 1; dy++)
						{
							if (dx == 0 && dy == 0) continue;
							count += find(map, x + dx, y + dy, dx, dy);
						}
					}

					if (map[x, y] == '#' && count >= 5)
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
