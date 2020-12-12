using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
	class Day12
	{
		public static void Problems()
		{
			var input = FileReader.ReadLines("Inputs\\day12.txt");
			
			(int x, int y) currentPosition = (0, 0);
			int direction = 90;
			foreach(var line in input)
			{
				var steps = Convert.ToInt32(line.Substring(1));
				switch(line[0])
				{
					case 'N':
						currentPosition.y += steps;
						break;
					case 'S':
						currentPosition.y -= steps;
						break;
					case 'E':
						currentPosition.x += steps;
						break;
					case 'W':
						currentPosition.x -= steps;
						break;
					case 'L':
						direction += 360 - steps;
						direction %= 360;
						break;
					case 'R':
						direction += steps;
						direction %= 360;
						break;
					case 'F':
						var move = GetDirection(direction);
						currentPosition.x += move.x * steps;
						currentPosition.y += move.y * steps;
						break;
				}
			}

			var part1 = Math.Abs(currentPosition.x) + Math.Abs(currentPosition.y);
			Console.WriteLine($"Part 1: {part1}");

						
			currentPosition = (0, 0);
			(int x, int y) point = (10, 1);

			foreach (var line in input)
			{
				var steps = Convert.ToInt32(line.Substring(1));
				switch (line[0])
				{
					case 'N':
						point.y += steps;
						break;
					case 'S':
						point.y -= steps;
						break;
					case 'E':
						point.x += steps;
						break;
					case 'W':
						point.x -= steps;
						break;
					case 'L':
						point = Rotate('L', steps, point);
						break;
					case 'R':
						point = Rotate('R', steps, point);
						break;
					case 'F':
						currentPosition.x += point.x * steps;
						currentPosition.y += point.y * steps;
						break;
				}
			}
			var part2 = Math.Abs(currentPosition.x) + Math.Abs(currentPosition.y);
			Console.WriteLine($"Part 2: {part2}");
		}

		public static (int x, int y) GetDirection(int degrees)
		{
			if (degrees == 0) return (0, 1);
			if (degrees == 90) return (1, 0);
			if (degrees == 180) return (0, -1);
			return (-1, 0);
		}

		public static (int x, int y) Rotate(char turn, int degrees, (int x, int y) point)
		{
			if(degrees == 0 ) return point;
			if (degrees == 90)
			{
				if (turn == 'R') return (point.y, -point.x);
				return (-point.y, point.x);
			}
			if (degrees == 180) return (-point.x, -point.y);

			if (turn == 'R') return (-point.y, point.x);
			return (point.y, -point.x);
		}
	}
}
