using System;
using UnityEngine;

public class SOInventoryItem : SOItem
{
    public static event Action<SOInventoryItem> OnRemoveItem;
    public static event Action<SOInventoryItem> OnAddItem;

    public override void PickUpItem(InventoryManager inventoryManager)
    {
        base.PickUpItem(inventoryManager);

        Debug.Log($"Added {name} to inventory. ");

		inventoryManager.AddItem(this);
    }

    public override void UseItem()
    {
        base.UseItem();

        // TODO - Set the item to the mouse cursor so you can equip/use/drop the item. 
    }

    // Heard by PlayerInventoryManager. 
    protected void AddToInventory()
    {
        OnAddItem?.Invoke(this);
    }

    // Heard by PlayerInventoryManager. 
    protected void RemoveFromInventory()
    {
        OnRemoveItem?.Invoke(this);
    }
}