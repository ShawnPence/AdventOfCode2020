using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
	/// <summary>
	/// misc. methods that might be useful for multiple challenges 
	/// or that have been useful in previous Advent of Code or other puzzles
	/// 
	/// note: these do not have input validation and are not suitable for production use - intended only for my personal use in Advent of Code puzzles
	/// </summary>
	class Misc
	{
		public static int GCD(int a, int b)
		{
			if (b == 0) return a;
			return GCD(b, a % b);
		}

		public static long GCD(long a, long b)
		{
			if (b == 0) return a;
			return GCD(b, a % b);
		}

		public static int LCM(int a, int b)
		{
			return (a * b / GCD(a, b));
		}

		public static long LCM(long a, long b)
		{
			return (a * b / GCD(a, b));
		}

		/// <summary>
		/// Heron's Formula for the area of a triangle given the lengths of all three sides
		/// </summary>
		public static double HeronsFormula(double a, double b, double c)
		{
			double s = (a + b + c) / 2.0;
			return Math.Sqrt(s * (s - a) * (s - b) * (s - c));
		}


		
	}
}
