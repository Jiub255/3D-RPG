using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuController : MonoBehaviour
{
    public static event Action OnOpenInventory;
    public static event Action OnOpenStatsMenu;

	[SerializeField]
	private GameObject _inventoryCanvas;
    [SerializeField]
    private GameObject _dialogCanvas;
    [SerializeField]
    private GameObject _statsCanvas;

    private void Start()
    {
        S.I.IM.PC.UI.ToggleStats.started += ToggleStatsMenu;
        S.I.IM.PC.UI.ToggleInventory.started += ToggleInventory;
        NPCDialog.OnInteractWithNPC += EnableDialogCanvas;
        UIDialog.OnDialogEnd += DisableDialogCanvas;
    }

    private void OnDisable()
    {
        S.I.IM.PC.UI.ToggleStats.started -= ToggleStatsMenu;
        S.I.IM.PC.UI.ToggleInventory.started -= ToggleInventory;
        NPCDialog.OnInteractWithNPC -= EnableDialogCanvas;
        UIDialog.OnDialogEnd -= DisableDialogCanvas;
    }

    private void ToggleStatsMenu(InputAction.CallbackContext context)
    {
        if (_statsCanvas.activeInHierarchy)
        {
            _statsCanvas.SetActive(false);
        }
        else
        {
            _statsCanvas.SetActive(true);
            OnOpenStatsMenu?.Invoke();
        }
    }

    private void EnableDialogCanvas(Transform _)
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
            _inventoryCanvas.SetActive(true);
            OnOpenInventory?.Invoke();
        }
    }
}