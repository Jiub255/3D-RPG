using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class NPCDialog : InteractablePressKey
{
    public static event Action<Transform> OnInteractWithNPC;

    [SerializeField] 
    protected SOTextAsset _textAssetSO;
    [SerializeField] 
    protected TextAsset _npcDialogTextAsset;
    [SerializeField]
    protected SOPlayerInstance _playerInstanceSO;

    public override void Interact(InputAction.CallbackContext context)
    {
        base.Interact(context);

        if (_playerInRange)
        {
            // Look at player.
            transform.parent.LookAt(_playerInstanceSO.PlayerInstanceTransform.position);

            // MenuController listens to open dialog UI. 
            // CameraManager listens and changes to dialog camera and has camera look at NPC.
            // SOPlayerMovementState listens to look at NPC. 
            OnInteractWithNPC?.Invoke(transform);
        }
    }

    public override void EnterInteractableZone(Collider otherCollider)
    {
        base.EnterInteractableZone(otherCollider);

        // TODO - Put question mark above NPC's head.

    }

    public override void LeaveInteractionZone(Collider otherCollider)
    {
        base.LeaveInteractionZone(otherCollider);

        // TODO - Disable question mark. 

    }
}