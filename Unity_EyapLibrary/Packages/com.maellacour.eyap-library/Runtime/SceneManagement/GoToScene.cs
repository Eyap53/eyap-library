namespace EyapLibrary.SceneManagement
{
	using UnityEngine;
	using EyapLibrary.SceneManagement;

	public class GoToScene : MonoBehaviour
	{
		[SerializeField] private SceneSO _sceneSO;

		public void Go()
		{
			if (SceneSwitcher.instanceExists)
			{
				SceneSwitcher.instance.SwitchScene(_sceneSO);
			}
		}
	}
}
