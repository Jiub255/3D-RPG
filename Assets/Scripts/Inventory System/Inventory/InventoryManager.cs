using UnityEngine;

public class InventoryManager : MonoBehaviour
{
	[SerializeField]
	private SOInventory _inventorySO;

	public void AddItem(SOInventoryItem item)
    {
		_inventorySO.AddItem(item);
    }

	public void RemoveItem(SOInventoryItem item)
    {
		_inventorySO.RemoveItem(item);
    }
}