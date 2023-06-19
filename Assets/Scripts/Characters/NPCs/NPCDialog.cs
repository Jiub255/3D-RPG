using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class NPCDialog : InteractablePressKey
{
    public static event Action<Transform> OnInteractWithNPC;

    [SerializeField] 
    private SOTextAsset _textAssetSO;
    [SerializeField] 
    private TextAsset _npcDialogTextAsset;

    private Transform _playerTransform;

    public override void Interact(InputAction.CallbackContext context)
    {
        base.Interact(context);

        if (_playerInRange)
        {
            // Look at player.
            transform.parent.LookAt(_playerTransform.position);

            // MenuController listens to open dialog UI. 
            // CameraManager listens and changes to dialog camera and has camera look at NPC.
            // SOPlayerMovementState listens to look at NPC. 
            OnInteractWithNPC?.Invoke(transform);
        }
    }

    public override void EnterInteractableZone(Collider collision)
    {
        base.EnterInteractableZone(collision);

        _playerTransform = collision.transform.parent;

        // TODO - Put question mark above NPC's head.

    }

    public override void LeaveInteractionZone(Collider otherCollider)
    {
        base.LeaveInteractionZone(otherCollider);

        // TODO - Disable question mark. 

    }
}