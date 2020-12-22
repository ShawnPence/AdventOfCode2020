using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
	class Day22
	{
		public static void Problems()
		{
			var input = FileReader.ReadLines("Inputs\\day22.txt");

			List<int> player1 = new List<int>();
			List<int> player2 = new List<int>();

			int lineNumber = 1;
			for (; lineNumber < input.Count; lineNumber++)
			{
				if (input[lineNumber] == "") break;
				player1.Add(Convert.ToInt32(input[lineNumber]));
			}
			lineNumber += 2;
			for (; lineNumber < input.Count; lineNumber++)
			{
				if (input[lineNumber] == "") break;
				player2.Add(Convert.ToInt32(input[lineNumber]));
			}

			//part 1
			
			List<int> winList = Part1(player1, player2);

			long result1 = 0;
	
			for(int i = 0; i < winList.Count; i++)
			{
				result1 += winList[i] * (winList.Count - i);
			}

			Console.WriteLine($"Part 1: {result1}");

			//part 2

			(winList, _) = Part2(player1, player2);

			long result2 = 0;

			for (int i = 0; i < winList.Count; i++)
			{
				result2 += winList[i] * (winList.Count - i);
			}

			Console.WriteLine($"Part 2: {result2}");
		}

		public static List<int> Part1(List<int> player1, List<int> player2)
		{
			Queue<int> player1Part1 = new Queue<int>(player1);
			Queue<int> player2Part1 = new Queue<int>(player2);

			while (player1Part1.Count > 0 && player2Part1.Count > 0)
			{
				var player1Card = player1Part1.Dequeue();
				var player2Card = player2Part1.Dequeue();

				if (player1Card > player2Card)
				{
					player1Part1.Enqueue(player1Card);
					player1Part1.Enqueue(player2Card);
				}
				else
				{
					player2Part1.Enqueue(player2Card);
					player2Part1.Enqueue(player1Card);
				}
			}

			return player1Part1.Count > 0 ? player1Part1.ToList() : player2Part1.ToList();
		}

		public static (List<int> winList, int winNumber) Part2(List<int> player1, List<int> player2)
		{
			var previous = new List<(List<int> player1, List<int> player2)>();

			while (player1.Count > 0 && player2.Count > 0)
			{
				foreach (var prevousCards in previous)
				{
					if (CompareLists(prevousCards.player1, player1) && CompareLists(prevousCards.player2, player2))
					{
						return (player1, 1);
					}
				}
				previous.Add((player1, player2));

				var player1Next = player1.Count > 1 ? new List<int>(player1.GetRange(1, player1.Count - 1)) : new List<int>();
				var player2Next = player2.Count > 1 ? new List<int>(player2.GetRange(1, player2.Count - 1)) : new List<int>();
				var player1Card = player1[0];
				var player2Card = player2[0];

				int winNumber;

				if (player1Card <= player1Next.Count && player2Card <= player2Next.Count)
				{
					(_, winNumber) = Part2(player1Next.GetRange(0,player1Card), player2Next.GetRange(0,player2Card));
				}
				else if (player1Card > player2Card)
				{
					winNumber = 1;	
				}
				else
				{
					winNumber = 2;
				}


				if (winNumber == 1)
				{
					player1Next.Add(player1Card);
					player1Next.Add(player2Card);
				}
				else
				{
					player2Next.Add(player2Card);
					player2Next.Add(player1Card);
				}

				player1 = player1Next;
				player2 = player2Next;
			}

			return player1.Count > 0 ? (player1, 1) : (player2, 2);
		}

		public static bool CompareLists(List<int> list1, List<int> list2)
		{
			if (list1.Count != list2.Count) return false;
			for (int i = 0; i < list1.Count; i++)
			{
				if (list1[i] != list2[i]) return false;
			}
			return true;
		}
	}
}
