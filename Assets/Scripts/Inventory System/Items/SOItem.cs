using UnityEngine;

public class SOItem : ScriptableObject
{
	[SerializeField]
	protected string _description = "Enter Item Description";

	[SerializeField]
	protected Sprite _icon;

	public virtual void UseItem()
    {
		Debug.Log($"Used {name}"); 
    }

	public virtual void PickUpItem()
    {
		Debug.Log($"Picked up {name}");
    }

	protected void RemoveFromInventory()
    {

    }
}