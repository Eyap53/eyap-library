namespace EyapLibrary.GeometryAndPlacement.RandomPoints
{
	using System;
	using UnityEngine;
	using Random = UnityEngine.Random;

	/// <summary>
	/// Utils class to get a random point inside a 2D shape.
	/// Currently supported shapes :
	/// Annulus
	/// </summary>
	public static class UniformRandomPoint2D
	{
		/// <inheritdoc cref="GetInAnnulus(float, float, float, float)"/>
		public static Vector2 GetInAnnulus(float internalRadius, float externalRadius) => GetInAnnulus(internalRadius, externalRadius, Random.value, Random.value);

		/// <param name="rng">The random number generator to use.</param>
		/// <inheritdoc cref="GetInAnnulus(float, float, float, float)"/>
		public static Vector2 GetInAnnulus(float internalRadius, float externalRadius, System.Random rng) => GetInAnnulus(internalRadius, externalRadius, (float)rng.NextDouble(), (float)rng.NextDouble());

		/// <summary>
		/// Get a uniform-random point inside the annulus (2D) shape.
		/// The shape is centered on (0, 0).
		/// </summary>
		/// <param name="width">The width of the cuboid.</param>
		/// <param name="length">The length of the cuboid.</param>
		/// <param name="randomValue1">The first random value to use.</param>
		/// <param name="randomValue2">The second random value to use.</param>
		/// <returns>A random point inside the shape.</returns>
		public static Vector2 GetInAnnulus(float internalRadius, float externalRadius, float randomValue1, float randomValue2)
		{
			// Arguments Verifications
			if (externalRadius <= 0)
			{
				throw new ArgumentException("The external radius needs to be strictly positive.", "externalRadius");
			}
			if (internalRadius <= 0)
			{
				throw new ArgumentException("The internal radius needs to be strictly positive.", "internal");
			}
			if (internalRadius >= externalRadius)
			{
				throw new ArgumentException("The internal radius needs to be strictly greater than the external radius.", "internal");
			}

			// Implementation
			float sqrExternalRadius = externalRadius * externalRadius;
			float sqrInternalRadius = internalRadius * internalRadius;

			float r = Mathf.Pow(randomValue1 * (sqrExternalRadius - sqrInternalRadius) + sqrInternalRadius, 1 / 2f);
			float theta = randomValue2 * 2.0f * Mathf.PI;

			Vector2 point = new(r * Mathf.Cos(theta), r * Mathf.Sin(theta));

			return point;
		}
	}
}
