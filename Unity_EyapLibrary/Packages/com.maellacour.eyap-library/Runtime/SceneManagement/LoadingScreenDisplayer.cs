namespace EyapLibrary.SceneManagement
{
	using UnityEngine;

	public class LoadingScreenDisplayer : MonoBehaviour
	{
		[SerializeField] private GameObject _loadingScreen;

		public void DisplayLoadingScreen(bool state)
		{
			_loadingScreen.SetActive(state);
		}

	}
}
