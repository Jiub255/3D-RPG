using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Loot/Loot SO", fileName = "Loot SO")]
public class SOLoots : ScriptableObject, IResettable
{
    // TODO - Make this a List<EnemyLoot> instead, so you can delete it and disable the enemy when 
    // you've taken the last item. Actually needed to carry the enemy name, not to delete it later. 
    // List is a class, so it's passed by reference. And enemy loot is a class too. 
	public List<EnemyLoot> EnemyLoots = new();
//    public List<List<ItemAmount>> EnemyLootLists = new();

    // TODO - Make this current enemy? 
    public EnemyLoot CurrentEnemyLoot;
//  	public List<ItemAmount> CurrentLootList = new();

    // Called by EnemyLootController, which is called by LootSlot, which is called from button. 
    public void RemoveFromCurrentLootList(ItemAmount itemAmount)
    {
        CurrentEnemyLoot.ItemAmounts.Remove(itemAmount);
        if (CurrentEnemyLoot.ItemAmounts.Count == 0)
        {
            RemoveEnemyLoot(CurrentEnemyLoot);
        }

/*        CurrentLootList.Remove(itemAmount);
        if (CurrentLootList.Count == 0)
        {
            RemoveEnemyLoot(CurrentLootList);
        }*/
    }

	public void AddEnemyLoot(EnemyLoot enemyLoot/*List<ItemAmount> itemAmounts*/)
    {
        Debug.Log($"Adding {enemyLoot}. Current list count {EnemyLoots.Count}");

        // If this was the first one added, make it the current EnemyLoot too. 
        if (EnemyLoots.Count == 0)
        {
            CurrentEnemyLoot = enemyLoot;
        }

        EnemyLoots.Add(enemyLoot);

/*        if (EnemyLootLists.Count == 0)
        {
            CurrentLootList = itemAmounts;
        }

        EnemyLootLists.Add(itemAmounts);*/
    }

	public void RemoveEnemyLoot(EnemyLoot enemyLoot/*List<ItemAmount> itemAmounts*/)
    {
        /*        foreach (EnemyLoot enemyLoot in EnemyLoots)
                {
                    if (enemyLoot.ID == enemyInstanceID)
                    {
                        EnemyLoots.Remove(enemyLoot);
                        if (CurrentEnemyLoot == enemyLoot)
                        {
                            if (EnemyLoots.Count == 0)
                            {
                                CurrentEnemyLoot = null;

                                // 
                            }
                            else
                            {
                                CurrentEnemyLoot = EnemyLoots[0];
                            }
                        }
                    }
                }*/

        EnemyLoots.Remove(enemyLoot);
        if (CurrentEnemyLoot == enemyLoot)
        {
            // If the last EnemyLoot was removed from the list, make the current one null. 
            if (EnemyLoots.Count == 0)
            {
                CurrentEnemyLoot = null;
            }
            // Otherwise make the first one on the list the current one. 
            else
            {
                CurrentEnemyLoot = EnemyLoots[0];
            }
        }

/*        EnemyLootLists.Remove(itemAmounts);

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
        }*/
    }

    // Called by buttons in loot UI. 
	public void NextLoot()
    {
        CurrentEnemyLoot = EnemyLoots[(GetIndex(CurrentEnemyLoot) + 1) % EnemyLoots.Count];

//		CurrentLootList = EnemyLootLists[(GetIndex(CurrentLootList) + 1) % EnemyLootLists.Count];
    }

	public void PreviousLoot()
    {
        CurrentEnemyLoot = EnemyLoots[(GetIndex(CurrentEnemyLoot) - 1) % EnemyLoots.Count];
        
//        CurrentLootList = EnemyLootLists[(GetIndex(CurrentLootList) - 1) % EnemyLootLists.Count];
    }

    private int GetIndex(EnemyLoot enemyLoot/*<ItemAmount> lootList*/)
    {
        for (int i = 0; i < EnemyLoots.Count; i++)
        {
            if (EnemyLoots[i] == enemyLoot)
            {
                return i;
            }
        }

        return -1;

/*        for (int i = 0; i < EnemyLootLists.Count; i++)
        {
            if (EnemyLootLists[i] == lootList)
            {
                return i;
            }
        }

        return -1;*/
    }

    public void ResetOnExitPlayMode()
    {
/*        EnemyLootLists.Clear();
        CurrentLootList = null;*/

        EnemyLoots = new();
        CurrentEnemyLoot = null;
    }
}
