using UnityEngine;

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