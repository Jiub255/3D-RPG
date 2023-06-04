using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterAnimation : MonoBehaviour
{
	private Animator _animator;
    private InputAction _movePlayerAction;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _movePlayerAction = S.I.IM.PC.World.MovePlayer;
    }

    private void FixedUpdate()
    {
        _animator.SetFloat("Speed", _movePlayerAction.ReadValue<Vector2>().sqrMagnitude);
    }
}