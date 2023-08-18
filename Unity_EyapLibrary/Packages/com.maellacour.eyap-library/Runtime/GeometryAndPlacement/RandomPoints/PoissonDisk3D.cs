namespace EyapLibrary.GeometryAndPlacement.RandomPoints
{
	using UnityEngine;
	using System;
	using System.Collections.Generic;

	/// <summary>
	/// Class that contains the methods for 3D Poisson Sampling.
	/// Only the cube is supported as of now.
	/// </summary>
	public static class PoissonDisk3D
	{
		/// <summary>
		/// Helper struct to calculate the x and y indices of a sample in the grid
		/// </summary>
		private struct GridPos
		{
			public int x;
			public int y;
			public int z;

			public GridPos(Vector3 sample, Vector3 cuboidDimensions, float cellSize)
			{
				x = (int)((sample.x + cuboidDimensions.x * 0.5f) / cellSize);
				y = (int)((sample.y + cuboidDimensions.y * 0.5f) / cellSize);
				z = (int)((sample.z + cuboidDimensions.z * 0.5f) / cellSize);
			}
		}

		/// <summary>
		/// PoissonDisk algorythm in 3D.
		/// </summary>
		/// <param name="cuboidDimensions">The dimensions of the cuboid as a Vector3.</param>
		/// <param name="samplingRadius">The minimal distance between sampling points.</param>
		/// <param name="maxSampleAttempts">The maximum number of attempts for a single sample before marking it inactive.</param>
		/// <param name="rng">The random object to be used.</param>
		/// <returns>A list of Vector3 points as a poisson disk in 3D.</returns>
		static public List<Vector3> GetCuboidSampling(Vector3 cuboidDimensions, float samplingRadius, System.Random rng, int maxSampleAttempts = 30)
		{
			// Arguments Verifications
			if (cuboidDimensions.x <= 0 || cuboidDimensions.y <= 0 || cuboidDimensions.z <= 0)
			{
				throw new ArgumentException("The cuboid dimensions need to be strictly positives.", "cuboidDimensions");
			}
			if (samplingRadius <= 0)
			{
				throw new ArgumentException("The sampling radius needs to be strictly positive.", "samplingRadius");
			}
			if (maxSampleAttempts <= 0)
			{
				throw new ArgumentException("The maximum sample attempts count needs to be strictly positive.", "maxSampleAttempts");
			}

			// Implementation
			float sqrRadius = samplingRadius * samplingRadius;  // radius squared
			float doubledRadius = 2 * samplingRadius;  // radius doubled
			float cellSize = samplingRadius / Mathf.Sqrt(3);
			Vector3[,,] grid = new Vector3[Mathf.CeilToInt(cuboidDimensions.x / cellSize),
							   Mathf.CeilToInt(cuboidDimensions.y / cellSize),
							   Mathf.CeilToInt(cuboidDimensions.z / cellSize)];
			Vector3 candidate;


			List<Vector3> activeSamples = new List<Vector3>();
			List<Vector3> finalList = new List<Vector3>();

			int attemptIndex = 0;
			bool found = false;

			{ // First sample is choosen randomly inside the cube
				candidate = UniformRandomPoint3D.GetInCuboid(cuboidDimensions, rng);
				activeSamples.Add(candidate);
				finalList.Add(candidate);
				GridPos pos = new GridPos(candidate, cuboidDimensions, cellSize);
				grid[pos.x, pos.y, pos.z] = candidate;
			}

			while (activeSamples.Count > 0)
			{
				// Pick a random active sample
				int randomActiveIndex = UnityEngine.Random.Range(0, activeSamples.Count);
				Vector3 sample = activeSamples[randomActiveIndex];

				attemptIndex = 0;
				found = false;
				do
				{
					attemptIndex++;
					candidate = sample + UniformRandomPoint3D.GetInSphericalShell(samplingRadius, doubledRadius, rng); // Generate random point around sample.

					found = IsInsideCuboid(candidate, cuboidDimensions) &&
							IsFarEnough(candidate, grid, cuboidDimensions, cellSize, sqrRadius);
				} while (attemptIndex < maxSampleAttempts && !found);

				if (found)
				{
					activeSamples.Add(candidate);
					finalList.Add(candidate);
					GridPos pos = new GridPos(candidate, cuboidDimensions, cellSize);
					grid[pos.x, pos.y, pos.z] = candidate;
				}
				else
				{
					activeSamples.RemoveAt(randomActiveIndex);
				}
			}

			return finalList;
		}

		static private bool IsFarEnough(Vector3 sample, Vector3[,,] grid, Vector3 cuboidDimensions, float cellSize, float radius2)
		{
			GridPos pos = new GridPos(sample, cuboidDimensions, cellSize);

			int xmin = Mathf.Max(pos.x - 2, 0);
			int ymin = Mathf.Max(pos.y - 2, 0);
			int zmin = Mathf.Max(pos.z - 2, 0);
			int xmax = Mathf.Min(pos.x + 2, grid.GetLength(0) - 1);
			int ymax = Mathf.Min(pos.y + 2, grid.GetLength(1) - 1);
			int zmax = Mathf.Min(pos.z + 2, grid.GetLength(2) - 1);

			for (int z = zmin; z <= zmax; z++)
			{
				for (int y = ymin; y <= ymax; y++)
				{
					for (int x = xmin; x <= xmax; x++)
					{
						Vector3 s = grid[x, y, z];
						if (s != Vector3.zero)
						{
							Vector3 d = s - sample;
							if (d.sqrMagnitude < radius2)
								return false;
						}
					}
				}
			}

			return true;

		}

		static private bool IsInsideCuboid(Vector3 point, Vector3 cuboidDimensions)
		{
			if (point.x >= -cuboidDimensions.x / 2 && point.x <= cuboidDimensions.x / 2 &&
				point.y >= -cuboidDimensions.y / 2 && point.y <= cuboidDimensions.y / 2 &&
				point.z >= -cuboidDimensions.z / 2 && point.z <= cuboidDimensions.z / 2)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
