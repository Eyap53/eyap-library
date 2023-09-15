namespace EyapLibrary.SceneManagement
{
	using System;
	using EyapLibrary.Utils;
	using UnityEngine;
	using UnityEngine.SceneManagement;

	public class SceneSwitcher : Singleton<SceneSwitcher>
	{
		public SceneSO CurrentlyLoadedScene { get; private set; }
		public bool CurrentlySwitchingScene { get; private set; } = false;

		private string _newSceneName;

		/// <summary>
		/// Switches the currently loaded scene to the scene with the given name.
		/// </summary>
		/// <param name="sceneToLoadName">The name of the scene to load.</param>
		/// <returns>True if the scene was switched, false otherwise.</returns>
		/// <exception cref="ArgumentException">If no scene with that name exists.</exception>
		public bool SwitchScene(SceneSO sceneSO)
		{
			// Checks
			if (sceneSO == null)
			{
				throw new ArgumentException("SceneLoader: No sceneSO provided.");
			}
			if (SceneManager.GetSceneByName(sceneSO.sceneName) == null)
			{
				throw new ArgumentException("SceneLoader: No scene with that name.");
			}
			if (CurrentlySwitchingScene)
			{
				Debug.LogWarning("SceneLoader: Scene currently loading");
				return false;
			}

			CurrentlyLoadedScene = sceneSO;
			CurrentlySwitchingScene = true;


			SceneLoader.LoadScene(sceneSO.sceneName, () => OnSceneSwitch(sceneSO));
			foreach (string sceneName in sceneSO.additionnalSceneNames)
			{
				SceneLoader.LoadScene(sceneName);
			}
			return true;
		}

		/// <summary>
		/// Called when the scene is switched.
		/// </summary>
		/// <param name="operation">The async operation that loaded the scene.</param>
		protected void OnSceneSwitch(SceneSO sceneSO)
		{
			CurrentlySwitchingScene = false;
			CurrentlyLoadedScene = sceneSO;
			SceneManager.SetActiveScene(SceneManager.GetSceneByName(_newSceneName));
		}
	}
}

