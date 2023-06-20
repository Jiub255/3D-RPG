public class PlayerDialogState : State<PlayerCharacterController2>
{
    public PlayerDialogState(PlayerCharacterController2 parent) : base(parent) 
    {

        UIDialog.OnDialogEnd += ChangeToMovementState;

        S.I.IM.DisableActionMap(S.I.IM.PC.Movement);
        S.I.IM.DisableActionMap(S.I.IM.PC.UI);
    }

/*    public override void Init(PlayerCharacterController2 parent)
    {
        base.Init(parent);
    }*/

    public override void Exit()
    {
        // Reenable movement and UI input.
        S.I.IM.PC.Movement.Enable();
        S.I.IM.PC.UI.Enable();

        UIDialog.OnDialogEnd -= ChangeToMovementState;
    }

    private void ChangeToMovementState()
    {
        _runner.ChangeState2(_runner.Movement());
    }

    public override void Update() {}
    public override void FixedUpdate() {}
}