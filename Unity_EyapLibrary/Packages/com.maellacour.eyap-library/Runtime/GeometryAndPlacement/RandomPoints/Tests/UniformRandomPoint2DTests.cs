namespace EyapLibrary.GeometryAndPlacement.RandomPoints.Tests
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using NUnit.Framework;
	using UnityEngine;

	/// <summary>
	/// Tests for <see cref="UniformRandomPoint2D"/>
	/// </summary>
	class UniformRandomPoint2DTests
	{
		[Test]
		public void GetInAnnulus_ExternalRadiusLessThanZero_ThrowsArgumentException()
		{
			// Arrange
			float internalRadius = 1f;
			float externalRadius = -1f;

			// Act
			// Assert
			Assert.Throws<ArgumentException>(() => UniformRandomPoint2D.GetInAnnulus(internalRadius, externalRadius));
		}

		[Test]
		public void GetInAnnulus_InternalRadiusLessThanZero_ThrowsArgumentException()
		{
			// Arrange
			float internalRadius = -1f;
			float externalRadius = 1f;

			// Act
			// Assert
			Assert.Throws<ArgumentException>(() => UniformRandomPoint2D.GetInAnnulus(internalRadius, externalRadius));
		}

		[Test]
		public void GetInAnnulus_InternalRadiusEqualToExternalRadius_ThrowsArgumentException()
		{
			// Arrange
			float internalRadius = 1f;
			float externalRadius = 1f;

			// Act
			// Assert
			Assert.Throws<ArgumentException>(() => UniformRandomPoint2D.GetInAnnulus(internalRadius, externalRadius));
		}

		[Test]
		public void GetInAnnulus_ExternalRadiusGreaterThanInternalRadius_ReturnsPointInsideAnnulus()
		{
			// Arrange
			float internalRadius = 1f;
			float externalRadius = 2f;

			// Act
			Vector2 point = UniformRandomPoint2D.GetInAnnulus(internalRadius, externalRadius);

			// Assert
			Assert.IsTrue(point.magnitude >= internalRadius);
			Assert.IsTrue(point.magnitude <= externalRadius);
		}

		[Test]
		public void GetInAnnulus_ExternalRadiusGreaterThanInternalRadius_ReturnsPointInsideAnnulusWithRandomValues()
		{
			// Arrange
			float internalRadius = 1f;
			float externalRadius = 2f;

			// Act
			Vector2 point1 = UniformRandomPoint2D.GetInAnnulus(internalRadius, externalRadius);
			Vector2 point2 = UniformRandomPoint2D.GetInAnnulus(internalRadius, externalRadius);

			// Assert
			Assert.AreNotEqual(point1, point2);
		}


		/// <summary>
		/// Test the reproducibility with a given rng object.
		/// </summary>
		[Test]
		public void GetInAnnulus_RngReproducibilityTest()
		{
			// Arrange
			float internalRadius = 1f;
			float externalRadius = 2f;
			System.Random rng = new(42);

			// Act
			Vector2 point1 = UniformRandomPoint2D.GetInAnnulus(internalRadius, externalRadius, rng);
			rng = new(42);
			Vector2 point2 = UniformRandomPoint2D.GetInAnnulus(internalRadius, externalRadius, rng);

			// Assert
			Assert.AreEqual(point1, point2);
		}
	}
}
