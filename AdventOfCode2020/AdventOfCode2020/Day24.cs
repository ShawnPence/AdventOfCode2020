using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
	class Day24
	{
		public static void Problems()
		{
			var input = FileReader.ReadLines("Inputs\\day24.txt");
			
			var tiles = new HashSet<(int x, int y)>();
			
			foreach(var value in input)
			{
				var location = Move(value);
				if(tiles.Contains(location))
				{
					tiles.Remove(location);
				}
				else
				{
					tiles.Add(location);
				}
			}

			Console.WriteLine($"Part 1: {tiles.Count}");

			int xMin = Int32.MaxValue;
			int xMax = Int32.MinValue;
			int yMin = Int32.MaxValue;
			int yMax = Int32.MinValue;

			foreach (var location in tiles)
			{
				xMin = Math.Min(xMin, location.x);
				xMax = Math.Max(xMax, location.x);
				yMin = Math.Min(yMin, location.y);
				yMax = Math.Max(yMax, location.y);
			}

			for (int i = 1; i <= 100; i++)
			{

				xMin--;
				xMax++;
				yMin--;
				yMax++;

				int xMinNext = Int32.MaxValue;
				int xMaxNext = Int32.MinValue;
				int yMinNext = Int32.MaxValue;
				int yMaxNext = Int32.MinValue;


				HashSet<(int x, int y)> next = new HashSet<(int x, int y)>();

				for (int x = xMin; x <= xMax; x++)
					for(int y = yMin; y <= yMax; y++)
					{
						var tileCount = CountTiles((x, y), tiles);
						if (tileCount == 2 || (tiles.Contains((x, y)) && tileCount == 1))
						{
							xMinNext = Math.Min(xMinNext, x);
							xMaxNext = Math.Max(xMaxNext, x);
							yMinNext = Math.Min(yMinNext, y);
							yMaxNext = Math.Max(yMaxNext, y);
							next.Add((x, y));
						}
					}
				tiles = next;
				xMin = xMinNext;
				xMax = xMaxNext;
				yMin = yMinNext;
				yMax = yMaxNext;
			}

			Console.WriteLine($"Part 2: {tiles.Count}");
		}

		public static (int x, int y) Move(string input)
		{

			//this puzzle is based on a hexagon grid
			//this solution uses coordinates with
			//alternating rows offset to make a hexagon grid (see example below)
			//
			//   Row         Column          
			//    3
			//    2     -1   0   1   2   3
			//    1       -1   0   1   2   3
			//    0     -1   0   1   2   3
			//   -1       -1   0   1   2   3

			int x = 0;
			int y = 0;

			var diagonal = false;
			foreach (var value in input)
			{
				switch (value)
				{
					case 'e':
						if(!diagonal || y % 2 == 0) x++;
						diagonal = false;
						break;
					case 'w':
						if(!diagonal || y % 2 != 0) x--;
						diagonal = false;
						break;
					case 'n':
						diagonal = true;
						y++;
						break;
					case 's':
						diagonal = true;
						y--;
						break;
				}
				
			}
			return (x, y);
		}

		public static int CountTiles((int x, int y) location, HashSet<(int x, int y)> tiles)
		{
			var result = 0;
			int offset = location.y % 2 == 0 ? -1 : 1;
			if (tiles.Contains((location.x - 1, location.y))) result++;
			if (tiles.Contains((location.x + 1, location.y))) result++;
			if (tiles.Contains((location.x, location.y + 1))) result++;
			if (tiles.Contains((location.x, location.y - 1))) result++;
			if (tiles.Contains((location.x + offset, location.y + 1))) result++;
			if (tiles.Contains((location.x + offset, location.y - 1))) result++;

			return result;
		}

	}
}
