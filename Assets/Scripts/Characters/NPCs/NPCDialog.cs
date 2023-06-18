using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class NPCDialog : InteractablePressKey
{
    // Heard by MenuController. 
    public static event Action OnInteractWithNPC;

    [SerializeField] 
    private SOTextAsset _textAssetSO;
    [SerializeField] 
    private TextAsset _npcDialogTextAsset;

    public override void Interact(InputAction.CallbackContext context)
    {
        base.Interact(context);

        if (_playerInRange)
        {
            OnInteractWithNPC?.Invoke();
        }
    }

    public override void EnterInteractableZone(Collider collision)
    {
        base.EnterInteractableZone(collision);

        // TODO - Put question mark above NPC's head.

        // TODO - Disable movement (and camera?) controls. 

    }

    public override void LeaveInteractionZone(Collider otherCollider)
    {
        base.LeaveInteractionZone(otherCollider);

        // TODO - Disable question mark. 

        // TODO - Reenable movement/camera controls. 

    }
}