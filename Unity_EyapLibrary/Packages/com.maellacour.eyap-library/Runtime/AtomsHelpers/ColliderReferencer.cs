namespace EyapLibrary.AtomsHelpers
{
	using UnityEngine;
	using UnityAtoms.BaseAtoms;

	public class ColliderReferencer : MonoBehaviour
	{
		[Tooltip("Optionnal")]
		[SerializeField] private Collider _collider;
		[SerializeField] private ColliderVariable _colliderVariableRef;

		void Awake()
		{
			if (_collider == null)
			{
				_collider = GetComponent<Collider>();
			}
			if (_colliderVariableRef.Value != null)
			{
				Debug.LogWarning("GameObjectReferencer: GameObject variable was already set. Overwriting.");
			}
			_colliderVariableRef.SetValue(_collider);
		}
	}
}
