namespace EyapLibrary.SceneManagement
{
	using UnityEngine;

	public class GameExiter : MonoBehaviour
	{
		/// <summary>
		/// Exits the game.
		/// </summary>
		/// <remarks>
		/// This method is safe to call from the editor.
		/// </remarks>
		public void ExitGame()
		{
#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
#endif
			Application.Quit();
		}
	}
}

