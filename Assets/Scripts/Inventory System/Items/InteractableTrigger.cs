using UnityEngine;

public class InteractableTrigger : MonoBehaviour
{
    // This method is meant to be overwritten.
    public virtual void EnterInteractableZone(Collider otherCollider)
    {
        Debug.Log($"Entered {gameObject.name}'s Interactable Zone");
    }

    // This method is meant to be overwritten.
    public virtual void LeaveInteractionZone(Collider otherCollider)
    {
        Debug.Log($"Left {gameObject.name}'s Interactable Zone");
    }

    // Put interactable colliders on a separate sub object that is in a layer that only collides with the player. 
    private void OnTriggerEnter(Collider otherCollider)
    {
        EnterInteractableZone(otherCollider);
    }

    private void OnTriggerExit(Collider otherCollider)
    {
        LeaveInteractionZone(otherCollider);
    }
}