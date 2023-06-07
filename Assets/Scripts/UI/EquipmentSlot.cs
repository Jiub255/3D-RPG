using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{
    [SerializeField]
    private SOEquipmentType _equipmentType;
    [SerializeField]
    private Image _iconImage;

    private SOEquipmentItem _equipmentItem;
    
    public SOEquipmentType EquipmentType { get { return _equipmentType; } }

    public void SetupSlot(SOEquipmentItem equipmentItem)
    {
        _equipmentItem = equipmentItem;
        _iconImage.enabled = true;
        _iconImage.sprite = equipmentItem.Icon;
    }

    public void ClearSlot()
    {
        _equipmentItem = null;
        _iconImage.enabled = false;
    }

    // Called by "unequip" button on slot prefab. 
    public void OnUnequipButton()
    {
        if (_equipmentItem != null)
        {
            _equipmentItem.Unequip();
        }
    }
}