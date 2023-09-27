namespace EyapLibrary.UI
{
	using TMPro;
	using UnityAtoms.BaseAtoms;
	using UnityEngine;

	public class IntDisplayerInputField : MonoBehaviour
	{
		[SerializeField] private TMP_InputField _textField;
		[SerializeField] private IntVariable _intVariable;

		protected void Awake()
		{
			UpdateTextField(_intVariable.Value);
		}

		public void UpdateTextField(int newValue)
		{
			_textField.text = newValue.ToString();
		}
	}
}
