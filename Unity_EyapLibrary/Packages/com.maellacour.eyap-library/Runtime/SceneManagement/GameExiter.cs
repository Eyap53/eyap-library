namespace EyapLibrary.SceneManagement
{
	using UnityEngine;

	/// <summary>
	/// Simple class to exit the game, in editor and in play.
	/// </summary>
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

