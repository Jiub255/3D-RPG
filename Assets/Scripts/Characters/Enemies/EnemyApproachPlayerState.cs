public class EnemyApproachPlayerState : State<EnemyController2>
{

    protected float _attackRadiusSquared;

    public EnemyApproachPlayerState(EnemyController2 parent, float attackRadius) : base(parent)
    {
        // Set animator bool to true.
        parent.Animator.SetBool("ApproachingPlayer", true);

        _attackRadiusSquared = attackRadius * attackRadius;
    }

    public override void Update() 
    {    
        if ((_runner.PlayerInstanceSO.PlayerInstanceTransform.position - _runner.transform.position).sqrMagnitude < _attackRadiusSquared)
        {
            _runner.ChangeStateTo(_runner.Attack());
        }
    }

    public override void FixedUpdate() 
    { 
        _runner.NavMeshAgent.SetDestination(_runner.PlayerInstanceSO.PlayerInstanceTransform.position);
        _runner.NavMeshAgent.transform.LookAt(_runner.PlayerInstanceSO.PlayerInstanceTransform.position);

        // Set speed to between 0 and 1 based on percent of max speed. 
        _runner.Animator.SetFloat("Speed", _runner.NavMeshAgent.velocity.magnitude / _runner.NavMeshAgent.speed);
    }

    public override void Exit() 
    {
        // Unset destination. Are both necessary? Could just ResetPath work? 
        _runner.NavMeshAgent.isStopped = true;
        _runner.NavMeshAgent.ResetPath();

        // Set animator bool to false.
        _runner.Animator.SetBool("ApproachingPlayer", false);
    }
}