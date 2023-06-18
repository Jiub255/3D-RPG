using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
	[SerializeField]
	private SOEquipment _equipmentSO;
    [SerializeField]
    private Transform _weaponParent;
    [SerializeField]
    private SOEquipmentType _weaponEquipmentType;

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

    public void Equip(SOEquipmentItem newItem)
    {
        _equipmentSO.Equip(newItem);

        if (newItem.EquipmentType == _weaponEquipmentType)
        {
            // Destroy old weapon object. 
            Destroy(_weaponParent.GetChild(0).gameObject);
        
            // Instantiate new weapon object
            GameObject weapon = Instantiate(newItem.EquipmentItemPrefab, _weaponParent);
            weapon.transform.localPosition = Vector3.zero;
        }
    }

    public void Unequip(SOEquipmentItem equipmentItem)
    {
        _equipmentSO.Unequip(equipmentItem);

        // Destroy old weapon object. 
        Destroy(_weaponParent.GetChild(0).gameObject);
    }
}