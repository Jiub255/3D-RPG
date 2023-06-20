using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public PlayerControls PC { get; private set; }

    public bool MovePlayerPressed { get; private set; }

    private void Awake()
    {
        PC = new PlayerControls();

        // Enable default action maps. 
        PC.Disable();
        PC.UI.Enable();
        PC.Camera.Enable();
        PC.Movement.Enable();
    }

    // Need to do this in case an action was held down while disabling actions or action maps.
    // It's an ugly fix that throws six errors, but it works for now. 
    // If you don't do this fix, when you re-enable the action, it doesn't respond to the first input
    // until you have released the button. Reset() doesn't help. Maybe updating unity/input system will? 
    public void DisableActionMap(InputActionMap actionMap)
    {
        // This fixes the bug but causes errors, other action maps wont work after.  
        // Not really sure what's going on and how it works. 
/*        PC.Movement.MovePlayer.Dispose();
        PC.Movement.Melee.Dispose();
        PC.Movement.Interact.Dispose();
        // This fixes the bug introduced by the above bug fix. 
        PC.Enable();*/

        actionMap.Disable();
    }

    private void OnEnable()
    {
        PC.Movement.MovePlayer.started += (c) => MovePlayerPressed = true;
        PC.Movement.MovePlayer.canceled += (c) => MovePlayerPressed = false;
    }

    private void OnDisable()
    {
        PC.Movement.MovePlayer.started -= (c) => MovePlayerPressed = true;
        PC.Movement.MovePlayer.canceled -= (c) => MovePlayerPressed = false;
    }
}