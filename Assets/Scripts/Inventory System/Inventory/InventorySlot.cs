using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField]
    private Image _iconImage;
    [SerializeField]
    private Button _useButton;
    [SerializeField]
    private TextMeshProUGUI _amountText;

    private ItemAmount _itemAmount;

    public void SetupSlot(ItemAmount newItemAmount)
    {
        _itemAmount = newItemAmount;

        _iconImage.sprite = newItemAmount.Item.Icon;
        _iconImage.enabled = true;
        _useButton.interactable = true;

        _amountText.text = newItemAmount.Amount == 1 ? "" : newItemAmount.Amount.ToString();
    }

    public void ClearSlot()
    {
        _itemAmount = null;

        _iconImage.sprite = null;
        _iconImage.enabled = false;
        _useButton.interactable = false;
        _amountText.text = "";
    }

    public void OnUseButton()
    {
        if (_itemAmount != null)
        {
            _itemAmount.Item.UseItem();
        }
    }
}