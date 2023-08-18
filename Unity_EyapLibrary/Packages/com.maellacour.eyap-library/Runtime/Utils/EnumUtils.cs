namespace EyapLibrary.Utils
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public static class EnumUtils
	{
		/// <summary>
		/// Get the values of the Enum and return it in an IEnumerable
		/// </summary>
		/// <returns>The values of the enum in an IEnumerable</returns>
		public static IEnumerable<T> GetValues<T>() where T : struct, IConvertible
		{
			return Enum.GetValues(typeof(T))
					.Cast<T>();
		}

		/// <summary>
		/// Get the count of values in an Enum
		/// </summary>
		/// <returns>The count of values in an Enum</returns>
		public static int GetValuesCount<T>() where T : struct, IConvertible
		{
			return Enum.GetNames(typeof(T)).Length;
		}
	}
}
