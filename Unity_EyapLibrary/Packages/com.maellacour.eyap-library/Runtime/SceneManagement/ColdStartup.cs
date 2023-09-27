namespace EyapLibrary.SceneManagement
{
	using UnityEngine;

	/// <summary>
	/// Class only for the editor, to start a scene from the current active scene, and not having to start from Initialisation scene.
	/// There must be a SceneSwitcher next to it, with a currentlyLoadedScene field empty.
	/// </summary>
	[RequireComponent(typeof(SceneSwitcher))]
	public class ColdStartup : MonoBehaviour
	{
#if UNITY_EDITOR
		[SerializeField] protected SceneSO _currentScene;

		protected void Awake()
		{
			SceneSwitcher sceneSwitcher = GetComponentInChildren<SceneSwitcher>();

			sceneSwitcher.SwitchScene(_currentScene, isColdStartup: true);
		}
#endif
	}
}
