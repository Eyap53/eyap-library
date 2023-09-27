namespace EyapLibrary.SceneManagement
{
	using UnityEngine;

	/// <summary>
	/// This class is responsible for starting the game by loading the persistent managers scene
	/// and raising the event to load the Main Menu.
	/// </summary>

	public class InitializationLoader : MonoBehaviour
	{
		[SerializeField] private SceneSO _sceneToLoad;

		protected void Start()
		{
			SceneSwitcher.instance.SwitchScene(_sceneToLoad);
		}
	}
}
