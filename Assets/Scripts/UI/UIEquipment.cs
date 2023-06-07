using System.Collections.Generic;
using UnityEngine;

public class UIEquipment : MonoBehaviour
{
	[SerializeField]
	private SOEquipment _equipmentSO;

	[SerializeField]
	private List<EquipmentSlot> _equipmentSlots;

    private void OnEnable()
    {
		_equipmentSO.OnEquipmentChanged += SetupEquipmentSlots;

		SetupEquipmentSlots();
    }

    private void OnDisable()
    {
		_equipmentSO.OnEquipmentChanged -= SetupEquipmentSlots;
	}

    private void SetupEquipmentSlots()
    {
		foreach (EquipmentSlot equipmentSlot in _equipmentSlots)
		{
			// Get slot's equipment type. 
			SOEquipmentType slotType = equipmentSlot.EquipmentType;

			// TODO - Can this be done without the bool? Like with break or return or something?
			bool isEquipped = false;

			// For each currently equipped item, 
			foreach (SOEquipmentItem currentlyEquippedItem in _equipmentSO.EquipmentItems)
			{
				// If it's of the same type as the current slot, 
				// ie, if this slot does have equipment, 
				if (slotType.name == currentlyEquippedItem.EquipmentType.name)
				{
					// Then update current slot with equipped item. 
					equipmentSlot.SetupSlot(currentlyEquippedItem);
					isEquipped = true;
				}
			}

			// If slot's type didn't match any equipment, it's unequipped, so clear the slot. 
			if (!isEquipped)
            {
				equipmentSlot.ClearSlot();
            }
		}
	}
}