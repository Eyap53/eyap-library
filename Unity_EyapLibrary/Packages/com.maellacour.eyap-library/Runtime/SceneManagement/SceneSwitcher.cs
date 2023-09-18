namespace EyapLibrary.SceneManagement
{
	using System;
	using System.Linq;
	using EyapLibrary.Utils;
	using UnityEngine;
	using UnityEngine.SceneManagement;

	public class SceneSwitcher : PersistentSingleton<SceneSwitcher>
	{
		[Tooltip("Either Initialization, or the current scene for an in-editor cold Startup.")]
		[SerializeField] private SceneSO currentlyLoadedScene;

		public SceneSO CurrentlyLoadedScene { get => currentlyLoadedScene; private set => currentlyLoadedScene = value; }
		public bool CurrentlySwitchingScene { get; private set; } = false;

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
			if (CurrentlyLoadedScene == sceneSO)
			{
				Debug.LogWarning("SceneLoader: Scene already loaded");
				return true;
			}


			CurrentlySwitchingScene = true;

			SceneLoader.LoadScene(sceneSO.sceneName, () => OnMainSceneSwitch(sceneSO));
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
		protected void OnMainSceneSwitch(SceneSO sceneSO)
		{
			SceneSO previousSceneSO = CurrentlyLoadedScene;
			CurrentlyLoadedScene = sceneSO;
			SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneSO.sceneName));
			UnloadPreviouScenes(CurrentlyLoadedScene, previousSceneSO, OnPreviousScenesUnloaded);
		}

		/// <summary>
		/// Unloads the previous scenes.
		/// </summary>
		/// <param name="previousSceneSO">The sceneSO of the previous scene.</param>
		/// <exception cref="ArgumentException">If no scene with that name exists.</exception>
		protected static void UnloadPreviouScenes(SceneSO newSceneSO, SceneSO previousSceneSO, Action sceneUnloadedCallback = null)
		{
			if (previousSceneSO == null)
			{
				return;
			}
			if (SceneManager.GetSceneByName(previousSceneSO.sceneName) == null)
			{
				throw new ArgumentException("SceneLoader: No scene with that name.");
			}

			if (!newSceneSO.additionnalSceneNames.Contains(previousSceneSO.sceneName))
			{
				SceneLoader.UnloadScene(previousSceneSO.sceneName, sceneUnloadedCallback);
			}
			foreach (string oldSceneName in previousSceneSO.additionnalSceneNames)
			{
				if (oldSceneName != newSceneSO.sceneName && !newSceneSO.additionnalSceneNames.Contains(oldSceneName))
				{
					SceneLoader.UnloadScene(oldSceneName);
				}
			}
		}

		/// <summary>
		/// Called when previous scenes unloaded.
		/// </summary>
		protected void OnPreviousScenesUnloaded()
		{
			CurrentlySwitchingScene = false;
		}
	}
}

