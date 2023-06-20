using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILoot : MonoBehaviour
{
	[SerializeField]
	private SOLoots _lootsSO;
	[SerializeField]
	private GameObject _lootSlotPrefab;
	[SerializeField]
	private Transform _lootContent;

	private void OnEnable()
	{
		SetupLootSlots();

		EnemyLoot.OnLootsSOChanged += SetupLootSlots;
		LootSlot.OnItemAmountLooted += (i) => { SetupLootSlots(); };
	}

    private void OnDisable()
    {
		EnemyLoot.OnLootsSOChanged -= SetupLootSlots;
		LootSlot.OnItemAmountLooted -= (i) => { SetupLootSlots(); };
	}

	private void SetupLootSlots()
    {
		ClearSlots();

		// TODO - Check if CurrentLootList == null here? 
		// TODO - Goblin can die over and over again, fix it. 
		foreach (ItemAmount itemAmount in _lootsSO.CurrentLootList)
        {
			GameObject slot = Instantiate(_lootSlotPrefab, _lootContent);
			slot.GetComponent<LootSlot>().SetupSlot(itemAmount);
		}
    }

	private void ClearSlots()
	{
		foreach (Transform slotTransform in _lootContent)
		{
			Destroy(slotTransform.gameObject);
		}
	}
}