namespace AtomsExtensions
{
	using UnityEngine;
	using UnityAtoms.BaseAtoms;

	public class ParseToIntVariable : MonoBehaviour
	{
		[SerializeField] private IntVariable _intVariable;

		public void ParseFromString(string value)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				throw new System.ArgumentException($"'{nameof(value)}' cannot be null or whitespace.", nameof(value));
			}

			int newValue = int.Parse(value);
			_intVariable.SetValue(newValue);
		}
	}
}
