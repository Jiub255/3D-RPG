using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;

    private Transform _transform;
    private Vector3 _movement;
    private Vector3 _forward;
    private Vector3 _right;
    private InputAction _movePlayerAction;

    private Animator _animator;

    private void Start()
    {
        _transform = transform;
        _forward = transform.forward;
        _right = transform.right;
        _movePlayerAction = S.I.IM.PC.World.MovePlayer;

        _animator = GetComponentInChildren<Animator>();

        CameraRotate.OnRotatedCamera += GetVectors;
    }

    private void OnDisable()
    {
        CameraRotate.OnRotatedCamera -= GetVectors;
    }

    private void Update()
    {
        Vector2 movementInput = _movePlayerAction.ReadValue<Vector2>();

        _movement = _forward * movementInput.y + _right * movementInput.x;
        _movement.Normalize();

        // Animation
        _animator.SetFloat("Speed", movementInput.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        _transform.position += _movement * Time.fixedDeltaTime * _speed;

        // TODO - Lerp instead of turn instantly?
        _transform.LookAt(transform.position + _movement);
    }

    private void GetVectors(Vector3 forward, Vector3 right)
    {
        _forward = forward;
        _right = right;
    }
}