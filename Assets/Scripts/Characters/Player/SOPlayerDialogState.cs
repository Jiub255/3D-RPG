using UnityEngine;

[CreateAssetMenu(menuName = "States/Player/Dialog State", fileName = "Player Dialog State")]
public class SOPlayerDialogState : SOState<PlayerCharacterController>
{
    public override void Init(PlayerCharacterController parent)
    {
        base.Init(parent);

        UIDialog.OnDialogEnd += ChangeToMovementState;

/*        _runner.ACoupleOfStupidFixesThatIHate();
        S.I.IM.PC.Movement.Disable();
        S.I.IM.PC.UI.Disable();*/

        S.I.IM.DisableActionMap(S.I.IM.PC.Movement);
        S.I.IM.DisableActionMap(S.I.IM.PC.UI);
    }

    public override void Exit()
    {
        // Reenable movement and UI input.
        S.I.IM.PC.Movement.Enable();
        S.I.IM.PC.UI.Enable();

        UIDialog.OnDialogEnd -= ChangeToMovementState;
    }

    private void ChangeToMovementState()
    {
        _runner.ChangeState(typeof(SOPlayerMovementState));
    }

    public override void CaptureInput() {}
    public override void Update() {}
    public override void CheckForStateChangeConditions() {}
    public override void FixedUpdate() {}
}