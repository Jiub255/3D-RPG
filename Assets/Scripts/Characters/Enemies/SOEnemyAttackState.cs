using UnityEngine;

[CreateAssetMenu(menuName = "States/Enemy/Attack State", fileName = "Enemy Attack State")]
public class SOEnemyAttackState : SOState<EnemyController>
{
    [SerializeField]
    protected AnimationClip _attackAnimationClip;
    protected float _animationLength { get { return _attackAnimationClip.length; } }

    // TODO - Set up multiple duplicate animations with animation event calling CheckForHits. 
    // Might just need one for player and one for enemies. 
    // Have each event call CheckForHits, the enemy one in MeleeAttack and the other in PlayerMeleeAttack.
    // Would this work? 
    [SerializeField, Tooltip("In Seconds")]
    protected float _timeUntilAttack = 0.58f;

    protected Animator _animator;
    protected float _timer;
    protected bool _hasAttacked;

    protected MeleeAttack _meleeAttack;

    public override void Init(EnemyController parent)
    {
        base.Init(parent);

        _timer = 0f;
        _hasAttacked = false;
        _meleeAttack = _runner.GetComponentInChildren<MeleeAttack>();        

        // Start attack animation. 
        _runner.GetComponentInChildren<Animator>().SetTrigger("Attack");
    }

    public override void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > _timeUntilAttack && !_hasAttacked)
        {
            _hasAttacked = true;
            _meleeAttack.CheckForHits();
        }
    }

    public override void CheckForStateChangeConditions()
    {
        // Go back to idle state when attack is finished. 
        if (_timer > _animationLength)
        {
            _runner.ChangeState(typeof(SOEnemyIdleState));
        }
    }
    
    public override void FixedUpdate() 
    {
        _runner.transform.LookAt(_runner.PlayerInstanceSO.PlayerInstanceTransform.position);
    }

    public override void CaptureInput() {}
    public override void Exit() {}
}