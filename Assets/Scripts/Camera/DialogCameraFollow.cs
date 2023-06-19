using UnityEngine;

public class DialogCameraFollow : MonoBehaviour
{
	[SerializeField]
	private SOPlayerInstance _playerInstanceSO;
    private Transform _playerTransform;
/*    [SerializeField]
    private Vector3 _offset;*/
    private Transform _transform;
    private Transform _cameraLeaderTransform;

    private void Awake()
    {
        _playerTransform = _playerInstanceSO.PlayerInstanceTransform;
        _transform = transform;
        _cameraLeaderTransform = _playerInstanceSO.PlayerInstanceTransform.GetComponentInChildren<DialogCameraPosition>().transform;
    }

    private void FixedUpdate()
    {
        _transform.position = _playerTransform.TransformPoint(_cameraLeaderTransform.localPosition);
    }
}