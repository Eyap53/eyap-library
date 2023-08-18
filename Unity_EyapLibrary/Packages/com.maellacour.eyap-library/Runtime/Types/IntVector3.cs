namespace EyapLibrary.Types
{
	using System;
	using UnityEngine;

	/// <summary>
	/// A 2-dimensional vector with integer components
	/// </summary>
	[Serializable]
	public struct IntVector3 : IEquatable<IntVector3>
	{
		/// <summary>
		/// Vector with both components being 1
		/// </summary>
		public static readonly IntVector3 one = new IntVector3(1, 1, 1);

		/// <summary>
		/// Vector with both components being 0
		/// </summary>
		public static readonly IntVector3 zero = new IntVector3(0, 0, 0);

		/// <summary>
		/// The x component of this vector
		/// </summary>
		public int x;

		/// <summary>
		/// The y component of this vector
		/// </summary>
		public int y;

		/// <summary>
		/// The z component of this vector
		/// </summary>
		public int z;

		/// <summary>
		/// Gets the squared magnitude of this vector
		/// </summary>
		public int sqrMagnitude
		{
			get { return (x * x) + (y * y) + (z * z); }
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
			get { return Mathf.Abs(x) + Mathf.Abs(y) + Mathf.Abs(z); }
		}

		/// <summary>
		/// Initialize a new vector
		/// </summary>
		public IntVector3(int x, int y, int z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public override bool Equals(object other)
		{
			if (ReferenceEquals(null, other))
				return false;
			if (ReferenceEquals(this, other))
				return true;
			if (this.GetType() != other.GetType())
				return false;
			return IsEqual((IntVector3)other);
		}

		public bool Equals(IntVector3 other)
		{
			// if (ReferenceEquals(null, other)) return false; // struct can't be null
			if (ReferenceEquals(this, other))
				return true;
			return IsEqual(other);
		}

		/// <summary>
		/// Simple hash multiplying by two primes
		/// </summary>
		public override int GetHashCode()
		{
			return (x, y, z).GetHashCode();
		}

		public override string ToString()
		{
			return string.Format("X: {0}, Y: {1}, Z: {2}", x, y, z);
		}

		public static bool operator ==(IntVector3 left, IntVector3 right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(IntVector3 left, IntVector3 right)
		{
			return !left.Equals(right);
		}

		public static implicit operator Vector3(IntVector3 vector)
		{
			return new Vector3(vector.x, vector.y, vector.z);
		}

		public static explicit operator IntVector3(Vector3 vector)
		{
			return new IntVector3((int)vector.x, (int)vector.y, (int)vector.z);
		}

		public static IntVector3 operator +(IntVector3 left, IntVector3 right)
		{
			return new IntVector3(left.x + right.x, left.y + right.y, left.z + right.z);
		}

		public static IntVector3 operator -(IntVector3 left, IntVector3 right)
		{
			return new IntVector3(left.x - right.x, left.y - right.y, left.z - right.z);
		}

		public static IntVector3 operator *(int scale, IntVector3 left)
		{
			return new IntVector3(left.x * scale, left.y * scale, left.z * scale);
		}

		public static IntVector3 operator *(IntVector3 left, int scale)
		{
			return new IntVector3(left.x * scale, left.y * scale, left.z * scale);
		}

		public static Vector3 operator *(float scale, IntVector3 left)
		{
			return new Vector3(left.x * scale, left.y * scale, left.z * scale);
		}

		public static Vector3 operator *(IntVector3 left, float scale)
		{
			return new Vector3(left.x * scale, left.y * scale, left.z * scale);
		}

		public static IntVector3 operator -(IntVector3 left)
		{
			return new IntVector3(-left.x, -left.y, -left.z);
		}

		/// <summary>
		/// Compare this object with other.
		/// </summary>
		private bool IsEqual(IntVector3 other)
		{
			return other.x == x && other.y == y && other.z == z;
		}
	}
}
