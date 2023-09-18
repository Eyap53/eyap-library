using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EyapLibrary.SceneManagement;

public class GoScene : MonoBehaviour
{
	[SerializeField]
	private SceneSO _sceneSO;

	// Start is called before the first frame update
	[Button]
	void GoToScene()
	{
		SceneSwitcher.instance.SwitchScene(_sceneSO);
	}
}
