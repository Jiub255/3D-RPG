using UnityEngine;
using UnityEngine.InputSystem;

public class OLDPlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed = 10f;
    [SerializeField]
    private float _turnSpeed = 15f;

    private Transform _transform;
    private Vector3 _movement;
    private Vector3 _forward;
    private Vector3 _right;
    private InputAction _movePlayerAction;
    private bool _moving = false;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _transform = transform;
        _forward = transform.forward;
        _right = transform.right;
        _movePlayerAction = S.I.IM.PC.Movement.MovePlayer;
        _rigidbody = GetComponent<Rigidbody>();

        CameraMoveRotate.OnRotatedCamera += GetVectors;

        S.I.IM.PC.Movement.MovePlayer.started += (c) => _moving = true;
        S.I.IM.PC.Movement.MovePlayer.canceled += (c) => _moving = false;
    }

    private void OnDisable()
    {
        CameraMoveRotate.OnRotatedCamera -= GetVectors;
   
        S.I.IM.PC.Movement.MovePlayer.started -= (c) => _moving = true;
        S.I.IM.PC.Movement.MovePlayer.canceled -= (c) => _moving = false;
    }

    private void FixedUpdate()
    {
        if (_moving)
        {
            // Get Input
            Vector2 movementInput = _movePlayerAction.ReadValue<Vector2>();
            _movement = _forward * movementInput.y + _right * movementInput.x;
            _movement.Normalize();

            // Lerp instead of turn instantly. 
            Quaternion lookRotation = Quaternion.LookRotation(_movement);
            _transform.rotation = Quaternion.Slerp(_transform.rotation, lookRotation, Time.fixedDeltaTime * _turnSpeed);

            _rigidbody.MovePosition(_rigidbody.position + _movement * Time.fixedDeltaTime * _speed);
        }
    }

    private void GetVectors(Vector3 forward, Vector3 right)
    {
        _forward = forward;
        _right = right;
    }
}