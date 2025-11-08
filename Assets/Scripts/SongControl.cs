using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongControl : MonoBehaviour
{

    public AudioSource audioSource;
    public static SongControl Instance;
    public float songDelay;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        Invoke(nameof(StartSong), songDelay);
    }


    public void StartSong()
    {
        audioSource.Play();
    }

    public void PauseSong()
    {
        audioSource.Pause();
    }

    public static double GetSongTime() //get a more accurate value for the audio source play time
    {
        return (double)Instance.audioSource.timeSamples / Instance.audioSource.clip.frequency;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
