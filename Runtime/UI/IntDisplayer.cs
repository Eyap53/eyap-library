namespace EyapLibrary.UI
{
	using TMPro;
	using UnityEngine;

	public sealed class IntDisplayer : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI _textField;

		public void UpdateTextField(int newValue)
		{
			_textField.text = newValue.ToString();
		}
	}
}
