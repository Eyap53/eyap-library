namespace EyapLibrary.Samples.SceneManagementDemo
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using EyapLibrary.SceneManagement;

	public class GoScene : MonoBehaviour
	{
		[SerializeField]
		private SceneSO _sceneSO;

		public void GoToScene()
		{
			SceneSwitcher.instance.SwitchScene(_sceneSO);
		}
	}
}
