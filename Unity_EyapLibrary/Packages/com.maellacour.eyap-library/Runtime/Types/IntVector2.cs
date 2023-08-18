namespace EyapLibrary.Types
{
	using System;
	using UnityEngine;

	/// <summary>
	/// A 2-dimensional vector with integer components
	/// </summary>
	[Serializable]
	public struct IntVector2 : IEquatable<IntVector2>
	{
		/// <summary>
		/// Vector with both components being 1
		/// </summary>
		public static readonly IntVector2 one = new IntVector2(1, 1);

		/// <summary>
		/// Vector with both components being 0
		/// </summary>
		public static readonly IntVector2 zero = new IntVector2(0, 0);

		/// <summary>
		/// The x component of this vector
		/// </summary>
		public int x;

		/// <summary>
		/// The y component of this vector
		/// </summary>
		public int y;

		/// <summary>
		/// Gets the squared magnitude of this vector
		/// </summary>
		public int sqrMagnitude
		{
			get { return (x * x) + (y * y); }
		}

		/// <summary>
		/// Returns the magnitude of this vector
		/// </summary>
		public float magnitude
		{
			get { return Mathf.Sqrt(sqrMagnitude); }
		}

		/// <summary>
		/// Gets the manhattan distance of this vector
		/// </summary>
		public int manhattanDistance
		{
			get { return Mathf.Abs(x) + Mathf.Abs(y); }
		}

		/// <summary>
		/// Initialize a new vector
		/// </summary>
		public IntVector2(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		public override bool Equals(object other)
		{
			if (ReferenceEquals(null, other))
				return false;
			if (ReferenceEquals(this, other))
				return true;
			if (this.GetType() != other.GetType())
				return false;
			return IsEqual((IntVector2)other);
		}

		public bool Equals(IntVector2 other)
		{
			// if (ReferenceEquals(null, other)) return false; // struct can't be null
			if (ReferenceEquals(this, other))
				return true;
			return IsEqual(other);
		}

		public override int GetHashCode()
		{
			return (x, y).GetHashCode();
		}

		public override string ToString()
		{
			return string.Format("X: {0}, Y: {1}", x, y);
		}

		public static bool operator ==(IntVector2 left, IntVector2 right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(IntVector2 left, IntVector2 right)
		{
			return !left.Equals(right);
		}

		public static implicit operator Vector2(IntVector2 vector)
		{
			return new Vector2(vector.x, vector.y);
		}

		public static explicit operator IntVector2(Vector2 vector)
		{
			return new IntVector2((int)vector.x, (int)vector.y);
		}

		public static IntVector2 operator +(IntVector2 left, IntVector2 right)
		{
			return new IntVector2(left.x + right.x, left.y + right.y);
		}

		public static IntVector2 operator -(IntVector2 left, IntVector2 right)
		{
			return new IntVector2(left.x - right.x, left.y - right.y);
		}

		public static IntVector2 operator *(int scale, IntVector2 left)
		{
			return new IntVector2(left.x * scale, left.y * scale);
		}

		public static IntVector2 operator *(IntVector2 left, int scale)
		{
			return new IntVector2(left.x * scale, left.y * scale);
		}

		public static Vector2 operator *(float scale, IntVector2 left)
		{
			return new Vector2(left.x * scale, left.y * scale);
		}

		public static Vector2 operator *(IntVector2 left, float scale)
		{
			return new Vector2(left.x * scale, left.y * scale);
		}

		public static IntVector2 operator -(IntVector2 left)
		{
			return new IntVector2(-left.x, -left.y);
		}

		/// <summary>
		/// Compare this object with other.
		/// </summary>
		private bool IsEqual(IntVector2 other)
		{
			return other.x == x && other.y == y;
		}
	}
}
