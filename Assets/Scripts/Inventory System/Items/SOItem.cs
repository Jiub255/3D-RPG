using UnityEngine;

public class SOItem : ScriptableObject
{
	public string Description = "Enter Item Description";

	public Sprite Icon;

	public virtual void UseItem()
    {
		Debug.Log($"Used {name}"); 
    }

	public virtual void PickUpItem(InventoryManager inventoryManager)
    {
		Debug.Log($"Picked up {name}");
    }
}