using UnityEngine;

[CreateAssetMenu(menuName = "States/Player/Knockback State", fileName = "Player Knockback State")]
public class SOPlayerKnockbackState : SOState<PlayerCharacterController>
{
    [SerializeField]
    protected float _knockbackDuration = 0.5f;
    protected float _timer;

    public override void Init(PlayerCharacterController parent)
    {
        base.Init(parent);

        _timer = 0f;

        _runner.Rigidbody.AddForce(_runner.KnockbackVector, ForceMode.Impulse);

        // Have to do this before disabling movement. Terrible bug fix. 
        _runner.ACoupleOfStupidFixesThatIHate();
        S.I.IM.PC.Movement.Disable();

        // Animation
        _runner.Animator.SetTrigger("GetHit");
    }

    public override void CheckForStateChangeConditions()
    {
        _timer += Time.deltaTime;
        if (_timer > _knockbackDuration)
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
    public override void Update() {}
    public override void FixedUpdate() {}
}