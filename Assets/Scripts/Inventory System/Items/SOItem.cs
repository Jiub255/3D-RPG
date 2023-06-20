using UnityEngine;

public class SOItem : ScriptableObject
{
	public string Description = "Enter Item Description";

	public Sprite Icon;

	public virtual void UseItem()
    {
		Debug.Log($"Used {name}"); 
    }

	// TODO - Do I need to be passing around the inventoryManager reference? 
	// Or can it be done without? 
	public virtual void PickUpItem(/*InventoryManager inventoryManager*/)
    {
		Debug.Log($"Picked up {name}");
    }
}