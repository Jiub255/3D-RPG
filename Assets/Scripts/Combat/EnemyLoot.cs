using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLoot : MonoBehaviour
{
    public static event Action OnLootsSOChanged;

	[SerializeField]
	private List<ItemAmount> _itemAmounts;
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
        // Update SOLoots. 
        _lootsSO.RemoveFromCurrentLootList(itemAmount);
        if (_lootsSO.CurrentLootList/*.Count == 0*/ == null)
        {
            // Disable loot collider. 
            _lootTriggerCollider.enabled = false;
        }

        // Update UILoot. 
        OnLootsSOChanged();
    }

    // Set loot collider to only collide with playerlayer?
    private void OnTriggerEnter(Collider other)
    {
        // Add loot list to SO. 
        _lootsSO.AddEnemyLoot(_itemAmounts);

        // MenuController listens, toggles menu. 
        // If one loot on list after adding, menu not open, so...
        if (_lootsSO.EnemyLootLists.Count == 1)
        {
            // Open menu. Heard by UILoot. 
            // Make this list the active list. 
            OnLootsSOChanged?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Remove loot list from SO.
        _lootsSO.RemoveEnemyLoot(_itemAmounts);

        // MenuController listens, toggles menu. 
        // If no loots on list after removing last one...
        if (_lootsSO.EnemyLootLists.Count == 0)
        {
            // Close menu. Heard by UILoot. 
            OnLootsSOChanged?.Invoke();
        }
    }
}