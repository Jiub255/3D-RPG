using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Equipment/Equipment Inventory", fileName = "Equipment Inventory")]
public class SOEquipment : ScriptableObject
{
	public event Action OnEquipmentChanged;
    public static event Action<SOEquipmentItem> OnUnequip;

/*	public SOWeaponItem WeaponItem;
	public SOArmorItem ArmorItem;
	public SOHelmetItem HelmetItem;
	public SOShieldItem ShieldItem;*/

	public List<SOEquipmentItem> EquipmentItems;

	public void Equip(SOEquipmentItem newItem) 
	{
        SOEquipmentType type = newItem.EquipmentType;

        // if there is something equipped in this slot, unequip it
        for (int i = 0; i < EquipmentItems.Count; i++)
        {
            if (type.name == EquipmentItems[i].EquipmentType.name)
            {
                SOEquipmentItem oldItem = EquipmentItems[i];

                Unequip(oldItem);
            }
        }

        // add new item to equipSO
        EquipmentItems.Add(newItem);

        // UIEquipment listens. 
        OnEquipmentChanged?.Invoke();
    }

    public void Unequip(SOEquipmentItem oldItem)
    {
        EquipmentItems.Remove(oldItem);

        // Heard by PlayerInventoryManager. 
        OnUnequip?.Invoke(oldItem);
    }
}