using UnityEngine;

[CreateAssetMenu(menuName = "States/Player/Attack State", fileName = "Player Attack State")]
public class SOPlayerAttackState : SOState<PlayerCharacterController>
{
    [SerializeField]
    private AnimationClip _attackAnimationClip;
    private float _animationLength { get { return _attackAnimationClip.length; } }
    [SerializeField, Tooltip("In Seconds")]
    private float _timeUntilAttack = 0.58f;

    private float _timer;
    private bool _hasAttacked;

    // Gets player stat data for attack. 
    private PlayerMeleeAttack _playerMeleeAttack;

    public override void Init(PlayerCharacterController parent)
    {
        base.Init(parent);

        _timer = 0f;
        _hasAttacked = false;
        _playerMeleeAttack = _runner.GetComponentInChildren<PlayerMeleeAttack>();

        // TODO - Find better fix eventually. 
        // Stupid fixes, need to be here so that the action gets "completed" and not stuck in progress
        // or whatever was happening. 
        // This fixes the bug but causes errors, other action maps wont work after.  
        S.I.IM.PC.Movement.MovePlayer.Dispose();
        S.I.IM.PC.Movement.Melee.Dispose();
        S.I.IM.PC.Movement.Interact.Dispose();
        // This fixes the bug introduced by the above bug fix. Fixception. I don't like it. Maybe try upgrading unity/input system instead? 
        S.I.IM.PC.Enable();
        //ACoupleOfStupidFixesThatIHate();

        // Disable movement input while attacking. 
        S.I.IM.PC.Movement.Disable();

        // Start attack animation. 
        _runner.Animator.SetTrigger("Melee");
    }

    public override void Update() 
    {
        // Update called before CheckForStateChangeConditions in the StateRunner update loop, so the timer works for both methods. 
        _timer += Time.deltaTime;

        // Countdown to attack animation peak, when actual attack happens. 
        if (_timer > _timeUntilAttack && !_hasAttacked)
        {
            _hasAttacked = true;
            _playerMeleeAttack.CheckForHits();
        }
    }

    public override void CheckForStateChangeConditions()
    {
        // Countdown to end of attack animation, when you go back to MovementState. 
        if (_timer > _animationLength)
        {
            _runner.ChangeState(typeof(SOPlayerMovementState));
        }
    }

    private void ACoupleOfStupidFixesThatIHate() 
    { 
    }

    public override void CaptureInput() {}
    public override void FixedUpdate() {}
    public override void Exit() {}
}