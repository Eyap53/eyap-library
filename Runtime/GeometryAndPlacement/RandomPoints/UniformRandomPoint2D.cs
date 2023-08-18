namespace EyapLibrary.GeometryAndPlacement.RandomPoints
{
	using UnityEngine;
	using System;

	/// <summary>
	/// Utils class to get a random point inside a 2D shape.
	/// Currently supported shapes :
	/// Annulus
	/// </summary>
	public static class UniformRandomPoint2D
	{
		/// <inheritdoc/>
		public static Vector2 GetInAnnulus(float internalRadius, float externalRadius) => GetInAnnulusWithRandomValues(internalRadius, externalRadius, Random.value, Random.value);

		/// <inheritdoc/>
		public static Vector2 GetInAnnulus(float internalRadius, float externalRadius, System.Random rng) => GetInAnnulusWithRandomValues(internalRadius, externalRadius, (float)rng.NextDouble(), (float)rng.NextDouble());

		/// <summary>
		/// Get a uniform-random point inside the cuboid shape.
		/// The shape is centered on (0, 0, 0)
		/// Use System.Random.
		/// </summary>
		/// <param name="width">The width of the cuboid.</param>
		/// <param name="length">The length of the cuboid.</param>
		/// <returns>A random point inside the shape.</returns>
		private static Vector2 GetInAnnulusWithRandomValues(float internalRadius, float externalRadius, float randomValue1, float randomValue2)
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
