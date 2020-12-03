using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
	/// <summary>
	/// extension methods that may be useful for sovling puzzles quickly
	/// </summary>
	static class Extensions
	{
		public static int[] ToIntArray(this string s, char d = ',')
		{
			return s.Split(d).Select(x => Convert.ToInt32(x)).ToArray();
		}

		public static int[] ToIntArray(this string s, char[] d)
		{
			return s.Split(d).Select(x => Convert.ToInt32(x)).ToArray();
		}

		public static long[] ToLongArray(this string s, char d = ',')
		{
			return s.Split(d).Select(x => Convert.ToInt64(x)).ToArray();
		}

		public static long[] ToLongArray(this string s, char[] d)
		{
			return s.Split(d).Select(x => Convert.ToInt64(x)).ToArray();
		}
	}
}
