namespace EyapLibrary.GeometryAndPlacement.RandomPoints.Tests
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using NUnit.Framework;
	using UnityEngine;

	/// <summary>
	/// Tests for <see cref="PoissonDisk3D"/>
	/// </summary>
	public class PoissonDisk3DTests
	{
		[Test]
		public void GetCuboidSampling_CuboidDimensionsXLessThanZero_ThrowsArgumentException()
		{
			// Arrange
			Vector3 cuboidDimensions = new Vector3(-1f, 1f, 1f);
			float samplingRadius = 1f;
			System.Random rng = new System.Random();

			// Act
			// Assert
			Assert.Throws<ArgumentException>(() => PoissonDisk3D.GetCuboidSampling(cuboidDimensions, samplingRadius, rng));
		}

		/// <summary>
		/// Tests that every point of the GetCuboidSampling is inside the cuboid.
		/// </summary>
		[Test]
		public void GetCuboidSampling_AllPointsInsideCuboid()
		{
			// Arrange
			Vector3 cuboidDimensions = new Vector3(10f, 10f, 10f);
			float samplingRadius = 1f;
			System.Random rng = new System.Random();

			// Act
			List<Vector3> points = PoissonDisk3D.GetCuboidSampling(cuboidDimensions, samplingRadius, rng);

			// Assert
			foreach (Vector3 point in points)
			{
				Assert.IsTrue(point.x >= -cuboidDimensions.x * 0.5f && point.x <= cuboidDimensions.x * 0.5f);
				Assert.IsTrue(point.y >= -cuboidDimensions.y * 0.5f && point.y <= cuboidDimensions.y * 0.5f);
				Assert.IsTrue(point.z >= -cuboidDimensions.z * 0.5f && point.z <= cuboidDimensions.z * 0.5f);
			}
		}
	}
}
