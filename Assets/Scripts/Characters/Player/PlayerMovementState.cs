using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerMovementState : State<PlayerCharacterController2>/*, IMovementActions*/ // How to use this interface?
{
//    public static event Action OnEnteredMovementState;

    [SerializeField]
    protected float _speed = 10f;
    [SerializeField]
    protected float _turnSpeed = 15f;

    protected Transform _transform;
    protected InputAction _movePlayerAction;
    protected bool _moving = false;
    protected NavMeshAgent _navMeshAgent;
    protected SOVectors _vectorsSO;

    // TODO - Do more things like this, with a separate plain c# class. Keeps things tidy. 
    // Animation
    protected PlayerMovementAnimation _playerMovementAnimation;

    public PlayerMovementState(PlayerCharacterController2 parent, float speed, float turnSpeed) : base(parent)
    {
        _speed = speed;
        _turnSpeed = turnSpeed;

        // References
        _transform = parent.transform;
        _movePlayerAction = S.I.IM.PC.Movement.MovePlayer;
        _navMeshAgent = _runner.NavMeshAgent;
        _vectorsSO = parent.VectorsSO;

        // Events
        S.I.IM.PC.Movement.MovePlayer.started += (c) => _moving = true;
        S.I.IM.PC.Movement.MovePlayer.performed += (c) => _moving = false;
        S.I.IM.PC.Movement.Melee.performed += ChangeToAttackState;
        NPCDialog.OnInteractWithNPC += StartDialog;

        // Initial state check for input.
        _moving = S.I.IM.PC.Movement.MovePlayer.IsPressed() ? true : false;

        // Animation
        Animator animator = parent.Animator;
        _playerMovementAnimation = new(animator, _movePlayerAction);
    }

/*    protected void ToggleMovement()
    {
        _moving = !_moving;
        // Hopefully stops player from spinning while stopped. 
        _navMeshAgent.updateRotation = !_navMeshAgent.updateRotation;
    }*/

    public override void FixedUpdate()
    {
        if (_moving)
        {
            // Doing input in here instead of Update loop, hopefully to stop Quaternion.LookRotation()
            // getting passed a zero vector after changing back to this state. 
            Vector2 movementInput = _movePlayerAction.ReadValue<Vector2>();
            Vector3 movement = _vectorsSO.Forward * movementInput.y + _vectorsSO.Right * movementInput.x;
            movement.Normalize();

            // Slerp look direction instead of turning instantly. 
            if (movement.sqrMagnitude > 0)
            {
                Quaternion lookRotation = Quaternion.LookRotation(movement);
                _transform.rotation = Quaternion.Slerp(_transform.rotation, lookRotation, Time.fixedDeltaTime * _turnSpeed);

                // Move player. 
                _navMeshAgent.velocity = movement * _speed;
            }
        }
        // Not sure if this is necessary, probably is though. 
        else
        {
            _navMeshAgent.velocity = Vector3.zero;
        }

        // Animation
        _playerMovementAnimation.FixedUpdate();
    }

    protected void ChangeToAttackState(InputAction.CallbackContext context)
    {
        // Change to AttackState after pressing attack button. 
        _runner.ChangeState2(_runner.Attack());
    }

    protected void StartDialog(Transform npcTransform)
    {
        _transform.LookAt(npcTransform);
        _runner.ChangeState2(_runner.Dialog());
    }

    public override void Exit()
    {
        S.I.IM.PC.Movement.MovePlayer.started -= (c) => _moving = true;
        S.I.IM.PC.Movement.MovePlayer.performed -= (c) => _moving = false;
        S.I.IM.PC.Movement.Melee.performed -= ChangeToAttackState;
        NPCDialog.OnInteractWithNPC -= StartDialog;

        // So you don't slide after transitioning to other states, especially attack. 
        _navMeshAgent.velocity = Vector3.zero;
    }

    public override void Update() {}
}