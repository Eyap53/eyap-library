namespace EyapLibrary.CommonManagers
{
	using UnityEngine;
	using System.Collections;
	using System.Collections.Generic;
	using EyapLibrary.Utils;

	public class PauseManager : Singleton<PauseManager>
	{
		#region Serialized variables

		[Header("References")]
		[SerializeField] private GameObject _pauseMenu;

		public bool isPaused { get; protected set; } = false;

		#endregion

		#region Private variables
		#endregion

		#region Properties
		#endregion

		#region Unity callbacks

		protected override void Awake()
		{
			base.Awake();
			PauseGame(false);
		}

		#endregion

		#region Public methods

		public void TogglePauseMenu()
		{
			isPaused ^= true; // invert
			PauseGame(isPaused);
		}

		#endregion

		#region Private helpers

		private void PauseGame(bool isPause)
		{
			_pauseMenu.SetActive(isPause);
		}

		#endregion
	}
}
