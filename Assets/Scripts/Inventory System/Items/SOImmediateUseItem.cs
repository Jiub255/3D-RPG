using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Immediate Use Item", menuName = "Inventory/Immediate Use Item")]
public class SOImmediateUseItem : SOItem
{
    public static event Action OnPickUpItem;

    public override void PickUpItem(/*InventoryManager inventoryManager*/)
    {
        base.PickUpItem(/*inventoryManager*/);

        Debug.Log($"Immediate-used {name}");

        OnPickUpItem?.Invoke();
    }
}