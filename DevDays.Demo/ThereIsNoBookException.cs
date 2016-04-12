using System;

namespace DevDays.Demo
{
	public class ThereIsNoBookException : Exception
	{
		public ThereIsNoBookException()
			: base("there is no")
		{
		}
	}
}