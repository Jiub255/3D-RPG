using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{
    [SerializeField]
    private Image _iconImage;

    private SOEquipmentItem _equipmentItem;

    public void SetupSlot(SOEquipmentItem equipmentItem)
    {
        _equipmentItem = equipmentItem;

        _iconImage.sprite = equipmentItem.Icon;
    }

    // Called by "unequip" button on slot prefab. 
    public void OnUseButton()
    {
        if (_equipmentItem != null)
        {
            _equipmentItem.Unequip();
        }
    }
}