using UnityEngine;

[CreateAssetMenu(menuName = "States/Enemy/Idle State", fileName = "Enemy Idle State")]
public class SOEnemyIdleState : SOState<EnemyController>
{
    [SerializeField]
    protected float _chaseRadius = 5f;
    protected float _chaseRadiusSquared { get { return _chaseRadius * _chaseRadius; } }

    public override void Init(EnemyController parent)
    {
        base.Init(parent);

        _runner.Animator.SetFloat("Speed", 0f);
    }

    public override void CheckForStateChangeConditions()
    {
        // Check if player is within sight range. If so, change to EnemyApproachPlayerState. 
        if ((_runner.PlayerInstanceSO.PlayerInstanceTransform.transform.position - _runner.transform.position).sqrMagnitude < _chaseRadiusSquared)
        {
            _runner.ChangeState(typeof(SOEnemyApproachPlayerState));
        }
    }

    public override void Update() {}
    public override void CaptureInput() {}
    public override void FixedUpdate() {}
    public override void Exit() {}
}