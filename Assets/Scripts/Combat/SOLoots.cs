using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Loot/Loot SO", fileName = "Loot SO")]
public class SOLoots : ScriptableObject, IResettable
{
    // TODO - Make this a List<EnemyLoot> instead, so you can delete it and disable the enemy when 
    // you've taken the last item. 
	public List<EnemyLoot/*Controller*/> EnemyLoots = new();
//	public List<List<ItemAmount>> EnemyLootLists = new();

    // TODO - Make this current enemy? 
    public EnemyLoot/*Controller*/ CurrentEnemyLoot;
    //	public List<ItemAmount> CurrentLootList = new();

    public void RemoveFromCurrentLootList(ItemAmount itemAmount)
    {
        CurrentEnemyLoot.ItemAmounts.Remove(itemAmount);
        if (CurrentEnemyLoot.ItemAmounts.Count == 0)
        {
            RemoveEnemyLoot(CurrentEnemyLoot.ID);
        }

/*        CurrentLootList.Remove(itemAmount);
        if (CurrentLootList.Count == 0)
        {
            RemoveEnemyLoot(CurrentLootList);
        }*/
    }

	public void AddEnemyLoot(EnemyLoot/*Controller*/ enemyLoot/*List<ItemAmount> itemAmounts*/)
    {
        Debug.Log($"Adding {enemyLoot}. Current list count {EnemyLoots.Count}");

        if (EnemyLoots.Count == 0)
        {
            CurrentEnemyLoot = enemyLoot;
        }

        EnemyLoots.Add(enemyLoot);

/*		if (EnemyLootLists.Count == 0)
        {
			CurrentLootList = itemAmounts;
        }

		EnemyLootLists.Add(itemAmounts);*/
    }

	public void RemoveEnemyLoot(int enemyInstanceID
        /*EnemyLoot*//*Controller*//* enemyLoot*//*List<ItemAmount> itemAmounts*/)
    {
        foreach (EnemyLoot enemyLoot in EnemyLoots)
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
        }

/*        EnemyLoots.Remove(enemyLoot);
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
        }*/

/*		EnemyLootLists.Remove(itemAmounts);

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

    private int GetIndex(EnemyLoot/*Controller*/ enemyLoot/*List<ItemAmount> lootList*/)
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
        EnemyLoots = new();
        CurrentEnemyLoot = null;
    }
}
