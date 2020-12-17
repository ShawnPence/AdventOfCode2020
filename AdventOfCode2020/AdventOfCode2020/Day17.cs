using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
	class Day17
	{
		public static void Problems()
		{
			var input = FileReader.ReadLines("Inputs\\day17.txt");

			PuzzleData data1 = new PuzzleData
			{
				xMax = input[0].Length - 1,
				yMax = input.Count - 1
			};

			//get starting values from puzzle input
			for (int x = 0; x < input[0].Length; x++)
			{
				for(int y = 0; y < input.Count; y++)
				{
					if (input[y][x] == '#') data1.Active.Add((x, y, 0, 0));
				}
			}

			PuzzleData data2 = new PuzzleData
			{
				xMax = input[0].Length - 1,
				yMax = input.Count - 1,
				Active = new HashSet<(int x, int y, int z, int w)>(data1.Active)
			};

			for (int i = 1; i <= 6; i++)
			{
				data1 = GetNext(data1);
				data2 = GetNext(data2, true);
			}

			Console.WriteLine($"Part 1: {data1.Active.Count}");
			Console.WriteLine($"Part 2: {data2.Active.Count}");

		}

		public class PuzzleData
		{
			public int xMin = 0;
			public int xMax = 0;
			public int yMin = 0;
			public int yMax = 0;
			public int zMin = 0;
			public int zMax = 0;
			public int wMin = 0;
			public int wMax = 0;

			///<summary>list of points that are active according to puzzle instructions</summary>
			public HashSet<(int x, int y, int z, int w)> Active = new HashSet<(int x, int y, int z, int w)>();
		}
		public static int Count((int x, int y, int z, int w) point, PuzzleData current)
		{
			int result = 0;
			for (int x1 = point.x - 1; x1 <= point.x + 1; x1++)
				for (int y1 = point.y - 1; y1 <= point.y + 1; y1++)
					for (int z1 = point.z - 1; z1 <= point.z + 1; z1++)
						for (int w1 = point.w - 1; w1 <= point.w + 1; w1++)
						{
							if ((x1, y1, z1, w1) == point) continue;
							if (current.Active.Contains((x1, y1, z1, w1))) result++;
						}
			return result;
		}

		public static PuzzleData GetNext(PuzzleData current, bool part2 = false)
		{
			PuzzleData next = new PuzzleData
			{
				xMin = current.xMin - 1,
				xMax = current.xMax + 1,
				yMin = current.yMin - 1,
				yMax = current.yMax + 1,
				zMin = current.zMin - 1,
				zMax = current.zMax + 1,
				wMin = part2 ? current.wMin - 1 : 0,
				wMax = part2 ? current.wMax + 1 : 0
			};

			for (int x = next.xMin; x <= next.xMax; x++)
				for (int y = next.yMin; y <= next.yMax; y++)
					for (int z = next.zMin; z <= next.zMax; z++)
						for (int w = next.wMin; w <= next.wMax; w++)
						{
							int count = Count((x, y, z, w), current);
							if (count == 3 || (current.Active.Contains((x, y, z, w)) && count == 2))
							{
								next.Active.Add((x, y, z, w));
							}
						}

			return next;
		}




	}
}
