using TMPro;
using UnityEngine;

public class UILoot : MonoBehaviour
{
	[SerializeField]
	private SOLoots _lootsSO;
	[SerializeField]
	private GameObject _lootSlotPrefab;
	[SerializeField]
	private Transform _lootContent;
	[SerializeField]
	private TextMeshProUGUI _currentEnemyName;

	private void OnEnable()
	{
		SetupLootSlots();

		EnemyLootController.OnLootsSOChanged += SetupLootSlots;
		LootSlot.OnItemAmountLooted += (i) => { SetupLootSlots(); };
	}

    private void OnDisable()
    {
		EnemyLootController.OnLootsSOChanged -= SetupLootSlots;
		LootSlot.OnItemAmountLooted -= (i) => { SetupLootSlots(); };
	}

	private void SetupLootSlots()
    {
		ClearSlots();

		if (_lootsSO.CurrentEnemyLoot != null)
        {
			// TODO - Pass a class with the loot list and the enemy name instead of just the list? 
			_currentEnemyName.text = _lootsSO.CurrentEnemyLoot.Name; 
//			_currentEnemyName.text = _lootsSO.CurrentLootList.transform.parent.gameObject.name;

			foreach (ItemAmount itemAmount in _lootsSO.CurrentEnemyLoot.ItemAmounts)
			{
				GameObject slot = Instantiate(_lootSlotPrefab, _lootContent);
				slot.GetComponent<LootSlot>().SetupSlot(itemAmount);
			}
        }
    }

	private void ClearSlots()
	{
		foreach (Transform slotTransform in _lootContent)
		{
			Destroy(slotTransform.gameObject);
		}
	}

	// Called by buttons in loot UI. 
	public void NextEnemyLoot()
    {
		_lootsSO.NextLoot();

		SetupLootSlots();
    }

	public void PreviousEnemyLoot()
    {
		_lootsSO.PreviousLoot();
	
		SetupLootSlots();
	}
}