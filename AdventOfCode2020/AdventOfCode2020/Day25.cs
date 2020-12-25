using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
	class Day25
	{
		public static void Problems()
		{
			var input = FileReader.ReadLinesLong("Inputs\\day25.txt");

			long number = 1;
			long repeat = 0;
			while(number != input[0])
			{
				number = (number * 7) % 20201227;
				repeat++;
			}
			number = 1;
			for(int i = 1; i <= repeat; i++)
			{ 
				number = (number * input[1]) % 20201227;
			}
			Console.WriteLine(number);
		}
	}
}
