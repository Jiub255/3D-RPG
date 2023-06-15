using UnityEngine;

public class InputManager : MonoBehaviour
{
    public PlayerControls PC { get; private set; }

    private void Awake()
    {
        PC = new PlayerControls();

        // Enable default action maps. 
        PC.Disable();
        PC.UI.Enable();
        PC.Camera.Enable();
        PC.Movement.Enable();
    }
}