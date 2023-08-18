namespace EyapLibrary.Utils
{
	using TMPro;
	using UnityEngine;

	public class VersionDisplayer : MonoBehaviour
	{
		[SerializeField]
		public TextMeshProUGUI _versionText;

		public void Start()
		{
			_versionText.text = Application.version;
			Debug.Log(string.Format("App version : {0}", Application.version));
		}
	}
}
