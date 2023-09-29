namespace EyapLibrary.SceneManagement
{
	using System;
	using System.Linq;
	using EyapLibrary.Utils;
	using UnityEngine;
	using UnityEngine.SceneManagement;

	/// <summary>
	/// Monobehaviour that is responsible to switch scenes.
	/// It will load all needed scene according to the given SO, and unload all unused (previous) scenes.
	/// </summary>
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
		public bool SwitchScene(SceneSO sceneSO) => SwitchScene(sceneSO, isColdStartup: false);
		internal bool SwitchScene(SceneSO sceneSO, bool isColdStartup)
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

			CurrentlySwitchingScene = true;

			if (!isColdStartup)
			{
				SceneLoader.LoadScene(sceneSO.sceneName, () => OnMainSceneSwitch(sceneSO));
			}
			else
			{
				OnMainSceneSwitch(sceneSO, isColdStartup);
			}

			// Load additional scenes, if not loaded
			foreach (string sceneName in sceneSO.additionnalSceneNames)
			{
				if (!SceneManager.GetSceneByName(sceneName).isLoaded)
				{
					SceneLoader.LoadScene(sceneName);
				}
			}

			return true;
		}

		/// <summary>
		/// Called when the scene is switched.
		/// </summary>
		/// <param name="operation">The async operation that loaded the scene.</param>
		protected void OnMainSceneSwitch(SceneSO sceneSO, bool isColdStartup = false)
		{
			SceneSO previousSceneSO = CurrentlyLoadedScene;
			CurrentlyLoadedScene = sceneSO;
			if (!isColdStartup)
			{
				SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneSO.sceneName));
			}

			if (previousSceneSO != null)
			{
				UnloadPreviouScenes(CurrentlyLoadedScene, previousSceneSO, OnPreviousScenesUnloaded);
			}
			else
			{
				CurrentlySwitchingScene = false;
			}
		}

		/// <summary>
		/// Unloads the previous scenes.
		/// </summary>
		/// <param name="previousSceneSO">The sceneSO of the previous scene.</param>
		/// <exception cref="ArgumentException">If current scene is null.</exception>
		/// <exception cref="ArgumentException">If scene to unload is null.</exception>
		/// <exception cref="ArgumentException">If no scene with that name exists.</exception>
		protected static void UnloadPreviouScenes(SceneSO currentSceneSO, SceneSO previousSceneSO, Action sceneUnloadedCallback = null)
		{
			if (currentSceneSO == null)
			{
				throw new ArgumentException("SceneLoader: Current scene is null.");
			}
			if (previousSceneSO == null)
			{
				throw new ArgumentException("SceneLoader: Scene to unload is null.");
			}
			if (SceneManager.GetSceneByName(previousSceneSO.sceneName) == null)
			{
				throw new ArgumentException("SceneLoader: No scene with that name.");
			}

			if (!currentSceneSO.additionnalSceneNames.Contains(previousSceneSO.sceneName))
			{
				SceneLoader.UnloadScene(previousSceneSO.sceneName, sceneUnloadedCallback);
			}
			foreach (string oldSceneName in previousSceneSO.additionnalSceneNames)
			{
				if (oldSceneName != currentSceneSO.sceneName && !currentSceneSO.additionnalSceneNames.Contains(oldSceneName))
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

