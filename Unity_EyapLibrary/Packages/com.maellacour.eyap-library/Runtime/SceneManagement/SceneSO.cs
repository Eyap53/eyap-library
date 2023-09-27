namespace EyapLibrary.SceneManagement
{
	using UnityEngine;

	/// <summary>
	/// Class containing the scene to load information, including the additionnal scene to load.
	/// </summary>
	/// <remarks>
	/// This class is used by the <see cref="SceneSwitcher"/> class.
	/// </remarks>
	/// <seealso cref="SceneSwitcher"/>
	[CreateAssetMenu(fileName = "Scene", menuName = "EypaLibrary/Scene SO")]
	public class SceneSO : ScriptableObject
	{
		/// <summary>
		/// The main scene that needs to be loaded.
		/// </summary>
		[SerializeField]
		public string sceneName;

		/// <summary>
		/// The additionnal scenes that also need to be loaded.
		/// </summary>
		[SerializeField]
		public string[] additionnalSceneNames;
	}
}
