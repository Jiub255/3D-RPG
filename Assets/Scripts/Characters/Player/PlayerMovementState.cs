using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerMovementState : State<PlayerCharacterController2>
{
    public static event Action OnEnteredMovementState;

    [SerializeField]
    protected float _speed = 10f;
    [SerializeField]
    protected float _turnSpeed = 15f;

    protected Transform _transform;
/*    protected Vector3 _forward;
    protected Vector3 _right;*/
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
//        CameraMoveRotate.OnRotatedCamera += GetVectors;
        S.I.IM.PC.Movement.MovePlayer.started += (c) => _moving = true;
        S.I.IM.PC.Movement.MovePlayer.performed += (c) => _moving = false;
//        S.I.IM.PC.Movement.MovePlayer.performed += (C) => _moving = !_moving;
        S.I.IM.PC.Movement.Melee./*started*/performed += ChangeToAttackState;
        NPCDialog.OnInteractWithNPC += StartDialog;

        // TODO - GetVectors not getting called on re-entering movement state. Why? 
        // TODO - Keep Vectors in an SO? 
        // CameraMoveRotate hears this and sends back the GetVectors event. 
//        OnEnteredMovementState?.Invoke();

        // Animation
        Animator animator = parent.Animator;
        _playerMovementAnimation = new(animator, _movePlayerAction);

        // Reenable Nav Mesh Agent rotation after knockback. Stops player rotating after being hit. 
        _navMeshAgent.updateRotation = true;
    }

    protected void ToggleMovement()
    {
        _moving = !_moving;
        // Hopefully stops player from spinning while stopped. 
        _navMeshAgent.updateRotation = !_navMeshAgent.updateRotation;
    }

    public override void FixedUpdate()
    {
        if (_moving)
        {
            // Doing input in here instead of Update loop, hopefully to stop Quaternion.LookRotation()
            // getting passed a zero vector after changing back to this state. 
            Vector2 movementInput = _movePlayerAction.ReadValue<Vector2>();
//            Debug.Log($"Movement input: {movementInput}");
            Vector3 movement = _vectorsSO.Forward * movementInput.y + _vectorsSO.Right * movementInput.x;
//            Debug.Log($"Movement: {movement}");
            movement.Normalize();
//            Debug.Log($"Movement normalized: {movement}");

            // Slerp look direction instead of turning instantly. 
            if (movement.sqrMagnitude > 0)
            {
                Quaternion lookRotation = Quaternion.LookRotation(movement);
                _transform.rotation = Quaternion.Slerp(_transform.rotation, lookRotation, Time.fixedDeltaTime * _turnSpeed);

                // Move player. 
                _navMeshAgent.velocity = movement * _speed;
//                Debug.Log($"Moving == true. Nav Mesh Agent Velocity: {_navMeshAgent.velocity}");
            }
        }
        // Not sure if this is necessary, probably is though. 
        else
        {
            _navMeshAgent.velocity = Vector3.zero;
//            Debug.Log($"Moving == false. Nav Mesh Agent Velocity: {_navMeshAgent.velocity}");
        }

        // Animation
        _playerMovementAnimation.FixedUpdate();
    }

    protected void ChangeToAttackState(InputAction.CallbackContext context)
    {
        // Change to AttackState after pressing attack button. 
        _runner.ChangeState2(_runner.Attack());
    }

/*    protected void GetVectors(Vector3 forward, Vector3 right)
    {
        Debug.Log("GetVectors called.");
        _forward = forward;
        _right = right;
    }*/

    protected void StartDialog(Transform npcTransform)
    {
        _transform.LookAt(npcTransform);
        _runner.ChangeState2(_runner.Dialog());
    }

    public override void Exit()
    {
//        CameraMoveRotate.OnRotatedCamera -= GetVectors;
        S.I.IM.PC.Movement.MovePlayer.started -= (c) => _moving = true;
        S.I.IM.PC.Movement.MovePlayer.performed -= (c) => _moving = false;
//        S.I.IM.PC.Movement.MovePlayer.performed += (C) => _moving = !_moving;
        S.I.IM.PC.Movement.Melee./*started*/performed -= ChangeToAttackState;
        NPCDialog.OnInteractWithNPC -= StartDialog;

        // So you don't slide after transitioning to other states, especially attack. 
        _navMeshAgent.velocity = Vector3.zero;
    }

    public override void Update() {}
}