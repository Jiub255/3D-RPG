using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Loot/Loot SO", fileName = "Loot SO")]
public class SOLoots : ScriptableObject
{
	public List<List<ItemAmount>> EnemyLootLists = new();

	public List<ItemAmount> CurrentLootList = new();

    public void RemoveFromCurrentLootList(ItemAmount itemAmount)
    {
        CurrentLootList.Remove(itemAmount);
        if (CurrentLootList.Count == 0)
        {
            RemoveEnemyLoot(CurrentLootList);
        }
    }

	public void AddEnemyLoot(List<ItemAmount> itemAmounts)
    {
		if (EnemyLootLists.Count == 0)
        {
			CurrentLootList = itemAmounts;
        }

		EnemyLootLists.Add(itemAmounts);
    }

	public void RemoveEnemyLoot(List<ItemAmount> itemAmounts)
    {
		EnemyLootLists.Remove(itemAmounts);

		if (CurrentLootList == itemAmounts)
        {
			if (EnemyLootLists.Count == 0)
            {
                CurrentLootList = null;
            }
            else
            {
                CurrentLootList = EnemyLootLists[0];
            }
        }
    }

	public void NextLoot()
    {
		CurrentLootList = EnemyLootLists[(GetIndex(CurrentLootList) + 1) % EnemyLootLists.Count];
    }

	public void PreviousLoot()
    {
		CurrentLootList = EnemyLootLists[(GetIndex(CurrentLootList) - 1) % EnemyLootLists.Count];
    }

    private int GetIndex(List<ItemAmount> lootList)
    {
        for (int i = 0; i < EnemyLootLists.Count; i++)
        {
            if (EnemyLootLists[i] == lootList)
            {
                return i;
            }
        }

        return -1;
    }
}
