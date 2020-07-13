using System;
using System.Collections.Generic;
using System.Text;

namespace ChatBotPrime.Core.Extensions
{

	public static class StringExtensions
	{
		public static bool Contains(this string source, string toCheck, StringComparison comp)
		{
			return source?.IndexOf(toCheck, comp) >= 0;
		}
	}

}
