namespace EyapLibrary.GeometryAndPlacement.Tests
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using NUnit.Framework;
	using UnityEngine;

	[TestFixture]
	public class CircleHelperTests
	{

		private static class Vector2ArrayComparer
		{
			private const float ACCURACY_VALUE = 0.0001f; // Is enough in our case where we use between 1f and 4f of radius. Would not be otherwise.

			public static bool AreEqual(Vector2[] left, Vector2[] right)
			{
				if (left == null && right == null)
				{
					return true;
				}

				if (left == null || right == null)
				{
					return false;
				}

				if (left.Length != right.Length)
				{
					return false;
				}

				bool isEqual = true;
				for (int i = 0; i < left.Length; i++)
				{
					isEqual = isEqual && Mathf.Abs(left[i].x - right[i].x) < ACCURACY_VALUE;
					isEqual = isEqual && Mathf.Abs(left[i].y - right[i].y) < ACCURACY_VALUE;
				}
				return isEqual;
			}
		}

		[Test]
		public void GetEvenlySpaced_TotalPointsTest1()
		{
			Vector2[] resultPoints = CircleHelper.GetEvenlySpaced(2);
			Vector2[] expectedPoints = new Vector2[] { new Vector2(1f, 0), new Vector2(-1, 0) };
			Assert.True(Vector2ArrayComparer.AreEqual(resultPoints, expectedPoints));
		}

		[Test]
		public void GetEvenlySpaced_TotalPointsTest2()
		{
			Vector2[] resultPoints = CircleHelper.GetEvenlySpaced(4);
			Vector2[] expectedPoints = new Vector2[] { new Vector2(1f, 0), new Vector2(0, 1f), new Vector2(-1f, 0f), new Vector2(0, -1f) };
			Assert.True(Vector2ArrayComparer.AreEqual(resultPoints, expectedPoints));
		}

		[Test]
		public void GetEvenlySpaced_RadiusTest()
		{
			Vector2[] resultPoints = CircleHelper.GetEvenlySpaced(4, radius: 4f);
			Vector2[] expectedPoints = new Vector2[] { new Vector2(4f, 0), new Vector2(0, 4f), new Vector2(-4f, 0f), new Vector2(0, -4f) };
			Assert.True(Vector2ArrayComparer.AreEqual(resultPoints, expectedPoints));
		}
	}
}
