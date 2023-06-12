using UnityEngine;
using UnityEngine.InputSystem;

public class InteractablePressKey : InteractableTrigger
{
    protected bool _playerInRange = false;

    private void Awake()
    {
        CheckIfPlayerIsInRange();
    }

    private void CheckIfPlayerIsInRange()
    {
        int playerLayerMask = LayerMask.NameToLayer("Player");
        float colliderRadius = GetComponent<CapsuleCollider>().radius;
        if (Physics.CheckSphere(transform.position, colliderRadius, playerLayerMask))
        {
            _playerInRange = true;
        }
        else
        {
            _playerInRange = false;
        }
    }

    private void Start()
    {
        S.I.IM.PC.World.Interact.canceled += Interact;
    }

    private void OnDisable()
    {
        S.I.IM.PC.World.Interact.canceled -= Interact;
    }

    public virtual void Interact(InputAction.CallbackContext context)
    {
        if (_playerInRange)
        {
            Debug.Log($"Pressed Interact on {gameObject.name}");
        }
    }

    public override void EnterInteractableZone(Collider otherCollider)
    {
        base.EnterInteractableZone(otherCollider);

        _playerInRange = true;
    }

    public override void LeaveInteractionZone(Collider otherCollider)
    {
        base.LeaveInteractionZone(otherCollider);

        _playerInRange = false;
    }
}