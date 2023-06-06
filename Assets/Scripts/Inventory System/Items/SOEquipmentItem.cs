using System;
using UnityEngine;

// Have SOWeaponItem and SOArmorItem inherit this. 
public class SOEquipmentItem : SOInventoryItem
{
    public static event Action<SOEquipmentItem> OnEquip;
    public static event Action<SOEquipmentItem> OnUnequip;

    // Equip the item. Send signal to equipment and inventory managers. 
    // Called by "use" button on inventory slot prefab. 
    public override void UseItem()
    {
        base.UseItem();

        // Sends signal to EquipmentManager. 
        OnEquip?.Invoke(this);

        // Sends signal to PlayerInventoryManager. 
        RemoveFromInventory();

        Debug.Log($"Equipped {name}");
    }

    // Unequip the item. Send signal to equipment and inventory managers. 
    // Called by "unequip" button on equipment slot prefab. 
    public void Unequip()
    {
        // Sends signal to EquipmentManager. 
        OnUnequip?.Invoke(this);

        // Sends signal to PlayerInventoryManager. 
        AddToInventory();
        
        Debug.Log($"Unequipped {name}");
    }
}