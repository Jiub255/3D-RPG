using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
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

    private void Start()
    {
        _transform = transform;
        _forward = transform.forward;
        _right = transform.right;
        _movePlayerAction = S.I.IM.PC.World.MovePlayer;

        CameraRotate.OnRotatedCamera += GetVectors;

        S.I.IM.PC.World.MovePlayer.started += (c) => _moving = true;
        S.I.IM.PC.World.MovePlayer.canceled += (c) => _moving = false;
    }

    private void OnDisable()
    {
        CameraRotate.OnRotatedCamera -= GetVectors;
   
        S.I.IM.PC.World.MovePlayer.started -= (c) => _moving = true;
        S.I.IM.PC.World.MovePlayer.canceled -= (c) => _moving = false;
    }

    private void Update()
    {
        if (_moving)
        {
            Vector2 movementInput = _movePlayerAction.ReadValue<Vector2>();

            _movement = _forward * movementInput.y + _right * movementInput.x;
            _movement.Normalize();
        }
    }

    private void FixedUpdate()
    {
        if (_moving)
        {
            // TODO - Move with rigidbody instead? 
            _transform.position += _movement * Time.fixedDeltaTime * _speed;

            // Lerp instead of turn instantly. 
            Quaternion lookRotation = Quaternion.LookRotation(_movement);
            _transform.rotation = Quaternion.Slerp(_transform.rotation, lookRotation, Time.fixedDeltaTime * _turnSpeed);
        }
    }

    private void GetVectors(Vector3 forward, Vector3 right)
    {
        _forward = forward;
        _right = right;
    }
}