namespace EyapLibrary.GeometryAndPlacement
{
	using UnityEngine;
	using System;

	public static class CircleHelper
	{
		/// <summary>
		/// Get evenly spaced points along a circle.
		/// </summary>
		/// <param name="radius">The radius of the circle.</param>
		/// <param name="totalPoints">Should be more or equal than 2.</param>
		/// <param name="firstPointAngle">The optional angle of the first point, in rad.
		/// If 0 (default), first point is at (radius,0)</param>
		/// <returns>An array of Vector2 representing the point coordinates.</returns>
		public static Vector2[] GetEvenlySpaced(int totalPoints, float radius = 1f, float firstPointAngle = 0f)
		{
			if (totalPoints <= 1)
			{
				throw new ArgumentException("Should be more or equal than 2.", nameof(totalPoints));
			}

			Vector2[] resultPoints = new Vector2[totalPoints];

			for (int i = 0; i < totalPoints; i++)
			{
				float theta = firstPointAngle + Mathf.PI * 2 * i / totalPoints;

				resultPoints[i].x = radius * Mathf.Cos(theta);
				resultPoints[i].y = radius * Mathf.Sin(theta);
			}
			return resultPoints;
		}
	}
}
