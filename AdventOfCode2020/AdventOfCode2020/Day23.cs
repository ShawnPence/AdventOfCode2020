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
			int i2 = 1;

			Node first = new Node(part2Input[0]);
			Node last = first;
			Dictionary<int, Node> nodes = new Dictionary<int, Node>();
			nodes[part2Input[0]] = first;
			while(i2 < part2Input.Count)
			{
				var temp = new Node(part2Input[i2]);
				last.Next = temp;
				nodes[part2Input[i2]] = temp;
				last = last.Next;
				i2++;
			}
			i2 = 10;
			while(i2 <= 1000000)
			{
				var temp = new Node(i2);
				last.Next = temp;
				nodes[i2] = temp;
				last = last.Next;
				i2++;
			}
			last.Next = first;
			
			for(int i = 0; i < 10000000; i++)
			{
				first = Part2(first, nodes);
			}

			long result2 = 1;
			var node = nodes[1].Next;
			result2 *= node.Value;
			node = node.Next;
			result2 *= node.Value;

			Console.WriteLine($"Part 2: {result2}");

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

		public static Node Part2(Node first,  Dictionary<int,Node> nodes)
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

			return newFirst;

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
