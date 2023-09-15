namespace EyapLibrary.SceneManagement
{
	using System;
	using UnityEngine;
	using UnityEngine.SceneManagement;

	/// <summary>
	/// Allows a "cold start" in the editor, when pressing Play and not passing from the Initialisation scene.
	/// </summary>
	public class EditorColdStartup : MonoBehaviour
	{
#if UNITY_EDITOR
		[SerializeField] private SceneSO _thisSceneSO = default;

		protected void Awake()
		{
			SceneSwitcher.instance.SwitchScene(_thisSceneSO);
		}
#endif
	}
}
