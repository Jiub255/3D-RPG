using System;
using UnityEngine;

public class SunOrbit : MonoBehaviour
{
    // TODO - Change sun starting position so that it rotates around player if it's tilted. Gonna be mathy.
    // Based off of _rotationAngle and _sunDistance. 
    [SerializeField]
    private float _sunDistance = 800f;

    [SerializeField]
    private float _rotationAngle = 23.4f;
    private Vector3 _rotationAxis { get { return new Vector3(1f, Mathf.Tan(_rotationAngle * Mathf.Deg2Rad), 0f); } }

    private float _rotationSpeed { get { return _gameTimeMultiplier / 240f; } }

	private Transform _transform;
    private float _gameTimeMultiplier;

    private void Awake()
    {
        _transform = transform;

        GameManager.OnGameTimeMultiplierChanged += UpdateGameTimeMultiplier;

        SetSunStartingPosition();
    }

    private void Start()
    {
        UpdateGameTimeMultiplier();

        Debug.Log($"Rotation Angle: {_rotationAngle}");
        Debug.Log($"Rotation Axis: {_rotationAxis}");
    }

    // Sets sun at noon position based on rotation angle and sun distance. 
    private void SetSunStartingPosition()
    {
        _transform.position = new Vector3(
            -1 * _sunDistance * Mathf.Sin(_rotationAngle * Mathf.Deg2Rad), 
            _sunDistance * Mathf.Cos(_rotationAngle * Mathf.Deg2Rad), 
            0f);
    }

    private void OnDisable()
    {
        GameManager.OnGameTimeMultiplierChanged -= UpdateGameTimeMultiplier;
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