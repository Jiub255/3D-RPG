using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuController : MonoBehaviour
{
    public static event Action OnOpenInventory;

	[SerializeField]
	private GameObject _inventoryMenu;

    private void Start()
    {
        S.I.IM.PC.World.ToggleInventory.started += ToggleInventory;
    }

    private void OnDisable()
    {
        S.I.IM.PC.World.ToggleInventory.started -= ToggleInventory;
    }

    private void ToggleInventory(InputAction.CallbackContext _)
    {
        if (_inventoryMenu.activeInHierarchy)
        {
            _inventoryMenu.SetActive(false);
        }
        else
        {
            OnOpenInventory?.Invoke();
            _inventoryMenu.SetActive(true);
        }
    }
}