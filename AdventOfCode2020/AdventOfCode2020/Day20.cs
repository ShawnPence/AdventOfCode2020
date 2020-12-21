using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
	class Day20
	{
		public static void Problems()
		{
			var input = FileReader.ReadLines("Inputs\\day20.txt");

			var tiles = new Dictionary<int, Tile>();

			for(int i = 0; i < 144; i++)
			{
				List<string> temp = new List<string>(input.GetRange(i * 12 + 1, 10));
				int id = Convert.ToInt32(input[i * 12][5..^1]);
				var tile = new Tile(temp, id);
				tiles[id] = tile;
			}

			var part1 = MakeSquare(tiles);
			
			long result1 = Convert.ToInt64(part1.TilesInGrid[0, 0]) * Convert.ToInt64(part1.TilesInGrid[0, 11]) * Convert.ToInt64(part1.TilesInGrid[11, 0]) * Convert.ToInt64(part1.TilesInGrid[11, 11]);
			Console.WriteLine($"Part 1: {result1}");

			long result2 = SearchGrid(part1, tiles);
			Console.WriteLine($"Part 2: {result2}");
	
		}

		public static long SearchGrid(Grid grid, Dictionary<int,Tile> tiles)
		{
			var current = grid.Print(tiles);

			var points = new List<List<int>>();
			var input = FileReader.ReadLines("Inputs\\day20_part2.txt");
			
			int max = 0;
			int pointCount = 0;
			foreach(var line in input)
			{
				List<int> temp = new List<int>();
				for(int i = 0; i < line.Length; i++)
				{
					if (line[i] == '#')
					{
						temp.Add(i);
						max = Math.Max(max, i);
						pointCount++;
					}
				}
				points.Add(temp);
			}

			var rotate = 0;
			var flip = 0;
			while(rotate < 4 || flip < 1)
			{
				if (rotate == 4) rotate = 0;
				var found = 0;

				//search through the grid for the pattern of points from the input for part 2
				for(int row = 0; row < current.Count - points.Count + 1; row++)
				{
					for(int column = 0; column < current[0].Length - max; column++ )
					{
						bool isValid = true;
						for(int i = 0; i < points.Count && isValid; i++)
						{
							foreach(var offset in points[i])
							{ 
								if (current[row + i][column + offset] != '#')
								{
									isValid = false;
									break;
								}
							}
							
						}
						if (isValid) found++;
						
					}
				}

				if(found > 0)
				{
					var count = 0;
					foreach (var line in current)
						foreach (var item in line)
							if (item == '#') count++;

					count -= pointCount * found;
					return count;
				}



				//if the pattern of points was not found, rotate the grid 90 degrees right and try again
				{
					List<string> next = new List<string>();
					for (int column = 0; column < current[0].Length; column++)
					{
						var temp = new StringBuilder();
						for(int row = current.Count - 1; row >= 0; row--)
						{
							temp.Append(current[row][column]);
						}
						next.Add(temp.ToString());
					}
					current = next;
					rotate++;

				}

				//if the grid has been rotated 4 times (to original position), flip the grid and try again
				if(rotate == 4)
				{
					rotate = 0;

					List<string> next = new List<string>();
					for(int i = current.Count - 1; i >= 0; i--)
					{
						next.Add(current[i]);
					}
					current = next;
					flip++;
				}

			}
			return 0;
		}




		public static Grid MakeSquare(Dictionary<int,Tile> tiles)
		{
			var start = new Grid();
			Stack<Grid> stack = new Stack<Grid>();
			start.LastTilePlaced = (0, -1);
			stack.Push(start);
			while(stack.Count > 0)
			{
				var current = stack.Pop();
				var next = current.LastTilePlaced;
				if(next.row == 11)
				{
					next.row = 0;
					next.column++;
				}
				else
				{
					next.row++;
				}

				var leftNeeded = -1;
				if (next.column != 0) leftNeeded = tiles[current.TilesInGrid[next.column-1, next.row]].Rotation(current.TileRotation[next.column-1, next.row]).right;
				var upNeeded = -1;
				if (next.row != 0) upNeeded = tiles[current.TilesInGrid[next.column, next.row - 1]].Rotation(current.TileRotation[next.column, next.row - 1]).down;


				foreach (var tile in tiles.Values)
				{
					if (!current.TilesPlaced.Contains(tile.Id))
					{
						//determine if any rotation of the tile will work for the next open position in the grid
						for(int r = 0; r < 8; r++)
						{
							var edges = tile.Rotation(r);
							if((leftNeeded == -1 || edges.left == leftNeeded) && (upNeeded == -1 || edges.up == upNeeded))
							{
								var nextGrid = new Grid(current);
								nextGrid.TilesInGrid[next.column, next.row] = tile.Id;
								nextGrid.TileRotation[next.column, next.row] = r;
								nextGrid.TilesPlaced.Add(tile.Id);
								nextGrid.LastTilePlaced = (next.column, next.row);

								//if this tile completes the puzzle, return the completed grid
								if (next.column == 11 && next.row == 11) 
									return nextGrid;


								stack.Push(nextGrid);
							}

						}
					}
				}

			}
			return start;
		}


		public static void SaveGridToFile(Grid grid, Dictionary<int,Tile> tiles, string fileName, bool fullGrid = false)
		{
			using (System.IO.StreamWriter output = new System.IO.StreamWriter(fileName))
			{
				var lines = fullGrid ? grid.PrintWithBorders(tiles) : grid.Print(tiles);
				foreach (var line in lines)
				{
					output.WriteLine(line);
				}
			}
		}

		public class Grid
		{
			public (int column, int row) LastTilePlaced = (0, 0);
			public HashSet<int> TilesPlaced = new HashSet<int>();
			public int[,] TilesInGrid = new int[12, 12];
			public int[,] TileRotation = new int[12, 12];

			public Grid(Grid original)
			{
				LastTilePlaced = (original.LastTilePlaced.column, original.LastTilePlaced.row);
				TilesPlaced = new HashSet<int>(original.TilesPlaced);
				for(int row = 0; row < 12; row++)
					for(int column = 0; column < 12; column++)
					{
						TilesInGrid[column,row] = original.TilesInGrid[column,row];
						TileRotation[column, row] = original.TileRotation[column, row];
					}
			}

			public Grid()
			{

			}

			public List<string> PrintWithBorders(Dictionary<int,Tile> tiles)
			{
				List<string> output = new List<string>();
				for(int gridRow = 0; gridRow < 12; gridRow++)
				{
					for(int tileRow = 0; tileRow < 10; tileRow++)
					{
						StringBuilder line = new StringBuilder();
						for(int gridColumn = 0; gridColumn < 12; gridColumn++)
						{
							if (TilesInGrid[gridColumn, gridRow] != 0)
							{
								line.Append(tiles[TilesInGrid[gridColumn, gridRow]].PrintTile(TileRotation[gridColumn, gridRow])[tileRow]);
								line.Append(' ');
							}

						}
						output.Add(line.ToString());
					}

					output.Add(" ");

				}
				return output;
			}

			public List<string> Print(Dictionary<int, Tile> tiles)
			{
				List<string> output = new List<string>();
				for (int gridRow = 0; gridRow < 12; gridRow++)
				{
					for (int tileRow = 1; tileRow < 9; tileRow++)
					{
						StringBuilder line = new StringBuilder();
						for (int gridColumn = 0; gridColumn < 12; gridColumn++)
						{
							if (TilesInGrid[gridColumn, gridRow] != 0)
							{
								line.Append(tiles[TilesInGrid[gridColumn, gridRow]].PrintTile(TileRotation[gridColumn, gridRow])[tileRow][1..^1]);
							}

						}
						output.Add(line.ToString());
					}

				}
				return output;
			}
		}

		public class Tile
		{
			public List<string> Data = new List<string>();

			public readonly int Id = 0;
			
			readonly int left = 0;
			readonly int right= 0;
			readonly int down = 0;
			readonly int up = 0;

			readonly int leftReverse = 0;
			readonly int rightReverse = 0;
			readonly int upReverse = 0;
			readonly int downReverse = 0;

			public Tile(List<string> data, int id)
			{
				Id = id;
				Data = data;
				for (int i = 0; i < 10; i++)
				{
					left *= 2;
					if (data[i][0] == '#') left++;

					right *= 2;
					if (data[i][9] == '#') right++;

					up *= 2;
					if (data[0][i] == '#') up++;

					down *= 2;
					if (data[9][i] == '#') down++;


				}

				for (int i = 0; i < 10; i++)
				{
					var value = 1 << i;
					leftReverse *= 2;
					if ((value & left) > 0) leftReverse++;

					rightReverse *= 2;
					if ((value & right) > 0) rightReverse++;

					upReverse *= 2;
					if ((value & up) > 0) upReverse++;

					downReverse *= 2;
					if ((value & down) > 0) downReverse++;

				}

			}

			public (int up, int down, int left, int right) Rotation(int value)
			{
				return value switch
				{
					0 => (up, down, left, right),
					1 => (leftReverse, rightReverse, down, up),//rotate right
					2 => (downReverse, upReverse, rightReverse, leftReverse),//rotate 180
					3 => (right, left, upReverse, downReverse),//rotate left
					4 => (upReverse, downReverse, right, left),//flip horizontal
					5 => (rightReverse, leftReverse, downReverse, upReverse),//flip horizontal, rotate right
					6 => (down, up, leftReverse, rightReverse),//flip vertical
					7 => (left, right, up, down),//flip horizontal, rotate left
					_ => (up, down, left, right),
				};
			}

			public List<string> PrintTile(int rotation)
			{
				switch (rotation)
				{
					case 0:
						return Data;
					case 1:
						{
							//rotate right
							var result = new List<string>();
							for (int column = 0; column <= 9; column++)
							{
								StringBuilder line = new StringBuilder();
								for (int row = 9; row >= 0; row--)
								{
									line.Append(Data[row][column]);
								}
								result.Add(line.ToString());
							}
							return result;
						}
					case 2:
						{
							//rotate 180
							var result = new List<string>();
							for (int row = 9; row >= 0; row--)
							{
								StringBuilder line = new StringBuilder();
								for (int column = 9; column >= 0; column--)
								{
									line.Append(Data[row][column]);
								}
								result.Add(line.ToString());
							}
							return result;
						}
					case 3:
						//rotate left
						{
							var result = new List<string>();
							for (int column = 9; column >= 0; column--)
							{
								StringBuilder line = new StringBuilder();
								for (int row = 0; row <= 9; row++)
								{
									line.Append(Data[row][column]);
								}
								result.Add(line.ToString());
							}
							return result;
						}
					case 4:
						//flip horizontal
						{
							var result = new List<string>();
							for (int row = 0; row <= 9; row++)
							{
								StringBuilder line = new StringBuilder();
								for (int column = 9; column >= 0; column--)
								{
									line.Append(Data[row][column]);
								}
								result.Add(line.ToString());
							}
							return result;
						}
					case 5:
						//flip horizontal, rotate right
						{
							var result = new List<string>();
							for (int column = 9; column >= 0; column--)
							{
								StringBuilder line = new StringBuilder();
								for (int row = 9; row >= 0; row--)
								{
									line.Append(Data[row][column]);
								}
								result.Add(line.ToString());
							}
							return result;
						}
					case 6:
						//flip vertical
						{
							var result = new List<string>();
							for (int row = 9; row >= 0; row--)
							{
								StringBuilder line = new StringBuilder();
								for (int column = 0; column <= 9; column++)
								{
									line.Append(Data[row][column]);
								}
								result.Add(line.ToString());
							}
							return result;
						}
					case 7:
						//flip horizontal, rotate left
						{
							var result = new List<string>();
							for (int column = 0; column <= 9; column++)
							{
								StringBuilder line = new StringBuilder();
								for (int row = 0; row <= 9; row++)
								{
									line.Append(Data[row][column]);
								}
								result.Add(line.ToString());
							}
							return result;
						}
					default:
						return Data;
				}
			}
		}
	}
}
