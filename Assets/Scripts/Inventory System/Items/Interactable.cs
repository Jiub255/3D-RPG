using UnityEngine;

public class Interactable : MonoBehaviour
{
    public virtual void Interact(Collider collision)
    {
        //this method is meant to be overwritten
        Debug.Log("interacted with " + transform);
    }

    // Put interactable colliders on a separate sub object that is in a layer that only collides with the player. 
    private void OnTriggerEnter(Collider collision)
    {
        Interact(collision);
    }
}