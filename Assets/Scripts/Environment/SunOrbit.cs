using UnityEngine;

public class SunOrbit : MonoBehaviour
{
    [SerializeField]
    private float _rotationSpeed = 1f;
    [SerializeField]
    // Corresponds to a 23.5 degree angle of the planet's axis. 
    // Earth on the equinox? solstice? whatever. 
    private Vector3 _rotationAxis = new Vector3(1f, 0.4348f, 0f);

	private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    private void FixedUpdate()
    {
        _transform.RotateAround(Vector3.zero, _rotationAxis, _rotationSpeed * Time.fixedDeltaTime);
    }
}