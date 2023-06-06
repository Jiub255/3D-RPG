using UnityEngine;

[System.Serializable]
public class ItemAmount
{
	public SOItem Item;
	public int Amount;

	public ItemAmount(SOItem item, int amount)
    {
		Item = item;
		Amount = amount;
    }
}