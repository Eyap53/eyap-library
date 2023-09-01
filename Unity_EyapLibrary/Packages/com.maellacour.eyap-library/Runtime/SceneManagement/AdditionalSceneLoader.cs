namespace EyapLibrary.SceneManagement
{
	using System;
	using UnityEngine;
	using UnityEngine.SceneManagement;

	public class AdditionalSceneLoader : MonoBehaviour
	{
		[SerializeField] private string _sceneName;

		public bool IsLoadingAdditionalScene { get; private set; }

		protected void Awake()
		{
			LoadAdditionalScene();
		}

		/// <summary>
		/// Loads the additional scene if it is not already loaded.
		/// </summary>
		/// <exception cref="ArgumentException"> If no scene with that name exists.</exception>
		protected void LoadAdditionalScene()
		{
			// Checks
			if (SceneManager.GetSceneByName(_sceneName) == null)
			{
				throw new ArgumentException("AdditionalSceneLoader: No scene with that name.");
			}
			if (SceneManager.GetSceneByName(_sceneName).isLoaded)
			{
				Debug.LogWarning("AdditionalSceneLoader: Scene already loaded");
				return;
			}
			if (IsLoadingAdditionalScene)
			{
				Debug.LogWarning("AdditionalSceneLoader: Scene currently loading");
				return;
			}

			// Implementation
			IsLoadingAdditionalScene = true;
			AsyncOperation async = SceneManager.LoadSceneAsync(_sceneName, LoadSceneMode.Additive);
			async.completed += OnAdditionalSceneLoad;
		}

		/// <summary>
		/// Called when the additional scene is loaded.
		/// </summary>
		/// <param name="a">The async operation that loaded the scene.</param>
		protected void OnAdditionalSceneLoad(AsyncOperation a)
		{
			IsLoadingAdditionalScene = false;
		}
	}
}

