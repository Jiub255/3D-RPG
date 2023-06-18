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
    private Vector3 _movement;
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

        _transform = parent.transform;
/*        _forward = parent.transform.forward;
        _right = parent.transform.right;*/
        _rigidbody = parent.GetComponent<Rigidbody>();
        _movePlayerAction = S.I.IM.PC.Movement.MovePlayer;

        CameraMoveRotate.OnRotatedCamera += GetVectors;

        S.I.IM.PC.Movement.MovePlayer.started += (c) => _moving = true;
        S.I.IM.PC.Movement.MovePlayer.canceled += (c) => _moving = false;

        // Changed to cancel to try to fix bug. 
        S.I.IM.PC.Movement.Melee.started/*canceled*/ += ChangeToAttackState;

        // CameraMoveRotate hears this and sends back the GetVectors event. 
        OnEnteredMovementState?.Invoke();

        // Animation
        _playerMovementAnimation = new(_runner.Animator, _movePlayerAction);
    }

    public override void FixedUpdate()
    {
        if (_moving)
        {
            // Returning zero after entering movement state while holding down move key, but sometimes it doesn't which is weird. 
         //   Debug.Log($"Move action value: {_movePlayerAction.ReadValue<Vector2>()}");
            // Doing input in here instead of Update loop, hopefully to stop Quaternion.LookRotation() getting passed a zero vector after changing back to this state. 
            Vector2 movementInput = _movePlayerAction.ReadValue<Vector2>();
            _movement = _forward * movementInput.y + _right * movementInput.x;
            _movement.Normalize();

            // Slerp look direction instead of turning instantly. 
            if (_movement.sqrMagnitude > 0)
            {
                Quaternion lookRotation = Quaternion.LookRotation(_movement);
                _transform.rotation = Quaternion.Slerp(_transform.rotation, lookRotation, Time.fixedDeltaTime * _turnSpeed);

                // Move player. 
                _rigidbody.MovePosition(_rigidbody.position + _movement * Time.fixedDeltaTime * _speed);
            }
        }

        // Animation
        _playerMovementAnimation.FixedUpdate();
    }

    private void ChangeToAttackState(InputAction.CallbackContext context)
    {
        // Doesn't help. 
        //_movePlayerAction.Reset();

        // Change to AttackState after pressing attack button. 
        _runner.ChangeState(typeof(SOPlayerAttackState));
    }

    // TODO - Have ChangeToKnockbackState method get called by event from wherever it should. 
    private void ChangeToKnockbackState()
    {
        // Change to KnockbackState after getting hit. 
        //_runner.SetState(typeof(SOKnockbackState));
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