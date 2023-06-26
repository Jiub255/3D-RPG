using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyLoot
{
    public string Name;
    public List<ItemAmount> ItemAmounts;

    public EnemyLoot(string name, List<ItemAmount> itemAmounts)
    {
        Name = name;
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
    public static event Action OnEnemyLootListsEmptied;

	public List<ItemAmount> ItemAmounts;

    [SerializeField]
    private SOLoots _lootsSO;
    [SerializeField]
    private Collider _lootTriggerCollider;

    private EnemyLoot _enemyLoot;

    private void OnEnable()
    {
        LootSlot.OnItemAmountLooted += RemoveItemAmount;
        EnemyHealthManager.OnEnemyDied += () => _lootTriggerCollider.enabled = true;

        // Get reference to the class here so it can be removed later. 
        _enemyLoot = new EnemyLoot(transform.parent.gameObject.name, ItemAmounts);
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

        // If EnemyLootLists is empty, then disable loot UI. 
        Debug.Log($"RemoveItemAmount EnemyLoots list count: {_lootsSO.EnemyLoots.Count}");
        if (_lootsSO.EnemyLoots.Count == 0)
        {
            Debug.Log("Inside if block");
            // Disable loot collider. 
            _lootTriggerCollider.enabled = false;

            // Heard by MenuController, disables loot UI. 
            OnEnemyLootListsEmptied?.Invoke();
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
        _lootsSO.AddEnemyLoot(_enemyLoot);

        // MenuController listens, toggles menu. 
        // If one loot on list after adding, menu not open, so...
        Debug.Log($"TriggerEnter EnemyLoots list count: {_lootsSO.EnemyLoots.Count}");
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
        _lootsSO.RemoveEnemyLoot(_enemyLoot);

        // If no loots on list after removing last one...
        Debug.Log($"TriggerExit EnemyLoots list count: {_lootsSO.EnemyLoots.Count}");
        if (_lootsSO.EnemyLoots.Count == 0)
        {
            // Close loot menu, heard by MenuController. 
            OnEnemyLootListsEmptied?.Invoke();
        }
        else
        {
            // Update UILoot. 
            OnLootsSOChanged?.Invoke();
        }
    }
}