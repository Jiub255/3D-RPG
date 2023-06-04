using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/Inventory")]
public class SOInventory : ScriptableObject
{
	private List<ItemAmount> _itemAmounts;

	private ItemAmount Contains(SOItem item)
    {
        foreach (ItemAmount itemAmount in _itemAmounts)
        {
            if (itemAmount.Item == item)
            {
                return itemAmount;
            }
        }

        return null;
    }

    public void AddItem(SOItem item)
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
            ItemAmount newItemAmount = new();
            newItemAmount.Item = item;
            newItemAmount.Amount = 1;
            _itemAmounts.Add(newItemAmount);
        }
    }

    public void RemoveItem(SOItem item)
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
                _itemAmounts.Remove(listItemAmount);
            }
            return;
        }
        else
        {
            Debug.LogWarning("Item to be removed not in inventory");
        }
    }
}