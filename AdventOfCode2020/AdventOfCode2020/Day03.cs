using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
	class Day03
	{
		public static void Problem1()
		{
			var inputs = FileReader.ReadLines("Inputs\\day3.txt");
			int result = 0;
			int column = 3;
			for(int i = 1; i < inputs.Count; i++)
			{
				var line = inputs[i];
				var c = line[column];
				if (c == '#') result++;
				column += 3;
				column %= line.Length;
			}
			Console.WriteLine(result);


		}
		public static void Problem2()
		{
			var inputs = FileReader.ReadLines("Inputs\\day3.txt");

			long result0 = 0; //right 1, down 1
			long result1 = 0; //right 3, down 1
			long result2 = 0; //right 5, down 1
			long result3 = 0; //right 7, down 1
			long result4 = 0; //right 1, down 2

			int column0 = 1;
			int column1 = 3;
			int column2 = 5;
			int column3 = 7;
			int column4 = 1;

			for (int i = 1; i < inputs.Count; i++)
			{
				var line = inputs[i];


				if (line[column0] == '#') result0++;
				column0 += 1;
				column0 %= line.Length;

				if (line[column1] == '#') result1++;
				column1 += 3;
				column1 %= line.Length;

				if (line[column2] == '#') result2++;
				column2 += 5;
				column2 %= line.Length;

				if (line[column3] == '#') result3++;
				column3 += 7;
				column3 %= line.Length;

				if (i % 2 == 0)
				{
					if (line[column4] == '#') result4++;
					column4 += 1;
					column4 %= line.Length;
				}

			}
			Console.WriteLine(result0 * result1 * result2 * result3 * result4 );


		}


	}
}
