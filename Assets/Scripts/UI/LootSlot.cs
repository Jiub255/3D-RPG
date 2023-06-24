using System;

public class LootSlot : InventorySlot
{
    public static event Action<ItemAmount> OnItemAmountLooted;

    // Called by clicking on the loot slot button. 
	public void OnAddButton()
    {
        // Heard by PlayerInventoryManager to add ItemAmount. 
        // Heard by EnemyLoot to update loot list and SO. 
        OnItemAmountLooted?.Invoke(_itemAmount);
    }
}