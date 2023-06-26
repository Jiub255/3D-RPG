using UnityEngine;
using UnityEngine.AI;

public class PlayerKnockbackState : State<PlayerCharacterController2>
{
    protected float _knockbackDuration = 0.5f;
    protected Vector3 _knockbackVector;
    protected float _timer;
    protected NavMeshAgent _navMeshAgent;
    protected Rigidbody _rigidbody;
    protected float _speed;
    protected float _angularSpeed;
    protected float _acceleration;

    public PlayerKnockbackState(PlayerCharacterController2 parent, float knockbackDuration, Vector3 knockbackVector) : base(parent)
    {
        _knockbackDuration = knockbackDuration;
        _knockbackVector = knockbackVector;

        _timer = 0f;
        _navMeshAgent = _runner.NavMeshAgent;
        _rigidbody = _runner.GetComponent<Rigidbody>();   

        // Save original values of NavMeshAgent. 
        _speed = _navMeshAgent.speed;
        _angularSpeed = _navMeshAgent.angularSpeed;
        _acceleration = _navMeshAgent.acceleration;

        // Set new values for knockback. 
        _navMeshAgent.speed = 10;
        // Keeps the enemy facing forwad instead of spinning. 
        _navMeshAgent.angularSpeed = 0;
        _navMeshAgent.acceleration = 20;


        // Disable movement input while being knocked back. 
//        S.I.IM.DisableActionMap(S.I.IM.PC.Movement);

        // Animation
        _runner.Animator.SetTrigger("GetHit");
    }

    public override void Update() 
    {
        _timer += Time.deltaTime;
        if (_timer > _knockbackDuration)
        {
            _runner.ChangeStateTo(_runner.Movement());
        }
    }

    public override void FixedUpdate()
    {
        _navMeshAgent.velocity = _knockbackVector;
    }

    public override void Exit() 
    {
        // Reenable movement input.
//        S.I.IM.PC.Movement.Enable();

        // Set back to default settings. 
        _navMeshAgent.speed = _speed;
        _navMeshAgent.angularSpeed = _angularSpeed;
        _navMeshAgent.acceleration = _acceleration;

        // Set velocity to zero to lessen sliding. 
        _navMeshAgent.velocity = Vector3.zero;

        // Set rigidbody rotation to zero to stop spinning.
        _rigidbody.angularVelocity = Vector3.zero;
    }
}