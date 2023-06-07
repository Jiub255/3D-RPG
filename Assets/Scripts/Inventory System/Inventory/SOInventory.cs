using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/Inventory")]
public class SOInventory : ScriptableObject , IResettable
{
    public event Action OnInventoryChanged;

	public List<ItemAmount> ItemAmounts;

	private ItemAmount Contains(SOItem item)
    {
        foreach (ItemAmount itemAmount in ItemAmounts)
        {
            if (itemAmount.Item == item)
            {
                return itemAmount;
            }
        }

        return null;
    }

    public void AddItem(SOInventoryItem item)
    {
        ItemAmount listItemAmount = Contains(item);
        // Increase amount if item already in list. 
        if (listItemAmount != null)
        {
                listItemAmount.Amount++;
        }
        // Add new itemAmount to list if not. 
        else
        {
            ItemAmount newItemAmount = new(item, 1);
            ItemAmounts.Add(newItemAmount);
        }

        // Heard by UIInventory, calls SetupSlots with the newly updated inventory SO. 
        OnInventoryChanged?.Invoke();
    }

    public void RemoveItem(SOInventoryItem item)
    {
        ItemAmount listItemAmount = Contains(item);
        if (listItemAmount != null)
        {
            // If there's more than one in inventory, decrease amount.
            if (listItemAmount.Amount > 1)
            {
                listItemAmount.Amount--;
            }
            // If there's only one in inventory, delete the ItemAmount entirely. 
            else
            {
                ItemAmounts.Remove(listItemAmount);
            }

            OnInventoryChanged?.Invoke();
            
            return;
        }
        else
        {
            Debug.LogWarning("Item to be removed not in inventory");
        }
    }

    // FOR TESTING
    // Clear inventory when exiting play mode. 
    public void ResetOnExitPlayMode()
    {
        ItemAmounts.Clear();
    }
}