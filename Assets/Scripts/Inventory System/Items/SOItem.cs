using System;
using UnityEngine;

public class SOItem : ScriptableObject
{
	public static event Action<SOItem> OnRemoveItem;

	public string Description = "Enter Item Description";

	public Sprite Icon;

	public virtual void UseItem()
    {
		Debug.Log($"Used {name}"); 

		// TODO - Set the item to the mouse cursor so you can equip/use/drop the item. 
    }

	public virtual void PickUpItem(InventoryManager inventoryManager)
    {
		Debug.Log($"Picked up {name}");
    }

	protected void RemoveFromInventory()
    {
		OnRemoveItem?.Invoke(this);
    }
}