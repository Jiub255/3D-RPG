// Only extending InventoryManager to listen for add/remove item events. 
public class PlayerInventoryManager : InventoryManager
{
    private void OnEnable()
    {
        SOInventoryItem.OnAddItem += AddItem;
        SOInventoryItem.OnRemoveItem += RemoveItem;
    }

    private void OnDisable()
    {
        SOInventoryItem.OnAddItem -= AddItem;
        SOInventoryItem.OnRemoveItem -= RemoveItem;
    }
}