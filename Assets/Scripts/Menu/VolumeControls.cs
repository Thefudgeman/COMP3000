using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControls : MonoBehaviour
{
    public Slider masterVolume;
    public Slider musicVolume;
    public Slider hitSoundVolume;
    public Slider soundFxVolume;

    public AudioMixer audioMixer;

    // Update is called once per frame
    void Update()
    {
        audioMixer.SetFloat("Master", Mathf.Log10(masterVolume.value)*20);
        audioMixer.SetFloat("Music", Mathf.Log10(musicVolume.value) * 20);
        audioMixer.SetFloat("HitSounds", Mathf.Log10(hitSoundVolume.value) * 20);
        audioMixer.SetFloat("SoundFX", Mathf.Log10(soundFxVolume.value) * 20);
    }
}
