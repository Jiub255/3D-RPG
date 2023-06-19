using UnityEngine;

public class DialogCameraFollow : MonoBehaviour
{
	[SerializeField]
	private SOPlayerInstance _playerInstanceSO;
    private Transform _playerTransform;
    [SerializeField]
    private Vector3 _offset;
    private Transform _transform;

    private void Awake()
    {
        _playerTransform = _playerInstanceSO.PlayerInstanceTransform;
        _transform = transform;
    }

    private void FixedUpdate()
    {
        _transform.position = _playerTransform.TransformPoint(_offset);
    }
}