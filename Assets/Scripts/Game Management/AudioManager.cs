using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource _musicSource;
    // Could break this up into multiple categories of SFX, 
    // like footsteps, voices, combat, etc. And each could
    // have its own slider to control its volume. 
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

    // Volume goes from 0 to 1. Hook up to a slider in UI.
    public void ChangeMainVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    public void ChangeMusicVolume(float volume)
    {
        _musicSource.volume = volume;
    }

    public void ChangeSFXVolume(float volume)
    {
        _sfxSource.volume = volume;
    }
}