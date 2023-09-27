namespace EyapLibrary.SceneManagement
{
	using UnityEngine;

	/// <summary>
	/// A simple script to show a loading script.
	/// May be improved / extended upon later.
	/// </summary>
	public class LoadingScreenDisplayer : MonoBehaviour
	{
		[SerializeField] protected GameObject _loadingScreen;

		public void DisplayLoadingScreen(bool state)
		{
			_loadingScreen.SetActive(state);
		}

	}
}
