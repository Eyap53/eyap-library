namespace EyapLibrary.Extensions
{
	using System;
	using UnityEngine;

	public static class GameObjectExtensions
	{
		/// <summary>
		/// Calls GameObject.Destroy on all children of transform.
		/// </summary>
		/// <param name="transform"></param>
		/// <param name="detachChildren">If true, immediately detaches the children
		/// from transform so after this call tranform.childCount is zero</param>
		public static void DestroyChildren(this Transform transform, bool detachChildren = true)
		{
			int childCount = transform.childCount;
			if (childCount > 0)
			{
				for (int i = childCount - 1; i >= 0; --i)
				{
					GameObject.Destroy(transform.GetChild(i).gameObject);
				}
				if (detachChildren)
				{
					transform.DetachChildren();
				}
			}
		}

		/// <summary>
		/// Sets the layer for this game object and all its children
		/// </summary>
		public static void SetLayerRecursively(this GameObject gameObject, int layer)
		{
			gameObject.layer = layer;

			// Non recursive, non allocating traversal
			Transform goTransform = gameObject.transform;
			if (goTransform.childCount > 0)
			{
				// Walk the hierarchy and set the layer

				if (goTransform.childCount == 0)
				{
					throw new InvalidOperationException("Root transform has no children");
				}

				Transform workingTransform = goTransform.GetChild(0);

				// Work until we get back to the root
				while (workingTransform != goTransform)
				{
					// Change layer
					workingTransform.gameObject.layer = layer;

					// Get children if we have
					if (workingTransform.childCount > 0)
					{
						workingTransform = workingTransform.GetChild(0);
					}
					// No children, look for siblings
					else
					{
						// Set to our sibling
						if (!TryGetNextSibling(ref workingTransform))
						{
							// Otherwise walk up parents and find THEIR next sibling
							workingTransform = workingTransform.parent;

							while (workingTransform != goTransform &&
								   !TryGetNextSibling(ref workingTransform))
							{
								workingTransform = workingTransform.parent;
							}
						}
					}
				}
			}
		}

		#region Private helpers

		/// <summary>
		/// Tries to advance to a sibling of <paramref name="transform"/>
		/// </summary>
		/// <param name="transform">The transform whose siblings we're looking for</param>
		/// <returns>True if we had a sibling. <paramref name="transform"/> will now refer to it.</returns>
		static bool TryGetNextSibling(ref Transform transform)
		{
			Transform parent = transform.parent;
			int siblingIndex = transform.GetSiblingIndex();

			// Get siblings if we don't have children
			if (parent.childCount > siblingIndex + 1)
			{
				transform = parent.GetChild(siblingIndex + 1);
				return true;
			}

			return false;
		}

		#endregion
	}
}
