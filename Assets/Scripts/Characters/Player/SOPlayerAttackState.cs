using UnityEngine;

[CreateAssetMenu(menuName = "States/Player/Attack State", fileName = "Player Attack State")]
public class SOPlayerAttackState : SOState<PlayerCharacterController>
{
    [SerializeField]
    protected AnimationClip _attackAnimationClip;
    protected float _animationLength { get { return _attackAnimationClip.length; } }
    protected float _timer;

    public override void Init(PlayerCharacterController parent)
    {
        base.Init(parent);

        _timer = 0f;

        // Disable movement input while attacking. 
        S.I.IM.DisableActionMap(S.I.IM.PC.Movement);

        // Start attack animation. 
        _runner.Animator.SetTrigger("Melee");
    }

    public override void CheckForStateChangeConditions()
    {
        // Countdown to end of attack animation, when you go back to MovementState. 
        _timer += Time.deltaTime;
        if (_timer > _animationLength)
        {
            _runner.ChangeState(typeof(SOPlayerMovementState));
        }
    }

    public override void Exit() 
    {
        // Reenable movement input.
        S.I.IM.PC.Movement.Enable();
    }

    public override void Update() 
    {
    }

    public override void CaptureInput() {}
    public override void FixedUpdate() {}
}