using UnityEngine;

public class PickUpItem : Interactable
{
    [SerializeField]
    private SOItem _item;

    public override void Interact(Collider collision)
    {
        base.Interact(collision);

        _item.PickUpItem(collision.GetComponent<InventoryManager>());

        gameObject.SetActive(false);
    }
}