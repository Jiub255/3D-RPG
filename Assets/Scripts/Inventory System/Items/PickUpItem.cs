using UnityEngine;

public class PickUpItem : Interactable
{
    [SerializeField]
    private SOItem _item;

    public override void Interact(Collider collision)
    {
        base.Interact(collision);

        _item.PickUpItem(collision.GetComponentInChildren<InventoryManager>());

        gameObject.SetActive(false);
    }
}