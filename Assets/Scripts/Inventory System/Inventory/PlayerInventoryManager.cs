// Only extending InventoryManager to listen for remove item events. 
public class PlayerInventoryManager : InventoryManager
{
    private void OnEnable()
    {
        SOItem.OnRemoveItem += RemoveItem;
    }

    private void OnDisable()
    {
        SOItem.OnRemoveItem -= RemoveItem;
    }
}