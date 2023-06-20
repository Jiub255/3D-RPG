using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "States/Player/Knockback State", fileName = "Player Knockback State")]
public class SOPlayerKnockbackState : SOState<PlayerCharacterController>
{
    [SerializeField]
    protected float _knockbackDuration = 0.5f;
    protected float _timer;
    protected NavMeshAgent _navMeshAgent;
    protected float _speed;
    protected float _angularSpeed;
    protected float _acceleration;

    public override void Init(PlayerCharacterController parent)
    {
        base.Init(parent);

        _timer = 0f;
        _navMeshAgent = _runner.NavMeshAgent;

        // Save original values of NavMeshAgent. 
        _speed = _navMeshAgent.speed;
        _angularSpeed = _navMeshAgent.angularSpeed;
        _acceleration = _navMeshAgent.acceleration;

        // Set new values for knockback. 
        _navMeshAgent.speed = 10;
        // Keeps the enemy facing forwad instead of spinning. 
        _navMeshAgent.angularSpeed = 0;
        _navMeshAgent.acceleration = 20;
        //        _runner.Rigidbody.AddForce(_runner.KnockbackVector, ForceMode.Impulse);

        // Have to do this before disabling movement. Terrible bug fix. 
        /*        _runner.ACoupleOfStupidFixesThatIHate();
                S.I.IM.PC.Movement.Disable();*/

        S.I.IM.DisableActionMap(S.I.IM.PC.Movement);

        // Animation
        _runner.Animator.SetTrigger("GetHit");
    }

    public override void CheckForStateChangeConditions()
    {
        _timer += Time.deltaTime;
        if (_timer > _knockbackDuration)
        {
            _runner.ChangeState(typeof(SOPlayerMovementState));
        }
    }

    public override void FixedUpdate()
    {
        _navMeshAgent.velocity = _runner.KnockbackVector;
    }

    public override void Exit() 
    {
        // Reenable movement input.
        S.I.IM.PC.Movement.Enable();

        // Set back to default settings. 
        _navMeshAgent.speed = _speed;
        _navMeshAgent.angularSpeed = _angularSpeed;
        _navMeshAgent.acceleration = _acceleration;

        // Set velocity to zero to lessen sliding. 
        _navMeshAgent.velocity = Vector3.zero;

        // Stop Nav Mesh Agent rotation after knockback. Stops player rotating after being hit. 
        _navMeshAgent.updateRotation = false;
    }

    public override void CaptureInput() {}
    public override void Update() {}
}