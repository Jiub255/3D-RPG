using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField]
    private Image _iconImage;
    [SerializeField]
    private TextMeshProUGUI _amountText;

    private ItemAmount _itemAmount;

    public void SetupSlot(ItemAmount newItemAmount)
    {
        _itemAmount = newItemAmount;

        _iconImage.sprite = newItemAmount.Item.Icon;

        _amountText.text = newItemAmount.Amount == 1 ? "" : newItemAmount.Amount.ToString();
    }

    // Called by "use" button on slot prefab. 
    public void OnUseButton()
    {
        if (_itemAmount != null)
        {
            _itemAmount.Item.UseItem();
        }
    }
}