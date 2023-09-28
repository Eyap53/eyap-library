namespace EyapLibrary.CommonManagers.Pause
{
	using UnityEngine;
	using UnityAtoms.BaseAtoms;

	public class PauseMenu : MonoBehaviour
	{
		[SerializeField] private GameObject _pauseMenu;

		[SerializeField] private BoolVariable _isPausedVariable;
		[SerializeField] private BoolEvent _isPausedEvent;

		protected void OnEnable()
		{
			PauseGame(_isPausedVariable.Value);
			_isPausedEvent.Register(PauseGame);
		}

		protected void OnDisable()
		{
			_isPausedEvent.Unregister(PauseGame);
		}

		private void PauseGame(bool isPause)
		{
			_pauseMenu.SetActive(isPause);
		}
	}
}
