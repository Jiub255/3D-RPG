using UnityEngine;

public class EnemyAttackState : State<EnemyController2>
{
    protected float _animationLength;

    // TODO - Set up multiple duplicate animations with animation event calling CheckForHits. 
    // Might just need one for player and one for enemies. 
    // Have each event call CheckForHits, the enemy one in MeleeAttack and the other in PlayerMeleeAttack.
    // Would this work? 
/*    [SerializeField, Tooltip("In Seconds")]
    protected float _timeUntilAttack = 0.5f;
    protected bool _hasAttacked;*/

    protected float _timer;

//    protected MeleeAttack _meleeAttack;

    public EnemyAttackState(EnemyController2 parent, AnimationClip attackAnimationClip) : base(parent)
    {
        _animationLength = attackAnimationClip.length;

        _timer = 0f;
//        _hasAttacked = false;
//        _meleeAttack = _runner.GetComponentInChildren<MeleeAttack>();        

        // Start attack animation. 
        parent.GetComponentInChildren<Animator>().SetTrigger("Attack");
    }

    public override void FixedUpdate() 
    {
        _runner.transform.LookAt(_runner.PlayerInstanceSO.PlayerInstanceTransform.position);
    }

    public override void Update()
    {
        // Go back to idle state when attack is finished. 
        _timer += Time.deltaTime;
        if (_timer > _animationLength)
        {
            _runner.ChangeStateTo(_runner.Idle());
        }

/*        if (_timer > _timeUntilAttack && !_hasAttacked)
        {
            _hasAttacked = true;
            _meleeAttack.CheckForHits();
        }*/
    }

    public override void Exit() {}
}