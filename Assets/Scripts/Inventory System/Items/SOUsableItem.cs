using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Usable Item", menuName = "Inventory/Usable Item")]
public class SOUsableItem : SOInventoryItem
{
    //public UsableItemEffect effect;

    public bool IsReusable = false;

    // TODO - How will this work? Each item sends the same event? Pass an identifying parameter?
    // Or just use Unity Events?
    //public static event Action<UsableItemEffect> OnUseItem;

    public List<SOEffect> Effects = new();
    
    public override void UseItem()
    {
        base.UseItem();

        //OnUseItem.Invoke(effect);

        foreach (SOEffect effect in Effects)
        {
            effect.ExecuteEffect(this);
        }

        if (!IsReusable)
        {
            RemoveFromInventory();
        }
    }
}

/*public enum UsableItemEffect
{
    HealPotion,
    ManaPotion
}*/