using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEquipment : MonoBehaviour
{
	[SerializeField]
	private SOEquipment _equipmentSO;

	[SerializeField]
	private GameObject _equipmentSlotPrefab;

	[SerializeField]
	private List<EquipmentSlot> _equipmentSlots;

/*	[SerializeField]
	private RectTransform _helmetRectTransform;
	[SerializeField]
	private RectTransform _armorRectTransform;
	[SerializeField]
	private RectTransform _weaponRectTransform;
	[SerializeField]
	private RectTransform _shieldRectTransform;

	[SerializeField]
	private SOEquipmentType _helmetType;
	[SerializeField]
	private SOEquipmentType _armorType;
	[SerializeField]
	private SOEquipmentType _weaponType;
	[SerializeField]
	private SOEquipmentType _shieldType;*/

    private void OnEnable()
    {
		_equipmentSO.OnEquipmentChanged += SetupEquipmentSlots;
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

			// TODO - Can this be done without the bool? Like with break or return?
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

		/*		GameObject helmetSlot = Instantiate(_equipmentSlotPrefab, _helmetRectTransform);
				helmetSlot.transform.localPosition = Vector3.zero;
				helmetSlot.GetComponent<EquipmentSlot>().SetupSlot(_equipmentSO.HelmetItem);

				GameObject armorSlot = Instantiate(_equipmentSlotPrefab, _armorRectTransform);
				armorSlot.transform.localPosition = Vector3.zero;
				armorSlot.GetComponent<EquipmentSlot>().SetupSlot(_equipmentSO.ArmorItem);

				GameObject weaponSlot = Instantiate(_equipmentSlotPrefab, _weaponRectTransform);
				weaponSlot.transform.localPosition = Vector3.zero;
				weaponSlot.GetComponent<EquipmentSlot>().SetupSlot(_equipmentSO.WeaponItem);

				GameObject shieldSlot = Instantiate(_equipmentSlotPrefab, _shieldRectTransform);
				shieldSlot.transform.localPosition = Vector3.zero;
				shieldSlot.GetComponent<EquipmentSlot>().SetupSlot(_equipmentSO.ShieldItem);*/
	}
}