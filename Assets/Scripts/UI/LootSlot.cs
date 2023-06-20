using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSlot : InventorySlot
{
    public static event Action<ItemAmount> OnItemAmountLooted;

	public void OnAddButton()
    {
        // Heard by PlayerInventoryManager to add ItemAmount. 
        // Heard by EnemyLoot? to change loot list? 
        OnItemAmountLooted?.Invoke(_itemAmount);
    }
}