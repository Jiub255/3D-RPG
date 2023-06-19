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

    public void FixedUpdate()
    {
        _animator.SetFloat("Speed", _movePlayerAction.ReadValue<Vector2>().sqrMagnitude);
    }
}