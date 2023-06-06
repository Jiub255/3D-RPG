using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SOEquipment : ScriptableObject
{
	public event Action OnEquipmentChanged;

	public SOWeaponItem WeaponItem;
	public SOArmorItem ArmorItem;
	public SOHelmetItem HelmetItem;
	public SOShieldItem ShieldItem;
}