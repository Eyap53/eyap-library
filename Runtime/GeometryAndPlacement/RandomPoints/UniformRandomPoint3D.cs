namespace EyapLibrary.GeometryAndPlacement.RandomPoints
{
	using UnityEngine;
	using System;

	/// <summary>
	/// Utils class to get a random point inside a 3D shape.
	/// Currently supported shapes :
	/// Cube / Cuboid
	/// Sphere
	/// Spherical shell (Sphere with a hole inside)
	/// </summary>
	public static class UniformRandomPoint3D
	{
		/// <summary>
		/// Get a uniform-random point inside the cuboid shape.
		/// The shape is centered on (0, 0, 0)
		/// </summary>
		/// <param name="width">The width of the cuboid.</param>
		/// <param name="length">The length of the cuboid.</param>
		/// <param name="depth">The depth of the cuboid.</param>
		/// <param name="rng">The random object to be used.</param>
		/// <returns>A random point inside the shape.</returns>
		public static Vector3 GetInCuboid(float width, float length, float depth, System.Random rng)
		{
			return new Vector3(
				((float)rng.NextDouble() - 0.5f) * width,
				((float)rng.NextDouble() - 0.5f) * length,
				((float)rng.NextDouble() - 0.5f) * depth);
		}

		/// <summary>
		/// Get a uniform-random point inside the cuboid shape.
		/// The shape is centered on (0, 0, 0).
		/// </summary>
		/// <param name="cuboidDimensions">The dimensions of the cuboid as a Vector3.</param>
		/// <param name="rng">The random object to be used.</param>
		/// <returns>A random point inside the shape.</returns>
		public static Vector3 GetInCuboid(Vector3 cuboidDimensions, System.Random rng)
		{
			return GetInCuboid(cuboidDimensions.x, cuboidDimensions.y, cuboidDimensions.z, rng);
		}


		/// <summary>
		/// Get a uniform-random point inside the sphere shape.
		/// </summary>
		/// <param name="radius">The radius of the sphere.</param>
		/// <param name="rng">The random object to be used.</param>
		/// <returns>A random point inside the shape.</returns>
		public static Vector3 GetInSphere(float radius, System.Random rng)
		{
			// Arguments Verifications
			if (radius <= 0)
			{
				throw new ArgumentException("The radius needs to be strictly positive.", "radius");
			}

			// Implementation
			float r = Mathf.Pow((float)rng.NextDouble(), 1 / 3f) * radius;
			float theta = (float)rng.NextDouble() * 2.0f * Mathf.PI;
			float phi = Mathf.Acos(2 * (float)rng.NextDouble() - 1); // Prevent bias on main axis (for theta = 0°)
			float sinPhi = Mathf.Sin(phi);
			return new Vector3(
				r * Mathf.Cos(theta) * sinPhi,
				r * Mathf.Sin(theta) * sinPhi,
				r * Mathf.Cos(phi)
			);
		}

		/// <summary>
		/// Get a uniform-random point inside the sphere shape, by discarding points from a initial cube.
		/// </summary>
		/// <param name="radius">The radius of the sphere.</param>
		/// <param name="rng">The random object to be used.</param>
		/// <returns>A random point inside the shape.</returns>
		public static Vector3 GetInSphereByDiscarding(float radius, System.Random rng)
		{
			// Arguments Verifications
			if (radius <= 0)
			{
				throw new ArgumentException("The radius needs to be strictly positive.", "radius");
			}

			// Implementation
			float sqrRadius = radius * radius;
			const int maxTryCount = 20; // Should be enough, probability of success is 53%.
			int tryIndex = 0;
			Vector3 point;
			float sqrMagnitude;
			do
			{
				tryIndex++;
				point = GetInCuboid(radius, radius, radius, rng);
				sqrMagnitude = point.sqrMagnitude;
			} while (sqrMagnitude > 1 && tryIndex < maxTryCount);
			if (tryIndex == maxTryCount && sqrMagnitude > sqrRadius)
			{
				throw new Exception("Can't find an acceptable point.");
			}
			return point;
		}


		/// <summary>
		/// Get a random point inside the sphere shape. NOT UNIFORM - Biased near center
		/// </summary>
		/// <param name="radius">The radius of the sphere.</param>
		/// <param name="rng">The random object to be used.</param>
		/// <returns>A random point inside the shape.</returns>
		public static Vector3 GetInBiasedSphere(float radius, System.Random rng)
		{
			// Arguments Verifications
			if (radius <= 0)
			{
				throw new ArgumentException("The radius needs to be strictly positive.", "radius");
			}

			// Implementation
			float r = (float)rng.NextDouble() * radius;
			float theta = (float)rng.NextDouble() * 2.0f * Mathf.PI;
			float phi = Mathf.Acos(2 * (float)rng.NextDouble() - 1); // Prevent bias on main axis (for theta = 0°)
			float sinPhi = Mathf.Sin(phi);
			return new Vector3(
				r * Mathf.Cos(theta) * sinPhi,
				r * Mathf.Sin(theta) * sinPhi,
				r * Mathf.Cos(phi)
			);
		}

		/// <summary>
		/// Get a uniform-random point inside the spherical shell (holed sphere) shape.
		/// </summary>
		/// <param name="internalRadius">The radius of the hole.</param>
		/// <param name="externalRadius">The radius of the sphere.</param>
		/// <param name="rng">The random object to be used.</param>
		/// <returns>A random point inside the shape.</returns>
		public static Vector3 GetInSphericalShell(float internalRadius, float externalRadius, System.Random rng)
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
			const int maxTryCount = 100;
			int tryIndex = 0;
			float minRandomValue = Mathf.Pow(internalRadius / externalRadius, 3);
			float randomValue;
			do
			{
				tryIndex++;
				randomValue = (float)rng.NextDouble();
			} while (randomValue < minRandomValue && tryIndex < maxTryCount);
			if (tryIndex == maxTryCount && randomValue < minRandomValue)
			{
				throw new Exception("Can't find an acceptable random radius.");
			}

			float r = Mathf.Pow(randomValue, 1 / 3f) * externalRadius;
			float theta = (float)rng.NextDouble() * 2.0f * Mathf.PI;
			float phi = Mathf.Acos(2 * (float)rng.NextDouble() - 1); // Prevent bias on main axis (for theta = 0°)
			float sinPhi = Mathf.Sin(phi);
			return new Vector3(
				r * Mathf.Cos(theta) * sinPhi,
				r * Mathf.Sin(theta) * sinPhi,
				r * Mathf.Cos(phi)
			);
		}

		/// <summary>
		/// Get a uniform-random point inside the spherical shell (holed sphere) shape.
		/// </summary>
		/// <param name="internalRadius">The radius of the hole.</param>
		/// <param name="externalRadius">The radius of the sphere.</param>
		/// <param name="rng">The random object to be used.</param>
		/// <returns>A random point inside the shape.</returns>
		public static Vector3 GetInSphericalShellByDiscarding(float internalRadius, float externalRadius, System.Random rng)
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
			const int maxTryCount = 100;
			int tryIndex = 0;
			Vector3 point;
			float sqrMagnitude;
			do
			{
				tryIndex++;
				point = GetInCuboid(externalRadius, externalRadius, externalRadius, rng);
				sqrMagnitude = point.sqrMagnitude;
			} while (sqrMagnitude > sqrExternalRadius && sqrMagnitude < sqrInternalRadius && tryIndex < maxTryCount);
			if (tryIndex >= maxTryCount && sqrMagnitude > sqrExternalRadius && sqrMagnitude < sqrInternalRadius)
			{
				throw new Exception("Can't find an acceptable random point.");
			}
			return point;
		}

		/// <summary>
		/// Get a uniform-random point inside the sphere shape.
		/// </summary>
		/// <param name="holeRadius">The radius of the hole.</param>
		/// <param name="externalRadius">The radius of the sphere.</param>
		/// <returns>A random point inside the shape.</returns>
		public static Vector3 GetInMeshByDiscarding(Mesh mesh, System.Random rng)
		{
			//TODO
			throw new NotImplementedException();
		}

		/// <summary>
		/// Check if the given point is inside the given mesh.
		/// </summary>
		/// <param name="point">Point that need to be checked.</param>
		/// <param name="mesh">The mesh the point will be checked against.</param>
		/// <returns>True if the point is inside, False otherwise.</returns>
		private static bool IsPointInsideMesh(Vector3 point, Mesh mesh)
		{
			// TODO
			throw new NotImplementedException();
		}
	}
}
