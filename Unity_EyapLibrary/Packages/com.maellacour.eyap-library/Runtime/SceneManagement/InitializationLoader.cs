namespace EyapLibrary.SceneManagement
{
	using UnityEngine;
	using UnityEngine.SceneManagement;

	/// <summary>
	/// This class is responsible for starting the game by loading the persistent managers scene
	/// and raising the event to load the Main Menu.
	/// </summary>

	public class InitializationLoader : MonoBehaviour
	{
		public SceneSO sceneToLoad;
		protected void Start()
		{
			SceneSwitcher.instance.SwitchScene(sceneToLoad);
		}
	}
}
