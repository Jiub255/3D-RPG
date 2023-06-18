using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "States/Enemy/Approach Player State", fileName = "Enemy Approach Player State")]
public class SOEnemyApproachPlayerState : SOState<EnemyController>
{
    [SerializeField]
    protected float _attackRadius = 2f;
    protected float _attackRadiusSquared { get { return _attackRadius * _attackRadius; } }

    protected NavMeshAgent _navMeshAgent;

    public override void Init(EnemyController parent)
    {
        base.Init(parent);

        _navMeshAgent = _runner.GetComponent<NavMeshAgent>();

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
        _navMeshAgent.SetDestination(_runner.PlayerInstanceSO.PlayerInstanceTransform.position);
    }

    public override void Exit() 
    {
        // Unset destination. Are both necessary? Could just ResetPath work? 
        _navMeshAgent.isStopped = true;
        _navMeshAgent.ResetPath();

        // Set animator bool to false.
        _runner.Animator.SetBool("ApproachingPlayer", false);
    }

    public override void CaptureInput() { }
    public override void Update() { }
}