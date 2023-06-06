using UnityEngine;

[System.Serializable]
public class ItemAmount
{
	public SOInventoryItem Item;
	public int Amount;

	public ItemAmount(SOInventoryItem item, int amount)
    {
		Item = item;
		Amount = amount;
    }
}