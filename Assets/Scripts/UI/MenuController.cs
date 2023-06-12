using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuController : MonoBehaviour
{
    public static event Action OnOpenInventory;

	[SerializeField]
	private GameObject _inventoryCanvas;
    [SerializeField]
    private GameObject _dialogCanvas;

    private void Start()
    {
        S.I.IM.PC.World.ToggleInventory.started += ToggleInventory;
        NPCDialog.OnInteractWithNPC += EnableDialogCanvas;
        UIDialog.OnDialogEnd += DisableDialogCanvas;
    }

    private void OnDisable()
    {
        S.I.IM.PC.World.ToggleInventory.started -= ToggleInventory;
        NPCDialog.OnInteractWithNPC -= EnableDialogCanvas;
        UIDialog.OnDialogEnd -= DisableDialogCanvas;
    }

    private void EnableDialogCanvas()
    {
        _dialogCanvas.SetActive(true);
    }

    private void DisableDialogCanvas()
    {
        _dialogCanvas.SetActive(false);
    }

    private void ToggleInventory(InputAction.CallbackContext _)
    {
        if (_inventoryCanvas.activeInHierarchy)
        {
            _inventoryCanvas.SetActive(false);
        }
        else
        {
            OnOpenInventory?.Invoke();
            _inventoryCanvas.SetActive(true);
        }
    }
}