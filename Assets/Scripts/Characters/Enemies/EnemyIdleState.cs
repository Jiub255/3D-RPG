public class EnemyIdleState : State<EnemyController2>
{
    /*    [SerializeField]
        protected float _chaseRadius = 5f;*/
    protected float _chaseRadiusSquared;

    public EnemyIdleState(EnemyController2 parent, float chaseRadius) : base(parent)
    {
        parent.Animator.SetFloat("Speed", 0f);
        _chaseRadiusSquared = chaseRadius * chaseRadius;
    }

    public override void Update()
    {
        // Check if player is within sight range. If so, change to EnemyApproachPlayerState. 
        if ((_runner.PlayerInstanceSO.PlayerInstanceTransform.transform.position - _runner.transform.position).sqrMagnitude < _chaseRadiusSquared)
        {
            _runner.ChangeStateTo(_runner.ApproachPlayer());
        }
    }

    public override void FixedUpdate() {}
    public override void Exit() {}
}