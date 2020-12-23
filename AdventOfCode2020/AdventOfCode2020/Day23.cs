using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
	class Day23
	{
		public static void Problems()
		{
			//puzzle input number from Advent of Code day 23 puzzle
			Console.WriteLine("Day 23 puzzle starting value: ");
			var input = Console.ReadLine();


			List<int> part1Input = new List<int>();
			
			for(int i = 0; i < input.Length; i++)
			{
				part1Input.Add(input[i] - '0');
			}
			
			List<int> part2Input = new List<int>(part1Input);

			//Part 1
			for (int i = 1; i <= 100; i++) Part1(part1Input);

			int i1 = part1Input.IndexOf(1);
			if(i1 != part1Input.Count - 1)
			{
				var temp = part1Input.GetRange(i1 + 1, part1Input.Count - (i1 + 1));
				part1Input.RemoveRange(i1, part1Input.Count - i1);
				part1Input.InsertRange(0, temp);
			}

			long result1 = 0;
			foreach (var value in part1Input)
			{
				result1 *= 10;
				result1 += value;
			}
			
			Console.WriteLine($"Part 1: {result1}");


			//Part 2

			//after solving the puzzle, I realized I could create a better solution.
			//both of my solutions appear below for performance comparison purposes

			Stopwatch stopwatch1 = new Stopwatch();
			stopwatch1.Start();
			long result2Improved = Part2Improved(part2Input);
			stopwatch1.Stop();
			Console.WriteLine($"Part 2 (using improved solution): {result2Improved} completed in {stopwatch1.ElapsedMilliseconds} ms");



			Stopwatch stopwatch2 = new Stopwatch();
			stopwatch2.Start();
			long result2 = Part2Original(part2Input);
			stopwatch2.Stop();

			Console.WriteLine($"Part 2: {result2} completed in {stopwatch2.ElapsedMilliseconds} ms");

		}

		public static void Part1(List<int> input)
		{
			int current = input[0];
			var next = current - 1;
			while (input.IndexOf(next) < 4)
			{
				next--;
				if (next < 1) next = 9;
			}
			var i = input.IndexOf(next);
			if (i == input.Count - 1)
			{
				input.AddRange(input.GetRange(1,3));
			}
			else
			{
				input.InsertRange(i + 1, input.GetRange(1,3));
			}
			input.Add(current);
			input.RemoveRange(0, 4);
		}

		public static long Part2Improved(List<int> input)
		{

			int[] numbers = new int[1000001];
			int current = input[0];
			for (int i = 0; i < input.Count - 1; i++)
			{
				numbers[input[i]] = input[i + 1];
			}
			numbers[input[input.Count - 1]] = 10;
			for (int i = 10; i < 1000000; i++)
			{
				numbers[i] = i + 1;
			}
			numbers[1000000] = current;

			for (int i = 1; i <= 10000000; i++)
			{
				int next1 = numbers[current];
				int next2 = numbers[next1];
				int next3 = numbers[next2];
				int newCurrent = numbers[next3];
				int moveAfter = current - 1;
				if (moveAfter == 0) moveAfter = 1000000;
				while (moveAfter == next1 || moveAfter == next2 || moveAfter == next3)
				{
					moveAfter--;
					if (moveAfter == 0) moveAfter = 1000000;
				}

				int remaining = numbers[moveAfter];
				numbers[current] = newCurrent;
				numbers[moveAfter] = next1;
				numbers[next3] = remaining;
				current = newCurrent;
			}

			int resultValue1 = numbers[1];
			int resultValue2 = numbers[resultValue1];
			return Convert.ToInt64(resultValue1) * Convert.ToInt64(resultValue2);
		}


		public static long Part2Original(List<int> input)
		{
			int i2 = 1;

			Node first = new Node(input[0]);
			Node last = first;
			Dictionary<int, Node> nodes = new Dictionary<int, Node>();
			nodes[input[0]] = first;
			while (i2 < input.Count)
			{
				var temp = new Node(input[i2]);
				last.Next = temp;
				nodes[input[i2]] = temp;
				last = last.Next;
				i2++;
			}
			i2 = 10;
			while (i2 <= 1000000)
			{
				var temp = new Node(i2);
				last.Next = temp;
				nodes[i2] = temp;
				last = last.Next;
				i2++;
			}
			last.Next = first;

			for (int i = 1; i <= 10000000; i++)
			{
				var node0 = first;
				var node1 = node0.Next;
				var node2 = node1.Next;
				var node3 = node2.Next;

				var moveAfter = first.Value - 1;
				if (moveAfter == 0) moveAfter = 1000000;
				while (moveAfter == node0.Value || moveAfter == node1.Value || moveAfter == node2.Value || moveAfter == node3.Value)
				{
					moveAfter--;
					if (moveAfter == 0) moveAfter = 1000000;
				}

				var newFirst = node3.Next;

				var remaining = nodes[moveAfter].Next;

				nodes[moveAfter].Next = node1;

				node3.Next = remaining;

				node0.Next = newFirst;

				first = newFirst;

			}

			long result2 = 1;
			var node = nodes[1].Next;
			result2 *= node.Value;
			node = node.Next;
			result2 *= node.Value;

			return result2;

		}

		public class Node
		{
			public readonly int Value;
			public Node Next;
			public Node(int value)
			{
				Value = value;
			}
		}

	}
}
