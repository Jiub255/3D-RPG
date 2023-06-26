using System;
using System.Collections;
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
    [SerializeField]
    private GameObject _lootCanvas;

    private GameManager _gameManager;
    private PlayerControls _playerControls;

    private void Start()
    {
        S.I.IM.PC.UI.ToggleStats.started += ToggleStatsMenu;
        S.I.IM.PC.UI.ToggleInventory.started += ToggleInventory;
        NPCDialog.OnInteractWithNPC += EnableDialogCanvas;
        UIDialog.OnDialogEnd += DisableDialogCanvas;
        EnemyLootController.OnEnteredFirstLootTrigger += OpenLootMenu;
        EnemyLootController.OnEnemyLootListsEmptied += CloseLootMenu;

        _gameManager = S.I.GameManager;
        _playerControls = S.I.IM.PC;
    }

    private void OnDisable()
    {
        S.I.IM.PC.UI.ToggleStats.started -= ToggleStatsMenu;
        S.I.IM.PC.UI.ToggleInventory.started -= ToggleInventory;
        NPCDialog.OnInteractWithNPC -= EnableDialogCanvas;
        UIDialog.OnDialogEnd -= DisableDialogCanvas;
        EnemyLootController.OnEnteredFirstLootTrigger -= OpenLootMenu;
        EnemyLootController.OnEnemyLootListsEmptied -= CloseLootMenu;
    }

    private void OpenLootMenu()
    {
        _lootCanvas.SetActive(true);
    }

    private void CloseLootMenu()
    {
        _lootCanvas.SetActive(false);
    }

    private void ToggleStatsMenu(InputAction.CallbackContext context)
    {
        if (_statsCanvas.activeInHierarchy)
        {
            _statsCanvas.SetActive(false);
            Pause(false);
            _playerControls.Movement.Enable();
        }
        else
        {
            _statsCanvas.SetActive(true);
            OnOpenStatsMenu?.Invoke();
            Pause(true);
        }
    }

    private void Pause(bool pause)
    {
        _gameManager.Pause(pause);
    }

    // Wait one frame to let the dialog camera line up.
    // Not sure if it helped. 
    private void EnableDialogCanvas(Transform _)
    {
        _dialogCanvas.SetActive(true);
       Pause(true);
//        StartCoroutine(WaitThenPause());
    }

    private IEnumerator WaitThenPause()
    {
        yield return new WaitForEndOfFrame();
        Pause(true);
    }

    private void DisableDialogCanvas()
    {
        _dialogCanvas.SetActive(false);
        Pause(false);
    }

    private void ToggleInventory(InputAction.CallbackContext _)
    {
        if (_inventoryCanvas.activeInHierarchy)
        {
            _inventoryCanvas.SetActive(false);
            Pause(false);
        }
        else
        {
            _inventoryCanvas.SetActive(true);
            OnOpenInventory?.Invoke();
            Pause(true);
        }
    }
}