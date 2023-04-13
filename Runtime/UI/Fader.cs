namespace EyapLibrary.UI
{
	using System.Collections;
	using UnityEngine;
	using UnityEngine.UI;
	using UnityAtoms.BaseAtoms;

	public class Fader : MonoBehaviour
	{
		[SerializeField] private Image _fadeImage;
		[SerializeField] private float _fadeDuration;

		[SerializeField] private BoolEvent _fadeCompleted;

		public void Fade(bool isFadingIn) => StartCoroutine(FadeCoroutine(isFadingIn));
		/// <summary>
		/// Fade from clear to black.
		/// </summary>
		public void FadeIn() => StartCoroutine(FadeCoroutine(true));
		/// <summary>
		/// Fade from black to clear.
		/// </summary>
		public void FadeOut() => StartCoroutine(FadeCoroutine(false));
		public void FadeInOut()
		{
			FadeIn();
			FadeOut();
		}
		public void FadeOutIn()
		{
			FadeOut();
			FadeIn();
		}

		IEnumerator FadeCoroutine(bool isFadeIn)
		{
			for (float i = 0; i <= 1; i += Time.deltaTime / _fadeDuration)
			{
				_fadeImage.color = new Color(0, 0, 0, isFadeIn ? i : 1 - i);
				yield return null;
			}
			_fadeImage.color = new Color(0, 0, 0, isFadeIn ? 1 : 0);

			_fadeCompleted.Raise(isFadeIn);
		}

	}
}
