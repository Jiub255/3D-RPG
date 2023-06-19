using UnityEngine;

public class SunOrbit : MonoBehaviour
{
    [SerializeField]
    // Earth on the equinox? solstice? On the equator? Not really sure.  
    private float _rotationAngle = 23.4f;
    // Translates angle into a rotation axis for RotateAround. 
    // TODO - Change sun starting position so that it rotates around player if it's tilted. Gonna be mathy. 
    private Vector3 _rotationAxis { get { return new Vector3(1f, Mathf.Tan(_rotationAngle * Mathf.Deg2Rad), 0f); } }
    private float _rotationSpeed { get { return _gameTimeMultiplier / 240f; } }

	private Transform _transform;
    private float _gameTimeMultiplier;

    private void Awake()
    {
        _transform = transform;

        GameManager.OnGameTimeMultiplierChanged += UpdateGameTimeMultiplier;

        // TODO - Set sun position so that it rotates around player, based off rotation angle/axis.
    }

    private void OnDisable()
    {
        GameManager.OnGameTimeMultiplierChanged -= UpdateGameTimeMultiplier;
    }

    private void Start()
    {
        UpdateGameTimeMultiplier();
    }

    private void UpdateGameTimeMultiplier()
    {
        _gameTimeMultiplier = S.I.GameManager.GetGameTimeMultiplier();
    }

    private void FixedUpdate()
    {
        _transform.RotateAround(Vector3.zero, _rotationAxis, _rotationSpeed * Time.fixedDeltaTime);
    }
}