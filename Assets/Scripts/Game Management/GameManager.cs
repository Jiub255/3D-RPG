using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action OnGameTimeMultiplierChanged;

    [SerializeField]
    private SOGameTime _gameTimeSO;

    private void FixedUpdate()
    {
        _gameTimeSO.IncrementTime(Time.fixedDeltaTime);
    }

    public GameTime GetGameTime()
    {
        return _gameTimeSO.GetGameTime();
    }

    public float GetGameTimeMultiplier()
    {
        return _gameTimeSO.GameTimeMultiplier;
    }

    public void SetGameTimeMultiplier(float multiplier)
    {
        _gameTimeSO.GameTimeMultiplier = multiplier;
        // SunOrbit listens to update its multiplier. 
        OnGameTimeMultiplierChanged?.Invoke();
    }

    public void Pause(bool pause)
    {
        Time.timeScale = pause ? 0 : 1;
    }

    /*    private void Start()
        {
            S.I.IM.PC.Home.Quit.performed += QuitGame;
            S.I.IM.PC.Scavenge.Quit.performed += QuitGame;
        }

        private void OnDisable()
        {
            S.I.IM.PC.Home.Quit.performed -= QuitGame;
            S.I.IM.PC.Scavenge.Quit.performed -= QuitGame;
        }

        private void QuitGame(InputAction.CallbackContext obj)
        {
            Quitter.Quit();
        }*/
}