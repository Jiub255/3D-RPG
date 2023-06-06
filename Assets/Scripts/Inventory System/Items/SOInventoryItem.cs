using UnityEngine;

public class SOInventoryItem : SOItem
{
    public override void PickUpItem(InventoryManager inventoryManager)
    {
        base.PickUpItem(inventoryManager);

        Debug.Log($"Added {name} to inventory. ");

		inventoryManager.AddItem(this);
    }
}