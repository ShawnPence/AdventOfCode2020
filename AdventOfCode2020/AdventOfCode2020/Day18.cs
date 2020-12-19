using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
	class Day18
	{
		public static void Problems()
		{
			var input = FileReader.ReadLines("Inputs\\day18.txt");

			long result1 = 0;
			for (int i = 0; i < input.Count; i++)
			{

				long value = 0;
				char operation = '+';
				Stack<(long value,char operation)> stack = new Stack<(long value, char operation)>();
				foreach (var c in input[i])
				{
					switch(c)
					{
						case '*':
						case '+':
							operation = c;
							break;
						case '(':
							stack.Push((value, operation));
							value = 0;
							operation = '+';
							break;
						case ')':
							var temp = stack.Pop();
							if (temp.operation == '+') value += temp.value;
							if (temp.operation == '*') value *= temp.value;
							break;
						case ' ':
							break;
						default:
							if (operation == '+') value += c - '0';
							if (operation == '*') value *= c - '0';
							break;

					}
				}

				result1 += value;
			}


			Console.WriteLine($"Part 1: {result1}");

			long result2 = 0;
			long result3 = 0;

			//both of the methods below correctly solve part 2
			//I kept both versions of my solution to compare their performance

			Stopwatch stopwatch2 = new Stopwatch();
			stopwatch2.Start();
			for(int i = 0; i < input.Count; i++) result2 += Part2(input[i]);
			stopwatch2.Stop();

			Stopwatch stopwatch3 = new Stopwatch();
			stopwatch3.Start();
			for (int i = 0; i < input.Count; i++) result3 += Part2_Stack(input[i]);
			stopwatch3.Stop();



			Console.WriteLine($"Part 2: {result2} completed in {stopwatch2.ElapsedMilliseconds} ms");
			Console.WriteLine($"Part 2 (using stack): {result3} completed in {stopwatch3.ElapsedMilliseconds} ms");
		}

		public static long Part2_Stack(string input)
		{
			input = input.Replace(" ", "");
			long value = 1;
			char operation = '*';
			Stack<(long value, char operation)> stack = new Stack<(long value, char operation)>();
			foreach (var c in input)
			{
				switch (c)
				{
					case '*':
						operation = c;
						stack.Push((value,c));
						value = 1;
						break;
					case '+':
						operation = c;
						break;
					case '(':
						stack.Push((value, operation));
						stack.Push((value, c));
						value = 1;
						operation = '*';
						break;
					case ')':
						while(stack.Peek().operation != '(')
						{
							value *= stack.Pop().value;
						}
						stack.Pop();
						var temp = stack.Pop();
						if (temp.operation == '+') value += temp.value;
						if (temp.operation == '*') value *= temp.value;
						break;
					case ' ':
						break;
					default:
						if (operation == '+') value += c - '0';
						if (operation == '*') value *= c - '0';
						break;

				}
			}
			while (stack.Count > 0) value *= stack.Pop().value;
			return value;

		}


		//This was my original solution to part 2.  I created the solution above after 
		//finishing the puzzle, and I kept both solutions to compare the performance of the two
		public static long Part2(string input)
		{
			while(input.Contains('('))
			{
				var l = input.IndexOf('(');
				int count = 0;
				var r = l + 1;
				for(int i = l + 1; i < input.Length; i++)
				{
					if(input[i] == ')' && count == 0)
					{
						r = i;
						break;
					}
					else if(input[i] == ')')
					{
						count--;
					}
					else if(input[i] == '(')
					{
						count++;
					}
				}
				input = input.Substring(0, l) + Part2(input.Substring(l+ 1, r - l - 1)) + input.Substring(r + 1);
			}

			var multiply = input.Split('*');
			long result = 1;
			foreach(var value in multiply)
			{
				if(value.Contains('+'))
				{
					long temp = 0;
					foreach (var value2 in value.Split('+')) temp += Convert.ToInt64(value2);
					result *= temp;
				}
				else
				{
					result *= Convert.ToInt64(value);
				}
			}
			return result;
		}
	}
}
