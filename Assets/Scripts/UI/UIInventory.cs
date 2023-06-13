using UnityEngine;

public class UIInventory : MonoBehaviour
{
	[SerializeField]
	private SOInventory _inventory;
	[SerializeField]
	private GameObject _inventorySlotPrefab;
	[SerializeField]
	private Transform _inventoryContent;

    private void OnEnable()
    {
		SetupInventorySlots();

		_inventory.OnInventoryChanged += SetupInventorySlots;
		MenuController.OnOpenInventory += SetupInventorySlots;
    }

    private void OnDisable()
    {
		_inventory.OnInventoryChanged -= SetupInventorySlots;
		MenuController.OnOpenInventory -= SetupInventorySlots;
	}

	private void SetupInventorySlots()
	{
		Debug.Log("SetupInventorySlots called. ");

		ClearSlots();

		foreach (ItemAmount itemAmount in _inventory.ItemAmounts)
		{
			GameObject slot = Instantiate(_inventorySlotPrefab, _inventoryContent);
			slot.GetComponent<InventorySlot>().SetupSlot(itemAmount);
		}
	}

	private void ClearSlots()
    {
        foreach (Transform slotTransform in _inventoryContent)
        {
			Destroy(slotTransform.gameObject);
        }
    }
}