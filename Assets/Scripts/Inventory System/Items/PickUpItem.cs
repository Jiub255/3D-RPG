using UnityEngine;

public class PickUpItem : InteractableTrigger
{
    [SerializeField]
    private SOItem _item;

    public override void EnterInteractableZone(Collider collision)
    {
        base.EnterInteractableZone(collision);

        _item.PickUpItem(collision.transform.parent.gameObject.GetComponentInChildren<InventoryManager>());

        gameObject.SetActive(false);
    }
}