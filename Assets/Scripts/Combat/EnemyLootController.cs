using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyLoot
{
    public int ID;
    public List<ItemAmount> ItemAmounts;

    public EnemyLoot(int id, List<ItemAmount> itemAmounts)
    {
        ID = id;
        ItemAmounts = itemAmounts;
    }
}

public class EnemyLootController : MonoBehaviour
{
    // Heard by UILoot, updates UI.
    public static event Action OnLootsSOChanged;
    // Heard by MenuController, opens loot UI.
    public static event Action OnEnteredFirstLootTrigger;
    // Heard by MenuController, closes loot UI.
    public static event Action OnEnemyLootsEmptied;

	public List<ItemAmount> ItemAmounts;

    [SerializeField]
    private SOLoots _lootsSO;
    [SerializeField]
    private Collider _lootTriggerCollider;

    private void OnEnable()
    {
        LootSlot.OnItemAmountLooted += RemoveItemAmount;
        EnemyHealthManager.OnEnemyDied += () => _lootTriggerCollider.enabled = true;
    }

    private void OnDisable()
    {
        LootSlot.OnItemAmountLooted -= RemoveItemAmount;
        EnemyHealthManager.OnEnemyDied -= () => _lootTriggerCollider.enabled = true;
    }

    private void RemoveItemAmount(ItemAmount itemAmount)
    {
        // Update SOLoots. Changes current enemy loot to first in list if current one gets emptied.
        _lootsSO.RemoveFromCurrentLootList(itemAmount);

        // If EnemyLoots is empty, then disable loot UI. 
        if (_lootsSO.EnemyLoots.Count == 0)
        {
            // Disable loot collider. 
            _lootTriggerCollider.enabled = false;

            // Heard by MenuController, disables loot UI. 
            OnEnemyLootsEmptied?.Invoke();
        }
        else
        {
            // Update UILoot. 
            OnLootsSOChanged?.Invoke();
        }
    }

    // Set loot collider to only collide with playerlayer?
    private void OnTriggerEnter(Collider other)
    {
        // Add loot list to SO. 
        _lootsSO.AddEnemyLoot(new EnemyLoot(transform.parent.gameObject.GetInstanceID(), ItemAmounts)/*this*/);

        // MenuController listens, toggles menu. 
        // If one loot on list after adding, menu not open, so...
        if (_lootsSO.EnemyLoots.Count == 1)
        {
            // Open menu. Heard by MenuController. 
            // Make this list the active list. 
            OnEnteredFirstLootTrigger?.Invoke();
        }

        // Update UILoot. 
        OnLootsSOChanged?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        // Remove loot list from SO.
        _lootsSO.RemoveEnemyLoot(new EnemyLoot(transform.parent.gameObject.GetInstanceID(), ItemAmounts)/*this*/);

        // If no loots on list after removing last one...
        if (_lootsSO.EnemyLoots.Count == 0)
        {
            // Close loot menu, heard by MenuController. 
            OnEnemyLootsEmptied?.Invoke();
        }
        else
        {
            // Update UILoot. 
            OnLootsSOChanged?.Invoke();
        }
    }
}