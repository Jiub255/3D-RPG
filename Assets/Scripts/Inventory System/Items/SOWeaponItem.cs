using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Item", menuName = "Inventory/Weapon Item")]
public class SOWeaponItem : SOEquipmentItem
{
    public override void UseItem()
    {
        base.UseItem();

        // Set the item to the mouse cursor, so you can equip it or drop it. 
        // TODO - Do this in the base item class instead? Since all items will act the same at this point?
        // Or at least do it in the equipment item class. 
    }
}