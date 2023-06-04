using UnityEngine;

public class InventoryManager : MonoBehaviour
{
	[SerializeField]
	private SOInventory _inventory;

	public void AddItem(SOItem item)
    {
		_inventory.AddItem(item);
    }

	public void RemoveItem(SOItem item)
    {
		_inventory.RemoveItem(item);
    }
}