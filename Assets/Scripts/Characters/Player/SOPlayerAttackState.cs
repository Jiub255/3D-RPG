using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Player/Attack State", fileName = "Player Attack State")]
public class SOPlayerAttackState : SOState<PlayerCharacterController>
{
    [SerializeField]
    private AnimationClip _attackAnimationClip;
    [SerializeField, Tooltip("In Seconds")]
    private float _peakAnimationTime = 0.58f;

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

        // This fixes the bug but causes errors, other action maps wont work after.  
        S.I.IM.PC.Movement.MovePlayer.Dispose();
        S.I.IM.PC.Movement.Melee.Dispose();
        S.I.IM.PC.Movement.Interact.Dispose();
        // This fixes the bug introduced by the above bug fix. Fixception. I don't like it. Maybe try upgrading unity/input system instead? 
        S.I.IM.PC.Enable();

        // Disable movement input while attacking. 
        S.I.IM.PC.Movement.Disable();

        // Start attack animation. 
        _runner.Animator.SetTrigger("Melee");
    }

    public override void CheckForStateChangeConditions()
    {
        // Go back to movement state when attack is finished. 
        _timer += Time.deltaTime;
        if (_timer > _peakAnimationTime && !_hasAttacked)
        {
            _hasAttacked = true;
            _playerMeleeAttack.CheckForHits();
        }
        if (_timer > _attackAnimationClip.length)
        {
            _runner.ChangeState(typeof(SOPlayerMovementState));
        }
    }

    // TODO - Have OnGotHit event call this. 
    private void ChangeToKnockbackState()
    {
        //_runner.SetState(typeof(SOKnockbackState));
    }

    public override void Exit()
    {
        // Reenable movement input.
       // S.I.IM.PC.Movement.Enable();
        
    }

    public override void CaptureInput() {}
    public override void FixedUpdate() {}
    public override void Update() {}
}