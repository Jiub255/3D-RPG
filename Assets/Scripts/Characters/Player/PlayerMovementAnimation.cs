using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementAnimation
{
	private Animator _animator;
    private InputAction _movePlayerAction;

    public PlayerMovementAnimation(Animator animator, InputAction movePlayerAction)
    {
        _animator = animator;
        _movePlayerAction = movePlayerAction;
    }

/*    private void Start()
    {
        _animator = GetComponent<Animator>();
        _movePlayerAction = S.I.IM.PC.Movement.MovePlayer;
    }*/

    public void FixedUpdate()
    {
        _animator.SetFloat("Speed", _movePlayerAction.ReadValue<Vector2>().sqrMagnitude);
    }
}