using UnityEngine;

public class SunOrbit : MonoBehaviour
{
    [SerializeField]
    private float _rotationSpeed = 1f;

	private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    private void FixedUpdate()
    {
        _transform.RotateAround(Vector3.zero, Vector3.right, _rotationSpeed * Time.fixedDeltaTime);
    }
}