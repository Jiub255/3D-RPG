using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "States/Player/Movement State", fileName = "Player Movement State")]
public class SOPlayerMovementState : SOState<PlayerCharacterController>
{
    public static event Action OnEnteredMovementState;

    [SerializeField]
    private float _speed = 10f;
    [SerializeField]
    private float _turnSpeed = 15f;

    private Transform _transform;
    private Vector3 _forward;
    private Vector3 _right;
    private InputAction _movePlayerAction;
    private bool _moving = false;
    private Rigidbody _rigidbody;

    // TODO - Do more things like this, with a separate plain c# class. Keeps things tidy. 
    // Animation
    private PlayerMovementAnimation _playerMovementAnimation;

    public override void Init(PlayerCharacterController parent)
    {
        base.Init(parent);

        // Reenable movement input.
        S.I.IM.PC.Movement.Enable();

        // References
        _transform = parent.transform;
        _rigidbody = parent.GetComponent<Rigidbody>();
        _movePlayerAction = S.I.IM.PC.Movement.MovePlayer;

        // Events
        CameraMoveRotate.OnRotatedCamera += GetVectors;
        S.I.IM.PC.Movement.MovePlayer.started += (c) => _moving = true;
        S.I.IM.PC.Movement.MovePlayer.canceled += (c) => _moving = false;
        S.I.IM.PC.Movement.Melee.started += ChangeToAttackState;

        // CameraMoveRotate hears this and sends back the GetVectors event. 
        OnEnteredMovementState?.Invoke();

        // Animation
        Animator animator = parent.Animator;
        Debug.Log($"Animator null: {animator == null}");
        Debug.Log($"InputAction: {_movePlayerAction.name}");
        _playerMovementAnimation = new(animator, _movePlayerAction);
    }

    public override void FixedUpdate()
    {
        if (_moving)
        {
            // Returning zero after entering movement state while holding down move key, but sometimes it doesn't which is weird. 
         //   Debug.Log($"Move action value: {_movePlayerAction.ReadValue<Vector2>()}");
            // Doing input in here instead of Update loop, hopefully to stop Quaternion.LookRotation() getting passed a zero vector after changing back to this state. 
            Vector2 movementInput = _movePlayerAction.ReadValue<Vector2>();
            Vector3 movement = _forward * movementInput.y + _right * movementInput.x;
            movement.Normalize();

            // Slerp look direction instead of turning instantly. 
            if (movement.sqrMagnitude > 0)
            {
                Quaternion lookRotation = Quaternion.LookRotation(movement);
                _transform.rotation = Quaternion.Slerp(_transform.rotation, lookRotation, Time.fixedDeltaTime * _turnSpeed);

                // Move player. 
                _rigidbody.MovePosition(_rigidbody.position + movement * Time.fixedDeltaTime * _speed);
            }
        }

        // Animation
        _playerMovementAnimation.FixedUpdate();
    }

    private void ChangeToAttackState(InputAction.CallbackContext context)
    {
        // Change to AttackState after pressing attack button. 
        _runner.ChangeState(typeof(SOPlayerAttackState));
    }

    private void GetVectors(Vector3 forward, Vector3 right)
    {
        _forward = forward;
        _right = right;
    }

    public override void Exit()
    {
        CameraMoveRotate.OnRotatedCamera -= GetVectors;
        S.I.IM.PC.Movement.MovePlayer.started -= (c) => _moving = true;
        S.I.IM.PC.Movement.MovePlayer.canceled -= (c) => _moving = false;
        S.I.IM.PC.Movement.Melee.started -= ChangeToAttackState;
    }

    public override void CaptureInput() {}
    public override void Update() {}
    public override void CheckForStateChangeConditions() {}
}