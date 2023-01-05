namespace EyapLibrary.Utils.RandomPoints
{
	using UnityEngine;
	using System;
	using System.Collections.Generic;

	public static class PoissonDisk3DGradient
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
		/// The delegate for the gradient method.
		/// </summary>
		/// <param name="position">The position that need to be evaluated.</param>
		/// <returns>A value between 0f and 1f.</returns>
		public delegate float Gradient(Vector3 position);

		/// <summary>
		/// PoissonDisk algorythm in 3D, with a gradient method to make the minimal distance vary.
		/// Produce nice natural-looking results.
		/// See : http://devmag.org.za/2009/05/03/poisson-disk-sampling/.
		/// </summary>
		/// <param name="cuboidDimensions">The dimensions of the cuboid as a Vector3.</param>
		/// <param name="minRadiusMinimal">The minimal distance between sampling points as its minimal (when gradient produce 0f).</param>
		/// <param name="minRadiusMaximal">The minimal distance between sampling points as its maximal (when gradient produce 1f).</param>
		/// <param name="gradient">Delegate that takes a position and gives a value that change the minimal distance. The higher the value the higher the minimal distance between points.</param>
		/// <param name="maxSampleAttempts">The maximum number of attempts for a single sample before marking it inactive.</param>
		/// <param name="rng">The random object to be used.</param>
		/// <returns>A list of Vector3 points as a poisson disk in 3D.</returns>
		static public List<Vector3> GetCuboidSampling(Vector3 cuboidDimensions, float minRadiusMinimal, float minRadiusMaximal, Gradient gradient, System.Random rng, int maxSampleAttempts = 30)
		{
			// Arguments Verifications
			if (cuboidDimensions.x <= 0 || cuboidDimensions.y <= 0 || cuboidDimensions.z <= 0)
			{
				throw new ArgumentException("The cuboid dimensions need to be strictly positives.", "cuboidDimensions");
			}
			if (minRadiusMinimal <= 0)
			{
				throw new ArgumentException("The sampling radius needs to be strictly positive.", "minRadiusMinimal");
			}
			if (minRadiusMaximal <= 0)
			{
				throw new ArgumentException("The sampling radius needs to be strictly positive.", "minRadiusMaximal");
			}
			if (minRadiusMinimal == minRadiusMaximal)
			{
				throw new ArgumentException("The sampling radius as its maximal needs to be strictly higher that as its minimal. If you don't want to use a gradient, use the PoissonDisk3D class instead.", "minRadiusMaximal");
			}
			if (minRadiusMinimal > minRadiusMaximal)
			{
				throw new ArgumentException("The sampling radius as its maximal needs to be strictly higher that as its minimal.", "minRadiusMaximal");
			}
			if (maxSampleAttempts <= 0)
			{
				throw new ArgumentException("The maximum sample attempts count needs to be strictly positive.", "maxSampleAttempts");
			}

			// Implementation
			float gradientCoefficient = minRadiusMaximal - minRadiusMinimal;
			float cellSize = minRadiusMaximal / Mathf.Sqrt(3);
			List<Vector3>[,,] grid = new List<Vector3>[Mathf.CeilToInt(cuboidDimensions.x / cellSize),
							   Mathf.CeilToInt(cuboidDimensions.y / cellSize),
							   Mathf.CeilToInt(cuboidDimensions.z / cellSize)];
			Vector3 candidate;


			List<Vector3> activeSamples = new List<Vector3>();
			List<Vector3> finalList = new List<Vector3>();

			int attemptIndex = 0;
			bool found = false;

			{ // First sample is choosen randomly inside the cube
				candidate = UniformRandomPoint.GetInCuboid(cuboidDimensions, rng);
				activeSamples.Add(candidate);
				finalList.Add(candidate);
				GridPos pos = new GridPos(candidate, cuboidDimensions, cellSize);
				grid[pos.x, pos.y, pos.z] = new List<Vector3> { candidate };
			}

			while (activeSamples.Count > 0)
			{
				// Pick a random active sample
				int randomActiveIndex = UnityEngine.Random.Range(0, activeSamples.Count);
				Vector3 sample = activeSamples[randomActiveIndex];
				float currentRadius = minRadiusMinimal + gradient(sample) * gradientCoefficient;
				float sqrRadius = currentRadius * currentRadius;  // radius squared

				attemptIndex = 0;
				found = false;
				do
				{
					attemptIndex++;
					candidate = sample + UniformRandomPoint.GetInHoledSphereByDiscarding(currentRadius, 2 * currentRadius, rng); // Generate random point around sample.

					found = IsInsideCuboid(candidate, cuboidDimensions) &&
							IsFarEnough(candidate, grid, cuboidDimensions, cellSize, sqrRadius);
				} while (attemptIndex < maxSampleAttempts && !found);

				if (found)
				{
					activeSamples.Add(candidate);
					finalList.Add(candidate);
					GridPos pos = new GridPos(candidate, cuboidDimensions, cellSize);
					if (grid[pos.x, pos.y, pos.z] != null)
					{
						grid[pos.x, pos.y, pos.z].Add(candidate);
					}
					else
					{
						grid[pos.x, pos.y, pos.z] = new List<Vector3> { candidate };
					}
				}
				else
				{
					activeSamples.RemoveAt(randomActiveIndex);
				}
			}

			return finalList;
		}

		static private bool IsFarEnough(Vector3 sample, List<Vector3>[,,] grid, Vector3 cuboidDimensions, float cellSize, float radius2)
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
						List<Vector3> samplesInCell = grid[x, y, z];
						if (samplesInCell != null && samplesInCell.Count > 0)
						{
							foreach (Vector3 s in samplesInCell)
							{
								Vector3 d = s - sample;
								if (d.sqrMagnitude < radius2)
								{
									return false;
								}
							}
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
