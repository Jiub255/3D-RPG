using UnityEngine;

// Put this on Camera Follower
public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform _follow;

    [SerializeField]
    private Transform _lookAt;

    [SerializeField, Range(0.1f, 1.0f)]
    private float _smoothTime = 0.3f;

    private Vector3 _velocity = Vector3.zero;
    private Transform _transform;

    /*    [SerializeField]
        private float _maxSpeed = 25f;*/

    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        _transform.position = Vector3.SmoothDamp(
            _transform.position,
            _follow.position,
            ref _velocity,
            _smoothTime,
            /*_maxSpeed*/Mathf.Infinity,
            Time.unscaledDeltaTime);
        _transform.LookAt(_lookAt);
    }
}