using UnityEngine;

public class GameManager : MonoBehaviour
{
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