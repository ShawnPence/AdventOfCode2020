using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
	/// <summary>
	/// misc. methods that might be useful for multiple challenges or that have been useful in previous year challenges
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

	}
}
