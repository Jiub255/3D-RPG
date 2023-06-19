using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "States/Enemy/Approach Player State", fileName = "Enemy Approach Player State")]
public class SOEnemyApproachPlayerState : SOState<EnemyController>
{
    [SerializeField]
    protected float _attackRadius = 2f;
    protected float _attackRadiusSquared { get { return _attackRadius * _attackRadius; } }

    public override void Init(EnemyController parent)
    {
        base.Init(parent);

        // Set animator bool to true.
        _runner.Animator.SetBool("ApproachingPlayer", true);
    }

    public override void CheckForStateChangeConditions() 
    {    
        if ((_runner.PlayerInstanceSO.PlayerInstanceTransform.position - _runner.transform.position).sqrMagnitude < _attackRadiusSquared)
        {
            _runner.ChangeState(typeof(SOEnemyAttackState));
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

    public override void CaptureInput() { }
    public override void Update() { }
}