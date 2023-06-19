using UnityEngine;

[CreateAssetMenu(menuName = "States/Player/Attack State", fileName = "Player Attack State")]
public class SOPlayerAttackState : SOState<PlayerCharacterController>
{
    [SerializeField]
    protected AnimationClip _attackAnimationClip;
    protected float _animationLength { get { return _attackAnimationClip.length; } }
    [SerializeField, Tooltip("In Seconds")]
    protected float _timeUntilAttack = 0.58f;

    protected float _timer;
    protected bool _hasAttacked;

    // Gets player stat data for attack. 
    protected PlayerMeleeAttack _playerMeleeAttack;

    public override void Init(PlayerCharacterController parent)
    {
        base.Init(parent);

        _timer = 0f;
        _hasAttacked = false;
        _playerMeleeAttack = _runner.GetComponentInChildren<PlayerMeleeAttack>();

        // TODO - Find better fix eventually. 
        // Has to be here before disabling Movement action map. 
/*        _runner.ACoupleOfStupidFixesThatIHate();

        // Disable movement input while attacking. 
        S.I.IM.PC.Movement.Disable();*/

        S.I.IM.DisableActionMap(S.I.IM.PC.Movement);

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

    public override void Exit() 
    {
        // Reenable movement input.
        S.I.IM.PC.Movement.Enable();
    }

    public override void CaptureInput() {}
    public override void FixedUpdate() {}
}