namespace EyapLibrary.SceneManagement
{
	using System;
	using EyapLibrary.Utils;
	using UnityEngine;
	using UnityEngine.SceneManagement;

	public class SceneSwitcher : Singleton<SceneSwitcher>
	{
		public string CurrentlyLoadedSceneName { get; private set; }
		public bool CurrentlySwitchingScene { get; private set; } = false;

		private string _newSceneName;

		protected override void Awake()
		{
			base.Awake();
			CurrentlyLoadedSceneName = SceneManager.GetActiveScene().name;
		}

		/// <summary>
		/// Switches the currently loaded scene to the scene with the given name.
		/// </summary>
		/// <param name="sceneToLoadName">The name of the scene to load.</param>
		/// <returns>True if the scene was switched, false otherwise.</returns>
		/// <exception cref="ArgumentException">If no scene with that name exists.</exception>
		public bool SwitchScene(string sceneToLoadName)
		{
			// Checks
			if (SceneManager.GetSceneByName(sceneToLoadName) == null)
			{
				throw new ArgumentException("AdditionalSceneLoader: No scene with that name.");
			}
			if (SceneManager.GetSceneByName(sceneToLoadName).isLoaded)
			{
				Debug.LogWarning("AdditionalSceneLoader: Scene already loaded");
			}
			if (CurrentlySwitchingScene)
			{
				Debug.LogWarning("AdditionalSceneLoader: Already switching scene");
				return false;
			}

			CurrentlySwitchingScene = true;
			_newSceneName = sceneToLoadName;
			SceneManager.UnloadSceneAsync(CurrentlyLoadedSceneName);
			AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoadName, LoadSceneMode.Additive);
			asyncOperation.completed += OnSceneSwitch;
			return true;
		}

		/// <summary>
		/// Called when the scene is switched.
		/// </summary>
		/// <param name="operation">The async operation that loaded the scene.</param>
		protected void OnSceneSwitch(AsyncOperation operation)
		{
			CurrentlySwitchingScene = false;
			CurrentlyLoadedSceneName = _newSceneName;
			SceneManager.SetActiveScene(SceneManager.GetSceneByName(_newSceneName));
		}
	}
}

