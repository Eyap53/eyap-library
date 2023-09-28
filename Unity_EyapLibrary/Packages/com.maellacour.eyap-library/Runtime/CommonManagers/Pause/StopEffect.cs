// namespace EyapLibrary.CommonManagers.Pause
// {
// 	using UnityEngine;
// 	using DG.Tweening;
// 	using UnityAtoms.BaseAtoms;

// 	public class StopEffect : MonoBehaviour
// 	{
// 		[SerializeField] private BoolEvent _isPausedVariable;

// 		[SerializeField, Tooltip("The time the pause input will take to ease from timescale 1 to 0")]
// 		private FloatReference _timeDicreasingTime;

// 		protected void OnEnable()
// 		{
// 			_isPausedVariable.Register(SetPlayPauseTime);
// 		}

// 		protected void OnDisable()
// 		{
// 			_isPausedVariable.Unregister(SetPlayPauseTime);
// 		}

// 		private void SetPlayPauseTime(bool isPause)
// 		{
// 			if (isPause)
// 			{
// 				DOTween.To(() => Time.timeScale, x => Time.timeScale = x, 0f, _timeDicreasingTime.Value);
// 			}
// 			else
// 			{
// 				DOTween.To(() => Time.timeScale, x => Time.timeScale = x, 1, _timeDicreasingTime.Value);
// 			}
// 		}
// 	}
// }
