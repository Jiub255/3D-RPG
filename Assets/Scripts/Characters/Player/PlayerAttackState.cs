using UnityEngine;

public class PlayerAttackState : State<PlayerCharacterController2>
{
    protected AnimationClip _attackAnimationClip;
    protected float _animationLength { get { return _attackAnimationClip.length; } }
    protected float _timer;

    public PlayerAttackState(PlayerCharacterController2 parent, AnimationClip attackAnimationClip) : base (parent)
    {
        _attackAnimationClip = attackAnimationClip;

        _timer = 0f;

        // Disable movement input while attacking. 
//        S.I.IM.DisableActionMap(S.I.IM.PC.Movement);

        // Start attack animation. 
        _runner.Animator.SetTrigger("Melee");
    }

/*    public override void Init(PlayerCharacterController2 parent)
    {
        base.Init(parent);

    }*/

    public override void Exit() 
    {
        // Reenable movement input.
//        S.I.IM.PC.Movement.Enable();
    }

    public override void Update() 
    {
        // Countdown to end of attack animation, when you go back to MovementState. 
        _timer += Time.deltaTime;
        if (_timer > _animationLength)
        {
            _runner.ChangeState2(_runner.Movement());
        }
    }

    public override void FixedUpdate() {}
}