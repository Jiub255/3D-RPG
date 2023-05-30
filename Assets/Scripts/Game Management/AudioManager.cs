using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource _musicSource;
    [SerializeField]
    private AudioSource _sfxSource;

    private void Start()
    {
        _musicSource.ignoreListenerPause = true;
    }

    public void PlaySoundEffect(AudioClip effectClip)
    {
        if (_sfxSource != null)
        {
            // Using PlayOneShot so you can play multiple clips at once without cutting off the previous played one. 
            _sfxSource.PlayOneShot(effectClip);
        }
    }

    public void PlayMusic(AudioClip musicClip)
    {
        if (_musicSource != null)
        {
            _musicSource.clip = musicClip;
            _musicSource.Play();
        }
    }
}