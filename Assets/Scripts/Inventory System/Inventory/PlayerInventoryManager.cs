// Only extending InventoryManager to listen for add/remove item events, 
// so that those events don't affect every inventory in the scene, only the player's. 
public class PlayerInventoryManager : InventoryManager
{
    private void OnEnable()
    {
        SOInventoryItem.OnAddItem += AddItem;
        SOEquipment.OnUnequip += AddItem;
        SOInventoryItem.OnRemoveItem += RemoveItem;
    }

    private void OnDisable()
    {
        SOInventoryItem.OnAddItem -= AddItem;
        SOEquipment.OnUnequip -= AddItem;
        SOInventoryItem.OnRemoveItem -= RemoveItem;
    }
}