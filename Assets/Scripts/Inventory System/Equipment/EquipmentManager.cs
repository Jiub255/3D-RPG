using System;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
	[SerializeField]
	private SOEquipment _equipmentSO;

    private void OnEnable()
    {
        SOEquipmentItem.OnEquip += Equip;
        SOEquipmentItem.OnUnequip += Unequip;
    }

    private void OnDisable()
    {
        SOEquipmentItem.OnEquip -= Equip;
        SOEquipmentItem.OnUnequip -= Unequip;
    }

    public void Equip(SOEquipmentItem equipmentItem)
    {
        if (equipmentItem.GetType() == typeof(SOHelmetItem))
        {
            _equipmentSO.HelmetItem = (SOHelmetItem)equipmentItem;
        }
        else if (equipmentItem.GetType() == typeof(SOArmorItem))
        {
            _equipmentSO.ArmorItem = (SOArmorItem)equipmentItem;
        }
        else if (equipmentItem.GetType() == typeof(SOWeaponItem))
        {
            _equipmentSO.WeaponItem = (SOWeaponItem)equipmentItem;
        }
        else if (equipmentItem.GetType() == typeof(SOShieldItem))
        {
            _equipmentSO.ShieldItem = (SOShieldItem)equipmentItem;
        }
    }

	public void Unequip(SOEquipmentItem equipmentItem)
    {
        if (equipmentItem.GetType() == typeof(SOHelmetItem))
        {
            _equipmentSO.HelmetItem = null;
        }
        else if (equipmentItem.GetType() == typeof(SOArmorItem))
        {
            _equipmentSO.ArmorItem = null;
        }
        else if (equipmentItem.GetType() == typeof(SOWeaponItem))
        {
            _equipmentSO.WeaponItem = null;
        }
        else if (equipmentItem.GetType() == typeof(SOShieldItem))
        {
            _equipmentSO.ShieldItem = null;
        }
    }
}