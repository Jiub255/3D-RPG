using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    // Is this script necessary? Or just a pointless middle man?
    // Could just send the events straight from the SOEquipmentItem to the SOEquipment. 
    // Except SOEquipment is an SO, not a monobehaviour, so how to subscribe it to events? 

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

    public void Equip(SOEquipmentItem newItem)
    {
        _equipmentSO.Equip(newItem);
    }

    public void Unequip(SOEquipmentItem equipmentItem)
    {
        _equipmentSO.Unequip(equipmentItem);
    }
}