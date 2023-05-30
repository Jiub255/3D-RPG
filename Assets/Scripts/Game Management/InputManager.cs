using UnityEngine;

public class InputManager : MonoBehaviour
{
    public PlayerControls PC { get; private set; }

    private void Awake()
    {
        PC = new PlayerControls();

        // Enable "World" as default action map
        PC.Disable();
        PC.World.Enable();
    }
}