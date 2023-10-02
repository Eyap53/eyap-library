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

		private SceneSO _nextSceneSO;
		private bool _isColdStartup;
		private Scene _transitionScene;

		private const string SceneTransitionLabel = "TransitionScene";

		public bool ReloadScene()
		{
			if (CurrentlyLoadedScene == null)
			{
				throw new Exception("SceneSwitcher: Current scene is null.");
			}
			return SwitchScene(CurrentlyLoadedScene);
		}


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
				throw new ArgumentException("SceneSwitcher: No sceneSO provided.");
			}
			if (SceneManager.GetSceneByName(sceneSO.sceneName) == null)
			{
				throw new ArgumentException("SceneSwitcher: No scene with that name.");
			}
			if (CurrentlySwitchingScene)
			{
				Debug.LogWarning("SceneSwitcher: Scene currently loading");
				return false;
			}

			CurrentlySwitchingScene = true;
			_nextSceneSO = sceneSO;
			_isColdStartup = isColdStartup;

			if (CurrentlyLoadedScene != null)
			{
				_transitionScene = SceneManager.CreateScene(SceneTransitionLabel);
				SceneManager.SetActiveScene(_transitionScene);
				UnloadPreviouScenes(_nextSceneSO, CurrentlyLoadedScene, OnPreviousScenesUnloaded);
			}
			else
			{
				OnPreviousScenesUnloaded();
			}

			// if (!isColdStartup)
			// {
			// 	SceneLoader.LoadScene(sceneSO.sceneName, () => OnMainSceneSwitch(sceneSO));
			// }
			// else
			// {
			// 	OnMainSceneSwitch(sceneSO, isColdStartup);
			// }

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
				throw new ArgumentException("SceneSwitcher: Current scene is null.");
			}
			if (previousSceneSO == null)
			{
				throw new ArgumentException("SceneSwitcher: Scene to unload is null.");
			}
			if (SceneManager.GetSceneByName(previousSceneSO.sceneName) == null)
			{
				throw new ArgumentException("SceneSwitcher: No scene with that name.");
			}

			foreach (string oldSceneName in previousSceneSO.additionnalSceneNames)
			{
				if (oldSceneName != currentSceneSO.sceneName && !currentSceneSO.additionnalSceneNames.Contains(oldSceneName))
				{
					SceneLoader.UnloadScene(oldSceneName);
				}
			}
			if (!currentSceneSO.additionnalSceneNames.Contains(previousSceneSO.sceneName))
			{
				SceneLoader.UnloadScene(previousSceneSO.sceneName, sceneUnloadedCallback);
			}
		}

		/// <summary>
		/// Called when previous scenes unloaded.
		/// </summary>
		protected void OnPreviousScenesUnloaded()
		{
			SceneLoader.LoadScene(_nextSceneSO.sceneName, OnSceneSwitchEnd);
		}

		/// <summary>
		/// Called when the scene is switched.
		/// </summary>
		/// <param name="operation">The async operation that loaded the scene.</param>
		protected void OnSceneSwitchEnd()
		{
			SceneSO previousSceneSO = CurrentlyLoadedScene;
			CurrentlyLoadedScene = _nextSceneSO;
			if (!_isColdStartup)
			{
				SceneManager.SetActiveScene(SceneManager.GetSceneByName(CurrentlyLoadedScene.sceneName));
			}

			if (_transitionScene != null && _transitionScene.isLoaded)
			{
				SceneManager.UnloadSceneAsync(_transitionScene);
			}
			CurrentlySwitchingScene = false;
		}
	}
}

