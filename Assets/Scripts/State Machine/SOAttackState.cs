using UnityEngine;

[CreateAssetMenu(menuName = "States/Attack State", fileName = "Attack State")]
public class SOAttackState : SOState<CharacterController>
{
    // TODO - Get rid of this and do a check sphere at the peak of the animation. Can do it based on time or by an animation event. 
/*    [SerializeField]
    private PlayerMeleeWeapon _playerMeleeWeapon;*/

    [SerializeField]
    private AnimationClip _attackAnimationClip;

    private Animator _animator;
    private float _timer;

    public override void Init(CharacterController parent)
    {
        base.Init(parent);

        _animator = parent.GetComponentInChildren<Animator>();

        // Set timer to attack animation length. 
        _timer = _attackAnimationClip.length;

        // Disable movement input while attacking. 
        S.I.IM.PC.Movement.Disable();

        // Enable weapon collider while attacking. 
        // TODO - Maybe just do a CheckSphere in front of player at the peak of the swing? 
        // It would eliminate accidental multiple hits, and just seems more controlled. 
        // Plus it would eliminate the need for this SO to get an in scene instance reference. DO IT. 
        //_playerMeleeWeapon.EnableCollider();
        _animator.SetTrigger("Melee");
    }

    // Called by animation event at peak of attack animation. 
    public void CheckHits()
    {
        // TODO - Set this up to detect hits within sphere (or whatever area). 
        Physics.CheckSphere(_runner.transform.position, 1f);
    }

    public override void ChangeState()
    {
        // Leave state if finished attack. 
        _timer -= Time.deltaTime;
        if (_timer < 0)
        {
            _runner.SetState(typeof(SOMovementState));
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
        S.I.IM.PC.Movement.Enable();

        // Disable weapon collider at end of attack. 
        // TODO - Maybe just do a CheckSphere in front of player at the peak of the swing? 
        // It would eliminate accidental multiple hits, and just seems more controlled. 
        // Plus it would eliminate the need for this SO to get an in scene instance reference. DO IT. 
        //_playerMeleeWeapon.DisableCollider();
    }

    public override void CaptureInput() {}
    public override void FixedUpdate() {}
    public override void Update() {}
}