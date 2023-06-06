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
	private RectTransform _helmetRectTransform;
	[SerializeField]
	private RectTransform _armorRectTransform;
	[SerializeField]
	private RectTransform _weaponRectTransform;
	[SerializeField]
	private RectTransform _shieldRectTransform;

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
		GameObject helmetSlot = Instantiate(_equipmentSlotPrefab, _helmetRectTransform);
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
		shieldSlot.GetComponent<EquipmentSlot>().SetupSlot(_equipmentSO.ShieldItem);
    }
}