using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Usable Item", menuName = "Inventory/Usable Item")]
public class SOUsableItem : SOItem
{
    public bool IsReusable = false;

    public static event Action OnUseItem;

    public override void UseItem()
    {
        base.UseItem();

        OnUseItem.Invoke();

        if (!IsReusable)
        {
            RemoveFromInventory();
        }
    }
}