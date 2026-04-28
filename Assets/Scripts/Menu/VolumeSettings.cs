using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    public Slider masterVolume;
    public Slider musicVolume;
    public Slider hitSoundVolume;
    public Slider soundFxVolume;

    public AudioMixer audioMixer;
    public RawImage optionsMenu;

    public void OpenOptions()
    {
        optionsMenu.gameObject.SetActive(true);
    }

    public void SaveChanges()
    {
        PlayerPrefs.SetFloat("MasterVolume", masterVolume.value);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume.value);
        PlayerPrefs.SetFloat("HitSoundVolume", hitSoundVolume.value);
        PlayerPrefs.SetFloat("SoundFXVolume", soundFxVolume.value);
        optionsMenu.gameObject.SetActive(false);
    }

    void Start()
    {
        if (PlayerPrefs.GetFloat("MasterVolume") > 0.0)
        {
            masterVolume.value = PlayerPrefs.GetFloat("MasterVolume");

        }
        if (PlayerPrefs.GetFloat("MusicVolume") > 0.0)
        {
            musicVolume.value = PlayerPrefs.GetFloat("MusicVolume");

        }
        if (PlayerPrefs.GetFloat("HitSoundVolume") > 0.0)
        {
            hitSoundVolume.value = PlayerPrefs.GetFloat("HitSoundVolume");

        }
        if (PlayerPrefs.GetFloat("SoundFXVolume") > 0.0)
        {
            soundFxVolume.value = PlayerPrefs.GetFloat("SoundFXVolume");

        }

        audioMixer.SetFloat("Master", Mathf.Log10(masterVolume.value) * 20);
        audioMixer.SetFloat("Music", Mathf.Log10(musicVolume.value) * 20);
        audioMixer.SetFloat("HitSounds", Mathf.Log10(hitSoundVolume.value) * 20);
        audioMixer.SetFloat("SoundFX", Mathf.Log10(soundFxVolume.value) * 20);
    }
}
