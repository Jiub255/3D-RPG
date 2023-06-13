using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMelee : MonoBehaviour
{
	private Animator _animator;

    [SerializeField]
    private PlayerMeleeWeapon _playerMeleeWeapon;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        S.I.IM.PC.World.Melee.started += MeleeAttack;
    }

    private void OnDisable()
    {
        S.I.IM.PC.World.Melee.started -= MeleeAttack;
    }

    private void MeleeAttack(InputAction.CallbackContext context)
    {
        _playerMeleeWeapon.EnableCollider();
        _animator.SetTrigger("Melee");
    }

    public void DisableCollider()
    {
        _playerMeleeWeapon.DisableCollider();
    }
}