using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Equipment/Equipment Inventory", fileName = "Equipment Inventory")]
public class SOEquipment : ScriptableObject , IResettable
{
	public event Action OnEquipmentChanged;

	public List<SOEquipmentItem> EquipmentItems;

	public void Equip(SOEquipmentItem newItem)
	{
        SOEquipmentType type = newItem.EquipmentType;

        // If there is something equipped in this slot, unequip it. 
        for (int i = 0; i < EquipmentItems.Count; i++)
        {
            if (type.name == EquipmentItems[i].EquipmentType.name)
            {
                SOEquipmentItem oldItem = EquipmentItems[i];

                Unequip(oldItem);
            }
        }

        // Add new item to EquipmentItems. 
        EquipmentItems.Add(newItem);

        // UIEquipment and StatManager listen. 
        OnEquipmentChanged?.Invoke();
    }

    public void Unequip(SOEquipmentItem oldItem)
    {
        // Remove old item from EquipmentItems. 
        EquipmentItems.Remove(oldItem);

        // UIEquipment and StatManager listen. 
        OnEquipmentChanged?.Invoke();
    }

    public void ResetOnExitPlayMode()
    {
        EquipmentItems.Clear();
    }
}