using UnityEngine;

[CreateAssetMenu(menuName = "States/Enemy/Attack State", fileName = "Enemy Attack State")]
public class SOEnemyAttackState : SOState<EnemyController>
{
    [SerializeField]
    protected AnimationClip _attackAnimationClip;

    protected Animator _animator;
    protected float _timer;

    public override void Init(EnemyController parent)
    {
        base.Init(parent);

        // Set timer to attack animation length. 
        _timer = _attackAnimationClip.length;

        // Start attack animation. 
        _runner.GetComponentInChildren<Animator>().SetTrigger("Attack");
    }

    public override void CheckForStateChangeConditions()
    {
        // Go back to idle state when attack is finished. 
        _timer -= Time.deltaTime;
        if (_timer < 0)
        {
            _runner.ChangeState(typeof(SOEnemyIdleState));
        }
    }

    public override void CaptureInput() {}
    public override void Update() {}
    public override void FixedUpdate() {}
    public override void Exit() {}
}