using UnityEngine;
using UnityEngine.InputSystem;

public class InteractablePressKey : InteractableTrigger
{
    protected bool _playerInRange = false;

    [SerializeField]
    protected LayerMask _playerInteractLayerMask;

    protected void Awake()
    {
        CheckIfPlayerIsInRange();
    }

    protected void CheckIfPlayerIsInRange()
    {
        int playerInteractLayerMask = LayerMask.NameToLayer("PlayerInteract");
        float colliderRadius = GetComponent<SphereCollider>().radius;
//        Debug.Log($"Collider radius: {colliderRadius}, PlayerLayerMask int: {playerInteractLayerMask}");
        if (Physics.CheckSphere(transform.position, colliderRadius, _playerInteractLayerMask))
        {
            _playerInRange = true;
//            Debug.Log("Player in range");
        }
        else
        {
            _playerInRange = false;
//            Debug.Log("Player not in range");
        }
    }

    protected void Start()
    {
        S.I.IM.PC.Movement.Interact./*canceled*/performed += Interact;
    }

    private void OnDisable()
    {
        S.I.IM.PC.Movement.Interact./*canceled*/performed -= Interact;
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