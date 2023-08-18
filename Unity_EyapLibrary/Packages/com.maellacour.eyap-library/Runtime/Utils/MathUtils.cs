namespace EyapLibrary.Utils
{
	using System;
	using System.Linq;

	public static class MathUtils
	{
		#region Max
		/// <summary>
		/// This method only exists for consistency, so you can *always* call MathExtensions.Max
		/// instead of alternating between MathExtensions.Max and Math.Max.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns>The max number between x and y.</returns>
		public static int Max(int x, int y)
		{
			return Math.Max(x, y);
		}

		public static int Max(int x, int y, int z)
		{
			return Max(Max(x, y), z);
		}

		public static int Max(int x, int y, int z, int w)
		{
			return Max(Max(x, y, z), w);
		}

		public static int Max(params int[] values)
		{
			return Enumerable.Max(values);
		}
		#endregion

		/// <summary>
		/// Clamps a value between a minimum int and maximum int value.
		/// </summary>
		/// <param name="value">The value to clamp.</param>
		/// <param name="min">The minimum value.</param>
		/// <param name="max">The maximum value, need to be more than min.</param>
		/// <returns>The clamped value.</returns>
		public static int Clamp(int value, int min, int max)
		{
			if (min > max)
			{
				throw new ArgumentOutOfRangeException("max", max, "Max is less than min.");
			}
			else if (value < min)
			{
				return min;
			}
			else if (value > max)
			{
				return max;
			}
			else
			{
				return value;
			}
		}

		/// <summary>
		/// Return the modulo of dividend % divisor, even when dividend is negative (which is not the case of native c# modulo operator)
		/// </summary>
		/// <param name="dividend">The dividend</param>
		/// <param name="divisor">The divisor</param>
		/// <returns>dividend % divisor</returns>
		public static int Modulo(int dividend, int divisor) => dividend - divisor * (int)Math.Floor(1f * dividend / divisor);
	}
}
