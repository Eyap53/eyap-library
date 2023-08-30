namespace EyapLibrary.Extensions.Tests
{
	using System;
	using NUnit.Framework;
	using UnityEngine;

	/// <summary>
	/// Tests for <see cref="GameObjectExtensions"/>.
	/// </summary>
	/// <seealso cref="GameObject"/>"
	[TestFixture]
	public class GameObjectExtensionsTests
	{
		/// <summary>
		/// Tests for <see cref="GameObjectExtensions.DestroyImmediateChildren"/>.
		/// </summary>
		[Test]
		public void DestroyImmediateChildrenTest()
		{
			GameObject gameObject = new GameObject();
			GameObject child1 = new GameObject();
			GameObject child2 = new GameObject();
			child1.transform.SetParent(gameObject.transform);
			child2.transform.SetParent(gameObject.transform);

			Assert.AreEqual(2, gameObject.transform.childCount);
			gameObject.transform.DestroyImmediateChildren(true);
			Assert.AreEqual(0, gameObject.transform.childCount);
			Assert.IsTrue(child1 == null);
			Assert.IsTrue(child2 == null);
		}

	}
}
