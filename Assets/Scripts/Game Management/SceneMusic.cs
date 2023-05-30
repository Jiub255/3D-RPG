using UnityEngine;

public class SceneMusic : MonoBehaviour
{
    public AudioClip sceneMusic;

    public void Start()
    {
        S.I.AudioManager.PlayMusic(sceneMusic);
    }
}