using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class NPCDialog : InteractablePressKey
{
    // TODO - Send NPC Transform through this action so dialog camera can LookAt it?
    // TODO - Have player LookAt NPC when they interact, or make a collision box in front of player for interactions, 
    // so they can't interact while facing away. 
    // Heard by MenuController. 
    public static event Action<Transform> OnInteractWithNPC;

    [SerializeField] 
    private SOTextAsset _textAssetSO;
    [SerializeField] 
    private TextAsset _npcDialogTextAsset;

    public override void Interact(InputAction.CallbackContext context)
    {
        base.Interact(context);

        if (_playerInRange)
        {
            // MenuController listens. 
            // TODO - Set up dialog camera and have it listen for this too. 
            // Have SOPlayerMovementState listen too, to look at NPC. 
            OnInteractWithNPC?.Invoke(transform);
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