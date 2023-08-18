namespace EyapLibrary.AtomsHelpers
{
	using UnityEngine;
	using UnityAtoms.BaseAtoms;

	public class GameObjectReferencer : MonoBehaviour
	{
		[SerializeField] private GameObjectVariable _gameObjectVariableRef;

		void Awake()
		{
			if (_gameObjectVariableRef.Value != null)
			{
				Debug.LogWarning("GameObjectReferencer: GameObject variable was already set. Overwriting.");
			}
			_gameObjectVariableRef.SetValue(gameObject);
		}
	}
}
